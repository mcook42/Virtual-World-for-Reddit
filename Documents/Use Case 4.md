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
