# Machine-Learning-Project

In this personal project, I have created applications that utuilize Python, C# using the .NET framework and machine learning. This project collects Reddit comments from a post and puts them into an array that is then converted into the JSON file format. Then using that JSON file to move through Microsoft machine learning functions using sample data to determine the probability of those Reddit comments of being toxic or non-toxic. 

First you will want to run the Python application to generate the JSON file.
NOTE: I am using the praw.ini file for my credentials, client id, and client secret. You will need to add a praw.ini file to your working python directory. You can use this example format:

[bot1] 

client_id=Y4PJOclpDQy3xZ

client_secret=UkGLTe6oqsMk5nHCJTHLrwgvHpr

password=pni9ubeht4wd50gk 

username=fakebot1 

Next you will want to copy the full path of the JSON file that it created and paste it into the path varible on line 20 of Program.cs

Then copy and paste the full path of the machine learning model provided into the variable DataPath on line 15 of Program.cs

After doing this the program should print the number of comments processed and the probability of those comments being toxic or non-toxic. Anything under 1.5 is non-toxic and anything over the 1.5 threshold is considered a post with toxic comments.

Note: You can change the Reddit post in the reddit query.py file on line 10 by using a different URL.

