import praw
from praw.models import MoreComments
import json

# client id, client secret, password, username
reddit = praw.Reddit("bot1", user_agent="bot1 user agent")

# reddit post for comment extraction
submission = reddit.submission(
    url='https://www.reddit.com/r/ShouldIbuythisgame/comments/gi1tn3/sib_breath_of_the_wild/')

# grab Reddit comments and put into array
postlist = []
submission.comments.replace_more(limit=None)
for comment in submission.comments.list():
    post = [comment.body]
    postlist.append(post)

# Debug print(*postlist, sep='\n')

# print array of Reddit comments to JSON file
with open("data.json", "w") as write_file:
    json.dump(postlist, write_file)
