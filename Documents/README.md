#Introduction

We aim to create a 3D city based off of the popular social media site Reddit. In the city, a building will be created for every Subreddit on Reddit. Inside the building, a room will be created for every thread within that Subreddit. Inside every room, stick figure characters will be created for every comment within that thread. Through interaction with these objects, the user will be able to view/interact with Reddit in a three dimensional setting. We make the assumption that our users will already be familiar with Reddit's structure and features.

##Stakeholders and Goals:

1. People with experience using Reddit:

  Our application will provide this stackholder with an alternative method of viewing Reddit. This stackholder will be able to view posts and comments, travel around our world, and create/login to a Reddit account using the objects and features provided in our three dimensional world.

2. People with a Reddit account: 
 
 In addition to viewing Reddit content, this stackholder will be able to create, posts and comments, create bookmarks, view and edit their account profile (bookmarks, mailbox, karma, trophies, and saved comments), give gold, and manage friends.
 
3. Reddit Admins/Moderators: 

 Performing moderator and administrator actions is not within the scope of our application. 
 
4. Reddit: 

 In order to get data from Reddit, our application will request data in the specific format that Reddit outlines [here](https://github.com/reddit/reddit/wiki/OAuth2).
 
5. Server: 

 In order to request data from Reddit, we will use a server. The server will request data from Reddit, store this data in a database, and then send the data to our application upon request. The goal of our server is to keep itself updated with Reddit content and provide this content to our all application upon request. 
 
##Actors and Goals:	

1. Content viewer:

 View posts and comments

 Navigation (via map, search bar)

 Create an account when prompted

2. Content creator

 Login to account or create one

 View posts and comments

 Post and comment

 Save content to personal data

 Create bookmarks

 Navigation (via map, search bar, bookmarks)

 Customize personal area (bookmarks, mailbox, karma, trophy case, saved comments)

 Give gold

 Manage friends and navigate to friend subreddits

3. Server

 Able to receive, process, and respond to requests

 Stay current using automatic and on-demand updates

 Analyze contents for graphics purposes

##Use Case Overview

Our application has four use cases.

1. Our server periodically loads information from Reddit and then sends this information to our application.

2. Our application creates a three dimensional world based off of the Reddit information.

3. Users travel around the world and views Reddit content.

4. When the user logs in, they are able to interact with the Reddit Content.
