# Reddit Features and Implementation
  This document lists out every feature that Reddit offers and how we plan to implement that feature.
It is currently a work in progress. Nothing here is set in stone. 
As we come up with new ideas or decide against old ideas, this document will be updated.

This document is organized by Reddit Features. The four main Reddit features subreddits, threads, comments, and users are listed.
Each of these different features has a layout section, an attribute section, and an ability to section.
The layout section lists out how our 3D implementation will be layouted. 
The attribute section lists out the properties of that Reddit section. 
The ability to section lists out the actions that can be performed on these sections (like organize comments by new or add a new comment).
The basic format is Reddit Feature:Our Implementation. 

##Overall Layout

  - A city consisting of buildings and neighborhoods.
  
  - User will be able to either walk or teleport around the city (a map may be used as the teleporation device)
  
  - Neighborhoods will probably only be loaded one at a time (this is due to a restriction in how fast we can get information from Reddit)

##Subreddits: Buildings

-	Layout

  -	Exists in a neighborhood with other similar buildings.

  -	Inside is a room with the first page of threads lined up as doors.
  
  - An elevator allows users to change the threads being viewed

-	Attributes

  -	Name: Name on outside of building

  -	Posting Rules: Information desk inside building

  -	Special Subreddits: Have a special building for each of these or create a neighborhood based around the subreddits in these special subreddits

  -	Moderators: police men walking around a building

  -	Multireddits, multiple subreddits combined into one: Not sure if we want to create one building for the multireddit or make a neighborhood of sorts out of the multireddit

  -	Subscriber #: size of building

  -	Number of Users currently viewing subreddit: have a certain number of random people within the building. 

  -	Age: ?
  
  -Category (Reddit does not automatically put a subreddit into a category. We will have to do this on our own): the neighborhood the building is in

  -Activeness of subreddit (Will have to do something like anylize posting times to get this information): make building more worn down the less active it is.

-	Ability to

  -	Organize threads by hot, top new etc: buttons inside elevator to take us to a new floor

  -	Search for subreddit: a map of our virtual world with a search bar. Searching for a subreddit would bring up a location within the world where the subreddit exists.

  -	View random subreddit: Teleport to random building.

  -	Subscribe to subreddit: Bring building into your neighborhood.

  -	Create your own subreddit: Create you own building. Give the user an option to determine the building design.

  -	Submit thread to subreddit: ?

  -	Posting restrictions: ?

  -	Create multireddit: ?

##Threads: Rooms inside buildings
-	Layout
  -	 layout will need to be large enough to accommodate many comments. 
-	Ability to
  -	Organize comment by hot, top, new etc: Bring certain stick figures to the front of the room.
  -	Search for a thread: Building map located on wall or in elevator, with search bar. 
  -	Up vote/down vote thread: ?
  -	Add thread: ?
  -	Delete your own thread: ?
  -	Share thread: ?
  -	Save thread: Add thread room to the inside of your house.
  -	Hide thread: remove door from building
  -	Report thread: ?
  -	Gild threads: Room made of solid gold.
-	Attributes
  -	Title: text on door
  -	Submission time: clock beside door
  -	Up vote #: ?
  -	subreddit: the building we are inside of
  -	Creator (a user): stick figure at front of room
  -	Percentage up votes/down votes: ?
  -	Sort Thread: Calls different stick figures to the front of the room.
  -	Announcements, special threads posted by moderators that are always sorted to the top: Have a stick figure with a megaphone.

##Comments: Stick Figures
-	Layout
  -	Little stick figures people with the ability to talk and move around.
-	Attributes
  -	Up vote #: happiness of stick figure
  -	Submission time: age of stick figure
  -	User: name tag
  -	Flair: a tee shirt
  -	Thread: room they are in
  -	 [removed]: put a paper bag over their head
-	Ability to
  -	Create: ?
  -	Up vote/down vote: ?
  -	Embed: probably won’t implement
  -	Gild: stick figure sitting on a pile of coins
  -	View parent and children: organize the stick figures into some sort of group based on who their parents are. 
  -	Comments:
  -	Save: ?
  -	Report: send to jail
  -	Reply to: ?
  
##Users: The player
-	Layout
  -	A house. 
  -	A house for each user. Make a neighborhood of just users
-	Attributes
  -	Name: ?
  - Age: Change their actual age
  -	Post Karma: size of house
  -	Comment Karma: size of house
  -	Karma Breakdown by subreddit: ?
  -	Trophy Case: an actual trophy case
  -	Posts: Rooms in house
    -	Overview: sorted by new, top etc: a room
    -	Comments: sorted by new, top etc: a room
    -	Submitted: sorted by new, top etc: a room
  -	Gilding revieced and given: ?
  -	Up voted: ?
  -	Down voted: ?
  -	Hidden: ?
  -	Saved: ?
  -	Preferences: may not implement
  -	Inbox: a mailbox
    -	All: a box of letters
    -	 Unread: mail in mail box
    - Messages: ?
    -	comment replies: ?
    - post replies: ?
    - username mentions: ?
    -Sent: ?
    -	[deleted]: make them as a ghost
-	Abilities
  -	Login/logout: will need a login/logout screen
  -	Add a user as a friend: make that person your neighbor

Mod/Admin Tools: ?
-	At this point, we will probably not worry about implementing any of this. 


