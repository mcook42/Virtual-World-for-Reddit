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

#Use Cases

##Use Case Overview

Our application has three summary level use cases.

1. Our server periodically loads information from Reddit and then sends this information to our application.

2. Our application creates a three dimensional world based off of the Reddit information.

3. Users travel around the world, view Reddit content, and if they have an account, log in and interact with Reddit content.


##Use Case: 1, Server Responds to Requests and Processes Information

###Characteristic Information

**Primary Actor:** Server

**Scope:** Reddit Data Handling/Backend

**Level:** Background (?)

**Trigger:** Server recieves request

**Success End Condition:** Server remains updated and promptly responds to requests

**Failed End Condition:** Server responds to all requests with error messages

###Stakeholders/Interests

 * Reddit users: Must receive data in timely fashion

 * Server: Must correctly structure data and preserve user preferences

 * Reddit: Requires its data to be appropriately used and represented, no misuse via content downloading or uploading
 
###Preconditions

 * Server is currently running

 * Reddit is currently running and a proper connection exists via OAuth2

###Minimal Gaurantee

 * Server can receive and process requests, and respond with appropriate error messages or data obtained successfully.

 * Server recognizes the need to update its content and will attempt to do so

 * All data successfully required is correctly stored

###Main Success Scenario

1. Recognizes a need to update data

 a) Request from user

 b) Automatic update

 c) Processing of graphic-specific data

2. Respond to the request

 a) Query the table for data

 b) Request the data from Reddit

3. Process, format, and return the acquired data to the user

###Extensions

 * 2: Server overloaded with requests
 
   + 1: Ignore new requests until request queue has room to accept new requests
   
 * 2a: Query takes too long
 
   + 1: Notify developers
   
   + 2: Notify user
   
   + 3: Let query continue unless there is a lot of traffic
   
   + 4: Abort query; Send error message to user and developers 

 * 2b: Unable to retrieve information from Reddit
 
   + 1: Retry the request 5 times
   
   + 2: Respond to user with an appropriate error message
   
   + 3: Notify developers that Reddit is inaccessible
   
   + 4: Record that server may not be up to date
  
 * 1a: User spams server with requests

   + 1: Remember user
   
   + 2: Ignore all requests from user for a set time period
   
###SCHEDULE
   
**Due Date:** release 1.0


##Use Case: 2, Mapping Reddit Features to a 3D World

###Charactersitic Information

**Goal:** Our application will map the majority of Reddit's features into a 3D world. Reddit objects such as posts, comments, and subreddits will be represented in objects in our city such as buildings, stick figures and neighborhoods. Interactions with Reddit will be available through interactions with these objects. 

**Primary Actor:** Unity Application

**Scope:** Backend of Application

**Level:** System

**Trigger:** User opens application or user travels to a part of the world that is not yet loaded.

**Success End Condition:** A 3D world is created from Reddit content.

**Failed End Condition:** An error message is produced.

###Stakeholders/Interests

 * Unity Application: Successfully generate 3D world from provided Reddit content.
 
 * User: View and interat with generated objects.
 
###Preconditions

 * Application has loaded Reddit content from our server
 
###Minimal Guarantee

 Application will create objects from all content that has been loaded. 

###Main Success Scenario

Application will create objects from all Reddit content loaded. All of the objects together will form a fully interactive city environment. Listed below is the overall city structure and all Reddit features we plan to implement. The list does not explain the Reddit features. For those new to Reddit, Reddit's main features are explained on the [Reddit Wikipedia page](https://en.wikipedia.org/wiki/Reddit) as well as in this [article](http://www.makeuseof.com/tag/download-best-of-the-web-delivered-the-reddit-manual/). A list of all Reddit's features (without explanation) can be found [here](https://www.reddit.com/dev/api/). 

####Overall Layout

The city will consist of buildings. Every building will represent a Subreddit. The buildings will be organized according to the categorization found [here](http://rhiever.github.io/redditviz/clustered/), created by researchers Randal S. Olson and Zachary P. Neal. The overall idea is that similiar Subreddit buildings will be found next to each other. If the user wishes to travel to any subreddits not listed in the categorization, the Subreddit building will be created on the outskirts of the city. Sections of the city will be divided off into neighborhoods. Each neighborhood will have its own style (described more under the Subreddit implementation). Neighborhoods will be created based on a predetermined category. An example categorization created by the Reddit user JAVOK is found [here](http://i.imgur.com/pAUoJLB.jpg). 

####Subreddit Implementation:

A building will be created for every Subreddit. The outside appearance of the building will depend on the neighborhood that the Subreddit is in. Each neighforhood will have a different building style. For example, the gaming neighborhood will consist of buildings that are all gameshops and the minecraft neighborhood will consist of buildings that are all minecraft style dirt houses. Subreddits existing on the outskirts of the city, not inside a neighborhood, will be simple houses.

Each building will have a main entrance. Upon entering one will find themselves in a room with an information desk, an elevator, and 25 doors along the walls. Each one of these objects is described in more detail below.

 * Posts: Each post will be a door on the wall. The name of the post as well as a description will be written beside the door. 

 * Name: Name will be posted above the main entrance of the building.
 
 * Posting Rules: An information desk will hold the posting rules. The information desk will consist of a kiosk with a single stick figure person inside of it. Interaction with the person will cause them to show the user the posting rules, in the form of a text that fills up the screen. 
 
 * Surscriber number: The more surscribers a Subreddit has, the larger the building will become. For every building style, there will be three sizes of buildings. Subreddits with more than 400,000 surscribers will have the maximum building size (approximately 100 Subreddits). Subreddits with more than 100,000 surscribers will have the middle building size (approximately 400 Subreddits). All other Subredditss will have the smallest building size.
 
 * Number of users viewing Subreddit: For every 1000 users viewing a Subreddit, one stick figure person will be created to walk around the building. If there are more than 20,000 users viewing a Subreddit, only 20 stick figure people will be created.
 
 * Activeness of Subreddit: For each building style and size, we will have three models representing the upkeep of the building. The condition will be based on the activeness of the Subreddit. We will look at the most recent 25 new posts and the posting time in order to determine the activeness of a Subreddit. If the average posting time of the 25 most recent posts is below 24 hours, then the building will look brand new. If the average posting time of the 25 most recent posts is below 30 days, the building will look worn. If the average posting time of the 25 most recent psots is above 30 days, the building will look rundown. 
 
 * Organize Subreddit by Top/Hot/New etc: 



##Use Case: 3, User Navigates World and Views Reddit Content

###Characteristic Information

**Primary Actor:** Reddit Guest

**Goal:** Actor will be able to travel around our world, navigate to the Reddit content they desire, and view that content.

**Scope:** User Interface

**Level:** User Goal

**Trigger:** User opens our application

**Success End Condition:** User navigates world, views content, and exits

**Failed End Condition:** User told that content unavailable and application either exits or loads default structure

###Stakeholders/Interests

 * Reddit guests: Navigate and view content in the virtual world

 * Server: Receive correct requests for data and send it back to the user

 * Reddit: Requires its data to be appropriately used and represented, and to give user appropriate warnings when they attempt to access certain functions when not logged in

###Preconditions

 * Application is loaded

 * Server is currently running

 * User is connected to the internet and has a system with correct capabilities (windows, android)

###Minimal Gaurantee

 * The program will open and provide a correct error message

 * The program will handle errors from the server

###Main Success Scenario

1. User appears in the main page of the virtual world

2. User successfully navigates to desired content (subreddit, thread, comment)

 a) Using map

 b) Using searchbar

3. User views content

 a) If the user attempts to interact with content, the program will prompt the user to login or create an account

4. User downloads content to their device’s library (images, gifs, videos, etc.)

###Extensions

 * 1: Content unavailable (server down, server not responding, Reddit down etc.)
 
   + 1: Give error message
   
   + 2: Quit application

 * 2: Content unavailable (server down, subreddit private, Reddit down etc.)
 
   + 1: Tell user that the content is unavailable
 
   + 2: In background, continuously attempt to get content
 
   + 3: Load default structure in place of content

 * 3a: Reddit unavailable
 
   + 1: Tell user they are unable to login
   
 * 3a: Unable to login
 
   + 1: Respond with error message
   
###SCHEDULE

**Due Date:** release 1.0


##Use Case: 4, User Interacts With World and Reddit

###Characteristic Information

**Primary Actor:** Reddit User

**Goal:** Actor will interact with objects in our world in order to up/downvote posts, create posts and comments, and view their profile. 

**Scope:** User Interface

**Level:** User Goal

**Success End Condition:** User interacts with the world and changes made in world are propogated to Reddit

**Failed End Condition:** User told that they are unable to interact with the world

**Trigger:** User logs into Reddit from our application

###Stakeholders/Interests

 * Reddit users: Navigate and view content, as well as interact with and create content in the virtual world

 * Server: Receive correct requests for data and send it back to the user, receive created data and send it to reddit

 * Reddit: Requires its data to be appropriately used and represented, and that the user be held to the terms and conditions of usage

###Preconditions

 * User has application loaded

 * User is connected to the internet and has a system with correct capabilities (windows, android)

 * User has/has created an account and is logged in

 * Server is currently running

###Minimal Gaurantee

 * The program will open and provide a correct error message

 * The program will handle errors from the server

###Main Success Scenario

1. User logs in and appears in personal area

2. User views personal data saved to personal area (mailbox, karma, trophy case)

3. User successfully navigates to desired content (subreddit, thread, comment)

 a) Map

 b) Searchbar

 c) Bookmarks in personal area and other saved content

4. User views content

5. User downloads content to their device’s library (images, gifs, videos, etc.)

6. User interacts with content

 a) Up/downvote

 b) Comment

 c) Create post
 
###Extensions

 * 6a. Thread archived
 
   + 1: Inform user that they cannot up/downvote in this thread
   
 * 6b. Thread locked/archived
 
   + 1: Inform user that they cannot comment in this thread
   
 * 6c. Posting restricted in subreddit
 
  + 1: Infrom user that they cannot post in this subreddit
  
###SCHEDULE

**Due Date:** release 1.0
