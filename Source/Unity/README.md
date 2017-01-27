# UnityDemo
This project is a test/documenation of the Unity features we will need for our project.

Currently this project looks at Unities ability to:

- move a player and camera

- generate objects dynamically

- switch scenes

- create text that is blocked by other objects

- Load many objects at once. 

- Occlusion culling.

This demo will be updated as we figure out how Unity/Our Server/Reddit API work.

The notes document contains a more specific description of the various pieces within the project.

###Things to work on:

- lighting (By this, I mean fully figure it out. The basics are easy but sometimes things mess up for seemingly no reason)

- interacting with objects

- first person viewpoint

- teleportation devices

- viewpoints in general. Making it so that the camera does not get stuck behind walls

- create more scenes
	
	- inside house
	
	- inside thread

- tests to determine how large of a world can be created at once and how much Unity handles for us

- Pull up the Reddit login page.

- scripts to parse Reddit JSON documents and then turn them into world objects

- scripts to communicate back and forth with the server

- script to create world using the categorization method we discussed

	- first, use the categorization method to create the map
	
	- next, store this map either in Unity or on the server
	
	- finally, create a buidling for each Subreddit, using the correct model

- import objects from Maya


###Issues:

- Player falls through floor when rapidly switching scenes.




