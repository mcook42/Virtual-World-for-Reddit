# UnityProject

This is the Unity Project for 3D World for Reddit. The project is currently under development.
 
##Project Overview
 
###Scenes

 All scenes have a SceneSetup object which controls the intantiation and management of the scene. SceneSetup objects frequently communicate with the game state classes.
 
 - Main: The main menu.
 
 - SubredditDome: Buildings organized in a circle pattern. Center building can be a normal subreddit, a default subreddit (Front Page, /r/all) or the house.
 
 - Subreddit: Inside of a building.
 
 - House: Inside of the house.
 
###Scene Transitions

 Scene transition scripts handle the transitions from one scene to another. The general layout of each script is 1. transfer required information into the GameState. 2. Load the new scene.
 The scripts are named based on the transitions they handle. A script with the name Scene1ToScene2 will handle transitions from scene 1 to scene 2.
 A script with the name ToScene2 will handle all transitions to scene 2, from any other scene.
 
###Reddit

 All Reddit functionality comes through a version of a third party Reddit library called RedditSharp. Most actions are handled by a single Reddit object. The RedditRetriever class contains methods to access and manipulate this object. Located in the WorldState folder.
 
###Server:

 Server actions are controlled through a simple server class. Located in the WorldState folder.
 
###Game State

 - WorldState: Stores and keeps track of the overall state of the application. This includes things like the player, map, and Reddit object.
 
 - SubredditDomeState: Stores values related to the player's location in the SubredditDome. Also contains methods to manipulate the map whenever a new Subreddit Dome is loaded.
 
###Menus

 All menus are panels with scripts that inherit from the Menu object. 
 A MenuController object keeps track of the number of menus loaded in order to handle enabling/disabling the player movement whenever a menu is loaded.
 In addition, the MenuController controls the instantiation of some common menus, such as the pause menu.



