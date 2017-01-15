#!/usr/bin/env python
# -*- coding: utf-8 -*-
"""
For initial user data collection on the server side.
"""

from AuthBot import login_read_only, login

# TODO: Figure out whether it's faster to obtain r.front and get all the info from there, or to obtain own version of
# TODO: (cont.) multireddit constructed from subreddit(subscription names) and get info from there
__author__ = "Matt Cook"
__version__ = "0.0.2"
__email__ = "mattheworion.cook@gmail.com"


# Login and obtain a Reddit object to work with
r = login()

# Get user info
# TODO: Get user's account age (uses unix timestamps) from 'created'
# TODO: Use the user-side client to get mod/mail info and gold info
user = r.user.me()
user_info = {'name': user.name,
             'over_18': str(user.over_18),
             'comment_karma': str(user.comment_karma),
             'link_karma': str(user.link_karma),
             'created': str(user.created)}
print(user_info)

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
subreddit_info = {}

# For getting the default front page for users
for subreddit in r.subreddits.default(limit=None):
    # TODO: Make this store in the appropriate database table
    # TODO: Extract all data needed

    # For age variable of building
    date_created = subreddit.created

    # For signage on building
    display_name = subreddit.display_name

    # For database?
    subreddit_id = subreddit.id

    # For NSFW classification
    over18 = subreddit.over18

    # For determining if traffic info is public (for later use in amount of people in building)
    pub_traffic = subreddit.public_traffic
    # TODO: pub_traffic will be useful for when we get traffic stats later

    # For use in decision to store on server for later use
    subreddit_type = subreddit.subreddit_type

    # For determining size of building
    subscribers = subreddit.subscribers

    # TODO: Replace this section with the database interactions
    subreddit_info[display_name] = [date_created, display_name,
                                    subreddit_id, over18,
                                    pub_traffic, subreddit_type, subscribers]

