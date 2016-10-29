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
#subreddit = reddit.get_subreddit("funny")

#subs = {}
# Get hot urls referenced by a subreddit and the corresponding comments
#for submission in subreddit.get_hot(limit=10):
#    subs.update({submission.url : submission.comments})

# For each url, print the top comments alongside their original url
for key in subs.keys():
    print("URL: ", key)    
    print("Comments: ")
    for item in subs[key]:
        if isinstance(item, MoreComments):
            continue
        print(item.body)