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

while True:
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
                              accounts_active, subreddit.subscribers, start_time)

            # Insert info into database
            dbInteractions.insert_subreddit(subreddit_info)

        # Since we are overriding PRAW, we must set the 2 second delay ourselves
        loop_time = time.time() - loop_time
        if loop_time > 2.0:
            loop_time = 2

        print(loop_time)
        time.sleep(2 - loop_time)

    except exceptions.APIException as e:
        logger.exception("API Exception at i = %s: ", i, e)
        # End the program
        exit(1)

