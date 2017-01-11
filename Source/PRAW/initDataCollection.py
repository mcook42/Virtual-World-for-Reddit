#!/usr/bin/env python
# -*- coding: utf-8 -*-
"""
For intitial data collection on the server side.
"""

from AuthBot import login_read_only

__author__ = "Matt Cook"
__version__ = "0.0.1"
__email__ = "mattheworion.cook@gmail.com"

# Login and obtain a Reddit object to work with
r = login_read_only()

# TESTED

# TODO: Utilize this structure to gather data for user's Front Page, too
# TODO: Change limit to be controlled by the user's settings
# for submission in r.subreddits.default(limit=50):
#     # TODO: Make this store in the appropriate database table
#     # TODO: Extract all data needed e.g. submission.mod.*
#     print(submission.title,
#           submission.id)
#
#     for comment in submission.comments(limit=200):
#         # TODO: Make this store in the appropriate database table
#         print(comment.author,
#               comment.body,
#               comment.id,
#               comment.submission.id)

# TESTED
# for submission in r.subreddits.new(limit=20):
#     print(submission.title,
#           submission.id,
#           submission.subscribers)

r.user
