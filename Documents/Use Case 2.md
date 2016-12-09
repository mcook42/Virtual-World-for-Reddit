##Use Case: 2, Mapping Reddit Features to a 3D World

###Characteristic Information

**Goal:** Our application maps the majority of Reddit's features into a 3D world. Reddit objects such as posts, comments, and subreddits are represented as objects such as buildings, stick figures and neighborhoods. Interactions with Reddit are available through interactions with these objects. 

**Primary Actor:** Unity Application

**Scope:** Backend of Application

**Level:** System

**Trigger:** User opens application or user travels to a part of the world that is not yet loaded.

**Success End Condition:** A 3D world is created from Reddit content.

**Failed End Condition:** An error message is produced.

###Stakeholders/Interests

 * Unity Application: Successfully generate 3D world from provided Reddit content.
 
 * User: View and interact with generated objects.
 
###Preconditions

 * Application has loaded Reddit content from our server
 
###Minimal Guarantee

 * Application will create objects from all content that has been loaded, or return an error.

###Main Success Scenario

Application creates objects from all Reddit content loaded. All of the objects together will form a fully interactive city environment. Listed below is the overall city structure and all Reddit features implemented. The list does not explain the Reddit features. For those new to Reddit, Reddit's main features are explained on the [Reddit Wikipedia page](https://en.wikipedia.org/wiki/Reddit) as well as in this [article](http://www.makeuseof.com/tag/download-best-of-the-web-delivered-the-reddit-manual/). A list of all Reddit's features (without explanation) can be found [here](https://www.reddit.com/dev/api/). 

####Overall Layout

The city consists of buildings. Every building represents a subreddit. The buildings are organized according to the categorization found [here](http://rhiever.github.io/redditviz/clustered/), created by researchers Randal S. Olson and Zachary P. Neal. The overall idea is that similar subreddit buildings are found next to each other. Sections of the city are divided off into neighborhoods. Each neighborhood has its own style (described more under the Subreddit Implementation section). Neighborhoods are created based on a predetermined category. An example categorization created by the Reddit user JAVOK is found [here](http://i.imgur.com/pAUoJLB.jpg). If the user wishes to travel to any subreddits not listed in the categorization, the subreddit building is created on the outskirts of the city. 

####Subreddit Implementation:

A building is created for every subreddit. The outside appearance of the building depends on the neighborhood. Each neighborhood has a different building style. For example, the gaming neighborhood consists of buildings that are all game shops and the Minecraft neighborhood consists of buildings that are all Minecraft style dirt houses. Subreddits existing on the outskirts of the city, not inside a neighborhood, are simple houses.

Each building has a main entrance. Inside the building is a room with an information desk, an elevator, and 25 doors along the walls. Each one of these objects is described in more detail below.

 * Posts: Each post is a door on the wall. Upon first entering the building, the room contains the 25 top, 'hot' posts within the subreddit. The name of the post as well as a description is written beside the door. If the user wishes to see more posts, they can use the elevator, which exists next to the main entrance, inside the room. Upon entering the elevator, the user is presented with an option to rank the posts differently (explained below) in addition to an up and down button. Hitting the up button takes the user to a room with the next 25 posts. Hitting the down button takes the user to a room with the previous 25 posts. If there are no more posts to load, then the up or down button disappears. 
 
 * Organize Subreddit by hot/new/rising/controversial/top/gilded/promoted: The elevator has a button for each possible ranking that Reddit provides (hot, new, rising, controversial, top, gilded, and promoted). Hitting the button takes the user to a room with the top 25 posts for that ranking. The default ranking is hot.

 * Name: The name of the Subreddit is posted above the main entrance of the building. Once inside the building, the name appears at the top of the screen until the user exits the building.
 
 * Posting Rules: An information desk holds the posting rules. The information desk consists of a kiosk with a single stick figure person inside of it. Interaction with the person causes them to show the user the posting rules, in the form of a text that fills up the screen. 
 
 * Subscriber Number: The more subscribers a subreddit has, the larger the building will become. For every building style, there are three sizes of buildings. Subreddits with more than 400,000 surscribers have the maximum building size (approximately 100 subreddits). Subreddits with more than 100,000 surscribers have the middle building size (approximately 400 subreddits). All other subreddits have the smallest building size.
 
 * Number of Users Viewing  the Subreddit: For every 1000 users viewing a subreddit, one stick figure person is created to walk around the building. If there are more than 20,000 users viewing a subreddit, only 20 stick figure people are created. The user is unable to interact with any of these stick figures.
 
 * Activeness of Subreddit: For each building style and size, there are three models representing the upkeep of the building. The model used is based on the activeness of the subreddit.  If the average posting time of the 25 newest posts is below 24 hours, then the building uses a model that looks brand new. If the average posting time of the posts is below 30 days, the building uses a model that looks worn. If the average posting time of the posts is above 30 days, the building uses a model that looks rundown. 
 
####Thread Implementation:

Each thread within a subreddit is represented by a door on the wall inside the main room of the subreddit. Entering the door brings the user into a room. The style of the room matches the style of the building.

 * Name: The name of the thread is written above its corresponding door. Entering the thread's room causes the name to appear at the top of the screen, below the subreddit name. It remains there until the user leaves the thread.
 
 * Up/downvote: The upvotes count is posted on the thread's door. An upvote and downvote button exists outside the door. When the user is logged in, clicking one of these buttons causes the post to be up/downvoted.
 
 * Thread Creator: The thread creator is a stick figure at the front of the room. He/she greets the user as they enter the room.
 
 * Submission Time: A clock and calendar exist on the wall inside the thread room next to the door. The clock shows the time posted and the calendar shows the day posted.
 
 * Announcements: A stick figure with a megaphone exists outside of every door that is an announcement thread. 
 
####Comment Implementation:

For every comment within a thread, a stick figure character is created and put inside the thread room. Only 25 comments will be visible at a time. As described below, interaction with either the thread creator or one of the stick figures can cause new comments to appear. 

 * View parent/children comments: Interaction with a stick figure brings up an option to view that comments parents or children. When the user chooses to view the parents/children, all other stick figures in the room vanish in a poof of dust. The top 24 parent/children comments then appear in the room. Interaction with the thread creator presents the user with an option to load the next or previous 24 parents/children.
  
 * Comment text: Interaction with a stick figure causes that stick figures to say their comment. A text bubble appears above the figure with the comment. 
 
  +  When the comment contains a link to an image/gif, an art canvas appears next to the figure with the image/gif. Gifs are initially paused and will only play upon interaction. When there is more than one link to an image/gif, only the first link is shown on the art canvas. 
  
  + When the comment contains a link to a subreddit, thread, or comment, clicking on the link teleports the user to that subreddit/thread/comment within our world.
  
  + When the comment contains a link to a website, clicking the link suspends our application and takes the user to the website through their internet browser. Upon return to our application, the world will be in the same state that the user left it in (same comments loaded, user in same location etc.).
  
 * User name: Each stick figure has a name tag with their username on it.
 
 * Up/down vote: The upvote count alters the mood of the stick figures. Stick figures with 100 or more upvotes have a smiley face. Stick figures with 1-100 upvotes have a neutral face. Stick figures with less than 1 upvote appear angry. When the user is logged in, they are equipped with a paint gun that allows them to upvote or downvote comments. The paint gun allows the user to paint the stick figure orange or blue. Painting them blue upvotes the comment. Painting them orange downvotes the comment. The user must open the comment before they are able to paint the stick figure.
 
 * Gild: The stick figure sits upon a thrown of gold when the comment is gilded.
 
 * [removed]: When the comment is removed, the stick figure does not have a name tag and instead has a paper bag over their head.
 
 * Flair: The stick figure wears a tee shirt with the flair on the front.

####User Profile Implemenation:

When the user of our application is logged in to Reddit, they have a house representing their user profile. The house exists in the center of the city. The house has one entrance, a living room, a hallway with doors, and an elevator.

 * Name: Users name appears on a mail box outside of the house.
 
 * Post/Comment Karma: There are three different house models to represent Post/Comment Karma. When the user has more than 500,000 combined karma, the house is a mansion. When the user has between 1,000 and 500,000 karma, the house is a normal house. When the user has less than 1,000 karma, the house is a cardboard box.
 
 * Subscribed subreddits: A teleportation device exists in the center of the living room. Stepping inside the teleportation device brings up an option menu containing a list of all subscribed subreddits. Upon choosing a subreddit, the user is teleported outside of the building representing that subreddit.
 
 * Mailbox: A mailbox exists outside of every house to represent the mailbox. Interaction with the mailbox allows the user to view all messages within the mailbox.

 * Trophies: A trophy case in the living room contains all the user's trophies. 
 
 * Posts: Every post the user creates corresponds to a door inside the hallway. Only the first 25 newest posts appear initially. The elevator functions similarly to the elevator within a subreddit and allows the user to view more posts or organize posts differently. 
 
 * Comments: Every comment the user creates corresponds to a picture frame on the wall with that comment inside of it. Only the first 25 comments appear initially. The elevator has an option to load more comments or rank the comments differently. 
