# UnityDemo

This project is a prototype of the final Unity project.

Currently this project looks at Unities ability to:

- Move a player and camera.

- Generate objects dynamically.

- Switch scenes.

- Create text that is blocked by other objects.

- Load many objects at once.

- Generate buildings.

- Occlusion culling.

- Connect to Reddit with RedditSharp (Currently in progress. To access this features use the menu item RedditSharp.)

###Things to Do:

####Back End Scripts:

 - Scripts to communicate back and forth with Reddit.
 
   - Get 25 new/hot/etc thread titles, upvotes etc from a subreddit. Also need to be able to get the next 25 threads of that sorting. 
   
   - Get 25 new/hot/etc comments from within a thread. Also need to be able to get next 25 comments of that storting. TODO: Figure out what comments we want to get at once.
   
   - Submit new comments.
   
   - Upvote comments.
   
   - Get comment's parent.
   
   - Get comment's children.
   
 - Scripts to communicate back and forth with the server.
 
   - Access server's database and get map and subreddit information.
   
   - Access server's database and get user information.
   
####Scenes:

 - Inside Subreddit Building.
 
   - Threads will simply a list of gameobjects doors with thread scripts to hold the information.
 
   - Upon entering a call will be made to Reddit. The player will remain in a loading screen until the threads are loaded. If loading takes too long, player will be notified to try again.
 
   - Calls backend script to get threads for that subreddit. 
   
   - Doors to threads will save thread information and then load the comment room scene.
   
   - New doors can be added if a logged in player submits a new thread. There will need to be the ability to type up the thread and then enter it. 
   
   - If the thread is an image, then the image will also be loaded and be displayed in some sort of object.
   
   - Exiting the building will go to the outside scene.
   
   - Teleporting with the map will go to the outside scene.
   
   - Elevator loads new threads.
   
      - Elevator exists within the scene, they are just boxes closed off from the main scene.
	  
	  - Elevator door will open upon interaction and close once player presses a button to a "floor".
	  
	  - Upon hitting a button, the elevator door will close and keep player trapped until the new threads have been loaded.
	  
	  - Information desk will allow the player to access the wiki.
	  
 - Inside Thread:
 
     - Comment gameobjects will hold all of their information. The comments structure will be seperate from the comment gameobjects, and will exist as comment ids in a tree structure.
 
     - Upon entering a call will be made to Reddit. The player will remain in a loading screen until comments are loaded. If loading takes too long, player will be notified to try again.
	 
	 - If the comment includes an image. Then the image will be loaded. If image takes too long to load, then just the link will be loaded.
	 
	 - Arbitrary amounts of parent and children comments should be able to be loaded by somehow interacting with a commenter.
	 
	 - New comments can be added. This will require the user to be able to type and then submit a comment.
	 
	 - Need a function to take a tree structure of comments and then determine where the comments will be located.
	 
	 - Exiting the room will bring back the subreddit building and thread. 
	 
 - Inside User House: 
 
    - Teleportation device that brings up a menu to travel to a selected subreddit.
	
	- Some sort of option to view things like saved comments or other things.
	 
 - Outside Scene

   - Complete map.
   
   - Modify world generation to include a house and change when a player logs in/out.
   
   - Modify building generation function to more easily include neighborhood designs and larger buildings.
   
####Overall game info:

 - Need a user class to hold the user info. Only active if the user if logged in.
 
 - Will need a thread class to hold what thread the player is located inside.
 
 - Pull up the Reddit login page (will need to access the browser).
 
####Overall things to do:

 - Plan for errors and create actions for when the errors occur.
 
 - Learn how to import objects from Maya.
 
 - Learn how to use the Unity Profiler. 

###Issues:

- Player rotation does not seem to be correctly oriented with building orientation. ( (0,0,0) for player orientation does not correspond to (0,0,0) for building orientation).




