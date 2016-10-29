"""
Created by Matt Cook
mattheworion.cook@gmail.com
October 29, 2016

Storage for OAuth2 information and useful functions.

Used as a tutorial: 
https://www.reddit.com/r/GoldTesting/comments/3cm1p8/how_to_make_your_bot_use_oauth2/
"""
import praw


user_agent = 'User-Agent: UwyoSenDesign2017:v0.0.1 (by /u/mcook42)'
app_id = 'DNvP_RE1N9NqQg'
app_uri = 'https://127.0.0.1:65010/authorize_callback'
app_scopes = 'account creddits edit flair history identity livemanage modconfig modcontributors modflair modlog modothers modposts modself modwiki mysubreddits privatemessages read report save submit subscribe vote wikiedit wikiread'
app_code = '3cV6G9Op2_3wVbXzOnU9NgEz8ns'
app_refresh = '64281718-kVkekkFsh-YOeFKUmqlpZF0lCkE'

def login():
    """
    Enters our scripts user information and sets up refresh token.
    Returns reddit object.
    """
    r = praw.Reddit(user_agent)
    r.set_oauth_app_info(app_id, app_secret, app_uri)
    r.refresh_access_information(app_refresh)
    return r