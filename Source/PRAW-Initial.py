from praw.objects import MoreComments
from AuthBot import login

reddit = login()

#EXAMPLES FROM: http://praw.readthedocs.io/en/praw4/pages/getting_started.html
####Example calls to get a subreddit and its general info#####

# assuming you have a Reddit instance referenced by reddit
#subreddit = reddit.get_subreddit("redditdev")
#
#print(subreddit.display_name) # Output: redditdev
#print(subreddit.title) # Output: reddit Development
#print(subreddit.description) # Output: A subreddit for discussion of ...
#
## assuming you have a Subreddit instance referenced by subreddit
#for submission in subreddit.get_hot(limit=10):
#    print(submission.title) # Output: the title of the submission
#    print(submission.ups) # Output: upvote count
#    print(submission.id) # Output: the ID of the submission
#    print(submission.url) # Output: the URL the submission points to
#                          # or the the submission URL if it's a self post
    
#####NEW CODE##########

def getSubredditComments(subreddit):
    """Takes a subreddit and returns a dictionary with (user,comment)"""
    subs = {}
    # Get hot urls referenced by a subreddit and the corresponding comments
    for submission in subreddit.get_hot(limit=10):
        subs.update({submission.author : submission.comments})
    return subs
    
#subreddit = reddit.get_subreddit("funny")
#
#subs = {}
## Get hot urls referenced by a subreddit and the corresponding comments
#for submission in subreddit.get_hot(limit=10):
#    subs.update({submission.url : (submission.user, submission.comments)})
#
## For each url, print the top comments alongside their original url
#for key in subs.keys():
#    print("URL: ", key)    
#    print("Comments: ")
#    for item in subs[key]:
#        # To handle the issue of CommentForests having MoreComments (subForests)
#        if isinstance(item, MoreComments):
#            continue
#        print(item.body)
#
## THE TRAFFIC FOR A SPECIFIC PAGE: /r/funny
#traffic = reddit.get_traffic("funny")
#
##Newest Subreddits right now
#newbie = reddit.get_new_subreddits()
#new = []
#for item in newbie:
#    new.append(item)
    
# Most popular subreddits right now (Generator)
popular = reddit.get_popular_subreddits()
comms = []
for subred in popular:
    temp = getSubredditComments(subred)
    comms.append(temp)

for key in comms[0].keys():
     print("User: ", key)
     print("Comments: ")
     for item in comms[0][key]:
         if isinstance(item, MoreComments):
             continue
         print(item.body)
 

