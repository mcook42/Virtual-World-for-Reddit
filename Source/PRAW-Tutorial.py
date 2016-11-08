# -*- coding: utf-8 -*-
"""
Created on Sun Oct 30 14:26:17 2016

@author: Someone
"""

import AuthBot

reddit = AuthBot.login()

user_name = "_Daimon_"
user = reddit.get_redditor(user_name)
