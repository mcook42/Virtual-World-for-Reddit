#Notes
 
#Introduction

We aim to create a 3D city based off of the popular social media site Reddit. In the city, a building will be created for every Subreddit on Reddit. Inside the building, a room will be created for every thread within that Subreddit. Inside every room, stick figure characters will be created for every comment within that thread. Through interaction with these objects, the user will be able to view/interact with Reddit in a three dimensional setting. We make the assumption that our users will already be familiar with Reddit's structure and features.

##Stakeholders and Interests

1. People who have experience using Reddit:

 View Reddit content in a 3D setting.

2. People with a Reddit account: 
 
 View and interact with Reddit content in a 3D setting.
 
3. Reddit Admins/Moderators: 

 Performing moderator and administrator actions is not within the scope of our application. 
 
4. Reddit: 

 Request information from the website using the specific format that Reddit outlines [here](https://github.com/reddit/reddit/wiki/OAuth2).
 
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

1. Our server periodically loads and stores information from Reddit 

2. Our application gets Reddit information from our server 

3. Our application creates a three dimensional world based off of the Reddit information

4. Users without Reddit accounts travel around the world and view the Reddit content

5. Users with a Reddit account login, and then interact with Reddit through our application 


##Use Case: 2, Server Responds to Requests and Processes Information

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

##Use Case: 3, User Navigates World and Views Reddit Content

###Characteristic Information

**Primary Actor:** Reddit Guest

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
