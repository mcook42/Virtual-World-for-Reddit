#Introduction

We aim to create a 3D city based off of the popular social media site Reddit. In the city, a building is created for every Subreddit on Reddit. Inside the building, a room is created for every thread within that Subreddit. Inside every room, stick figure characters are created for every comment within that thread. Through interaction with these objects, the user is able to view/interact with Reddit in a three dimensional setting. We make the assumption that our users are already familiar with Reddit's structure and features.

The application exists as both a Windows desktop application as well as a Google Cardboard virtual reality application. For both applications, a keyboard and mouse is used to control the movement of the user. The user views the world through a first person viewpoint. 

In the backend, the application will be created using [Unity](https://unity3d.com/). A server will be used to gather information from Reddit and then send this data to the application.

##Stakeholders and Goals:

1. People with experience using Reddit:

  Our application will provide this stakeholder with an alternative method of viewing Reddit. Using features provided in the world, this stakeholder will be able to:
  
 * View posts and comments.

 * Travel around the world.

 * Create/login to a Reddit account.

2. People with a Reddit account: 
 
 In addition to viewing Reddit content, this stakeholder will be able to:
 
 * Create posts. 
 
 * Create comments. 
 
 * Subscribe to subreddits.
 
 * View and edit their account profile (subscriptions, mailbox, karma, trophies, and saved comments).
 
 * Give gold.
 
 * Manage friends.
 
3. Reddit: 

 In order to get data from Reddit, our application will request data in the specific format that Reddit outlines [here](https://github.com/reddit/reddit/wiki/OAuth2).
 
4. Server: 

 In order to request data from Reddit, we will use a server. The goal of our server is to keep itself updated with Reddit content and provide this content to our application upon request. The server performs the following actions:
 
 * Requests data from Reddit.
 
 * Stores this data in a database.
 
 * Sends the data to our application upon request.
 
5. Reddit Admins/Moderators: 

 Performing moderator and administrator actions is not within the scope of our application. 
 
6. Those unfamiliar with Reddit:

 Our application is not for people without experience using Reddit.

##Use Case Overview

Our application has four use cases.

1. Our server periodically loads information from Reddit and then sends this information to our application.

2. Our application creates a three dimensional world based off of the Reddit information.

3. Users travel around the world and views Reddit content.

4. When the user logs in, they can interact with the Reddit content through objects in the world.
