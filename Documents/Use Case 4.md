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

1. User enters Username and Password into login screen and appears in personal area.
   (For more information on personal area view Use Case 2)

2. User views personal data saved to personal area (mailbox, karma, trophy case)

3. User successfully navigates to desired content (subreddit, thread, comment)

4. User views content which is rendered on screen (for more info view Use Case 2)

5. User downloads content to their device’s library (images, gifs, videos, etc.). The user interacts with a popup to confirm the save to device.

6. User interacts with content

 a) User clicks on up/down vote icons.  Selection is sent to Reddit with Oauth2 credentials via PRAW.

 b) Comment is created within a popup screen using keyboard. The post is then sent to Reddit with Oauth2 credentials via PRAW.

 c) Create post within a popup screen using keyboard.  The post is then sent to Reddit with Oauth2 credentials via PRAW.
 
 d) Subscribe to a subreddit by clicking on a door bell outside the main door. The user is then subscribed to the subreddit using Oauth2 credentials via PRAW.
 
###Extensions

 * 1a. Username/Password incorrect

   + 1: Inform user with popup that credentials are incorrect. Allow user to close popup and try again. If necessary, redirect to Reddit login page for more help.
 
 * 1b. Username/Password correct

   + 1: User recieves Oauth2 token to continue to interact with Reddit content later on.  Token is stored on user computer.

 * 2a. User views trophies in trophy case

   + 1: Trophies specific to user are rendered inside trophy case embossed with specific trophy information.

 * 2b. User views mailbox

   + 1: Mail is rendered as envelopes with sender's username and the subject of the mail.  Rendered as opened/unopened envelope respectively.

   + 2: Mail can be opened by clicking "open" button, which renders the mail content
  
   + 3: Mail can be replied to by clicking "reply", or deleted by "delete".

 * 3a. User uses graphical map to navigate
   
   + 1: User clicks on "map" icon on screen to render the map of reddit communities (more on this in Use Case 2)
 
   + 2: User's can click on subreddit to navigate to it.  Popup will show to confirm navigation.

 * 3b. User uses searchbar to navigate
  
   + 1: User enters specific subreddit to navigate to. If subreddit exists, navigation is performed. If not, suggestions will be made visible below search bar.

 * 3c. User uses bookmarks to navigate

   + 1: User views their bookmarked links and clicks on one.  User is navigated to the specific link within the world.

 * 6a. Thread archived
 
   + 1: Inform user that they cannot up/downvote in this thread
   
 * 6b. Thread locked/archived
 
   + 1: Inform user that they cannot comment in this thread
   
 * 6c. Posting restricted in subreddit
 
   + 1: Infrom user that they cannot post in this subreddit
   
  
###SCHEDULE

**Due Date:** release 1.0
