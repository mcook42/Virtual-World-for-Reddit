#!/usr/bin/env python
"""
Created by Matt Cook
mattheworion.cook@gmail.com
October 29, 2016

Storage for OAuth2 information and useful functions.

Used as a tutorial: 
https://www.reddit.com/r/GoldTesting/comments/3cm1p8/how_to_make_your_bot_use_oauth2/
"""
import praw

app_secret = ''
user_agent = 'VRedditDataCollection by /u/mcook42'
app_id = 'iUXaCcjsrDsXOw'
app_uri = 'http://localhost:8080'
app_scopes = 'identity mysubreddits wikiread read'
app_refresh = ''

# TODO: Rename to login() or get_reddit_instance()
def login_read_only():
    """
    Enters our scripts access info. Returns reddit object.
    """
    r = praw.Reddit(client_id=app_id,
                    redirect_uri='http://localhost:8080',
                    client_secret=app_secret,
                    refresh_token=app_refresh,
                    user_agent=user_agent)
    # Debug
    # print(r.auth.scopes())
    return r

