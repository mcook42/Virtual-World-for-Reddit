#!/usr/bin/env python
# -*- coding: utf-8 -*-
"""
For initial user data collection on the server side.
"""
import time

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
    # Bypass the subreddit.default 49 item limit.
    # NOTE: Reddit's API rate limit is 30 per minute, so we have to limit it ourselves.
    url = "/subreddits/?count=25&after="
    last_fullname = ""

    while True:
        try:
            # Blank argument for 'after' doesn't seem to affect the outcome on the initial request
            after_url = url + last_fullname

            # Get reddit objects from the page to work with
            defaults = r.get(after_url)

            # Update "&after=" argument in get() request to get next page
            last_fullname = defaults.after

            # For delay workaround
            loop_time = time.time()

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
                dbInteractions.insert_subreddit(subreddit_info)

            # Since we are overriding PRAW, we must set the 2 second delay ourselves
            loop_time = time.time() - loop_time
            if loop_time > 2.0:
                loop_time = 2

            print(loop_time)
            time.sleep(2 - loop_time)

        except exceptions.APIException:
            # Obtain a new token
            r = login_read_only()

        except prawcore.RequestException:
            # End the program
            exit(1)


if __name__ == "__main__":
    main()
