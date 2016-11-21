Notes
Actors and Goals:	
1. Content viewer
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

Actor: Server
Scope: Reddit Data Handling/Backend
Level: Background (?)
Stakeholders/Interests
Reddit users: Must receive data in timely fashion
Server: Must correctly structure data and preserve user preferences
Reddit: Requires its data to be appropriately used and represented, no misuse via content downloading or uploading
Preconditions
Server is currently running
Reddit is currently running and a proper connection exists via OAuth2
Minimal Gaurantee
Server can receive and process requests, and respond with appropriate error messages or data obtained successfully.
Server recognizes the need to update its content and will attempt to do so
All data successfully required is correctly stored
Main Success Scenario
1. Recognizes a need to update data
a) Request from user
b) Automatic update
c) Processing of graphic-specific data
2. Respond to the request
a) Query the table for data
b) Request the data from Reddit
3. Process, format, and return the acquired data to the user

Actor: Reddit Guest
Scope: User Interface
Level: User Goal
Stakeholders/Interests
Reddit guests: Navigate and view content in the virtual world
Server: Receive correct requests for data and send it back to the user
Reddit: Requires its data to be appropriately used and represented, and to give user appropriate warnings when they attempt to access certain functions when not logged in
Preconditions
Application is loaded
Server is currently running
User is connected to the internet and has a system with correct capabilities (windows, android)
Minimal Gaurantee
The program will open and provide a correct error message
The program will handle errors from the server
Main Success Scenario
1. User appears in the main page of the virtual world
2. User successfully navigates to desired content (subreddit, thread, comment)
a) Using map
b) Using searchbar
3. User views content
a) If the user attempts to interact with content, the program will prompt the user to login or create an account
4. User downloads content to their device’s library (images, gifs, videos, etc.)

Actor: Reddit User
Scope: User Interface
Level: User Goal
Stakeholders/Interests
Reddit users: Navigate and view content, as well as interact with and create content in the virtual world
Server: Receive correct requests for data and send it back to the user, receive created data and send it to reddit
Reddit: Requires its data to be appropriately used and represented, and that the user be held to the terms and conditions of usage
Preconditions
User has application loaded
User is connected to the internet and has a system with correct capabilities (windows, android)
User has/has created an account and is logged in
Server is currently running
Minimal Gaurantee
The program will open and provide a correct error message
The program will handle errors from the server
Main Success Scenario
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
d) Create subreddit
