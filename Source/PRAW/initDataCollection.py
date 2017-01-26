#!/usr/bin/env python
# -*- coding: utf-8 -*-
"""
For initial user data collection on the server side.
"""
import time
import logging
from praw import exceptions
import psycopg2

from AuthBot import login_read_only, login
import dbInteractions

__author__ = "Matt Cook"
__version__ = "0.0.2"
__email__ = "mattheworion.cook@gmail.com"

# Setup logger
FORMAT = '%(asctime)-15s %(clientip)s %(user)-8s %(message)s'
logging.basicConfig(format=FORMAT)
logger = logging.getLogger('dataCollection')


# Login and obtain a Reddit object to work with
r = login()

# Get user info
user = r.user.me()
user_info = {'name': user.name,
             'over_18': str(user.over_18),
             'comment_karma': str(user.comment_karma),
             'link_karma': str(user.link_karma),
             'created': str(user.created)}

# NOTE: Default limit=100, url = 'subreddits/mine/subscriber'
# TODO: Diagnose issue with subscriptions not appearing sometimes (I can't seem to recreate the issue...)

# Get the subreddits for the user's home
subscribed = list(r.user.subreddits(limit=None))

# TODO: Send names to database
names = []
for item in subscribed:
    names.append(item.display_name)

# TODO: Utilize this structure to gather data for user's Front Page, too
# TODO: Change limit to be controlled by the user's settings
subreddit_info = ()

# Bypass the subreddit.default 49 item limit.
# NOTE: Reddit's API rate limit is 30 per minute
url = "/subreddits/"
last_fullname = ""

start_time = time.time()
i = 0
while i < 29:
    try:
        # Blank argument for 'after' doesn't seem to affect the outcome on the initial request
        after_url = url + "?count=25&after=" + last_fullname

        # Get reddit objects from the page to work with
        defaults = r.get(after_url)

        # Update "&after=" argument in get() request to get next page
        last_fullname = defaults.after

        # For delay workaround
        loop_time = time.time()

        # For getting the default front page objects for users
        for subreddit in defaults:
            # TODO: Make this store in the appropriate database table

            # Since value can be None, and we need an int value, set it to 0 if none
            if subreddit.accounts_active is None:
                accounts_active = 0

            # TODO: Replace this section with the database interactions
            subreddit_info = (subreddit.fullname, subreddit.created_utc, subreddit.description,
                              subreddit.display_name, subreddit.public_description,
                              subreddit.lang, subreddit.over18, subreddit.public_traffic,
                              subreddit.submission_type, accounts_active)

            # Insert info into database
            # dbInteractions.insert_subreddit(subreddit_info)

            try:
                # Connect to an existing database
                conn = psycopg2.connect("dbname=redditserver user=postgres password=m5270685")
            except psycopg2.Error:
                print("Cannot connect to server.")

            # Open a cursor to perform database operations
            cur = conn.cursor()

            """
            Structure of the table:

            TABLE reddit.subreddit(
               full_name text,
               created_utc bigint NOT NULL,
               description text NOT NULL,
               display_name text NOT NULL,
               public_description text NOT NULL,
               language text NOT NULL,
               over18 bool NOT NULL,
               public_traffic bool,
               accounts_active int NOT NULL,
               PRIMARY KEY(full_name)
               );
            """

            # Pass data to fill a query placeholders and let Psycopg perform
            # the correct conversion (no more SQL injections!)
            # Note: Uses the tuple passed in to insert values
            cur.executemany("""INSERT INTO reddit.subreddit (full_name, created_utc, description, display_name, public_description,
                            language, over18, public_traffic, accounts_active)
                            VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s)""", subreddit_info)

            # Make the changes to the database persistent
            conn.commit()
            fetch = conn.fetchone()
            print(fetch)

            # Close communication with the database
            cur.close()
            conn.close()

        i += 1
        # Since we are overriding PRAW, we must set the 2 second delay ourselves
        loop_time = time.time() - loop_time
        print(loop_time)
        time.sleep(2 - loop_time)

    except exceptions.APIException as e:
        logger.exception("API Exception at i = %s: ", i, e)
        # End the loop
        i = 30
        pass


print(len(subreddit_info))
