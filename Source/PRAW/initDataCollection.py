#!/usr/bin/env python
# -*- coding: utf-8 -*-
"""
For initial user data collection on the server side.
"""
import time
import sys

import dbInteractions
import prawcore
from AuthBot import login_read_only
from praw import exceptions

__author__ = "Matt Cook"
__version__ = "1.0.1"
__email__ = "mattheworion.cook@gmail.com"


def main():

    # Login and obtain a Reddit object to work with
    # r = login_read_only()
    r = login_read_only()

    # Get the last full_name from database
    last_fullname = dbInteractions.get_last_full_name()
    # If this script ran recently enough, it could be None. If that's true, make it an empty string.
    if last_fullname is None:
        last_fullname = ""
    url = "/subreddits/?after="

    while True:
        try:
            # Blank argument for 'after' doesn't seem to affect the outcome on the initial request
            after_url = url + last_fullname

            # Get reddit objects from the page to work with
            defaults = r.get(after_url, params={'limit': '100'})

            # Update "&after=" argument in get() request to get next page
            last_fullname = defaults.after

            # For delay workaround
            loop_time = time.time()

            # Open connection to database for this set of subreddits
            # NOTE: The reason we do single insertions is to protect database integrity. We also have no real need to
            # make things go faster because Reddit requires 2 seconds in between requests. It may be necessary later on
            # to make this execute fewer instructions, but for now it is not necessary.
            conn = dbInteractions.open_conn()

            # For getting the pages of subreddit information
            for subreddit in defaults:

                # Since value can be None, and we need an int value, set it to 0 if None
                accounts_active = subreddit.accounts_active
                if accounts_active is None:
                    accounts_active = 0

                # Create tuple for database insertion or updates.  Should be optimal structure
                subreddit_info = (subreddit.fullname, subreddit.created_utc, subreddit.description,
                                  subreddit.display_name, subreddit.public_description,
                                  subreddit.lang, subreddit.over18, subreddit.public_traffic,
                                  accounts_active, subreddit.subscribers, loop_time)

                # Insert info into database
                dbInteractions.insert_subreddit(subreddit_info, conn)

            # Close the connection to the database
            dbInteractions.close_conn(conn)

            # Since we are overriding PRAW, we must set the 2 second delay ourselves
            loop_time = time.time() - loop_time
            if not loop_time > 2.0:
                time.sleep(2 - loop_time)

            # Debugging
            print(loop_time)

        except exceptions.APIException as e:
            # End program
            print("API exception: ", e)
            exit(1)

        except prawcore.RequestException:
            # Refresh the token manually because PRAW won't do it for me...
            r = login_read_only()


if __name__ == "__main__":
    main()
