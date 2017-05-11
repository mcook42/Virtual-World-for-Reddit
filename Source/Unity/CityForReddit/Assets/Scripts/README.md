
## Script Overview

### SceneSetup

 All scenes have a SceneSetup object which controls the intantiation and management of the scene. SceneSetup objects frequently communicate with the game state classes. Four scenes currently exist in the project.
 
 - Main: The main menu.
 
 - SubredditDome: Buildings organized in a circle pattern. Center building can be a normal subreddit, a default subreddit (Front Page, /r/all) or the house.
 
 - Subreddit: Inside of a building.
 
 - House: Inside of the house.
 
### Scene Transitions

 Scene transition scripts handle the transitions from one scene to another. The general layout of each script is, 1. Transfer required information into the GameState, 2. Load the new scene.
 The scripts are named based on the transitions they handle. A script with the name Scene1ToScene2 will handle transitions from scene 1 to scene 2.
 A script with the name ToScene2 will handle all transitions to scene 2, from any other scene.
 
 ### Game State

 - WorldState: Stores and keeps track of the overall state of the application. This includes things like the player, map, and Reddit object.
 
 - SubredditDomeState: Stores values related to the player's location in the SubredditDome. Also contains methods to manipulate the map whenever a new Subreddit Dome is loaded.
 
### Building Scripts

Reddit information is attached to the post and building objects. Simple scripts hold this information for each object.

### Reddit

 All Reddit functionality comes through a modified version of a third party Reddit library called RedditSharp. RedditSharp is stored as a dll in the Plugins folder. Most actions are handled by a single Reddit object. The RedditRetriever class contains methods to access and manipulate this object. LoginObservable and LoginObserver are interfaces that create an observer pattern that is used to change minor aspects of the scene whenever a user logs in or out. Located in the GameState folder.
 
### Server:

 Server actions are controlled through a simple server class. Located in the WorldState folder.
 
### Map

The currently loaded subreddits are stored in a simple graph of nodes and edges. All scripts are stored in the MapGraph folder.
 
### Menus

 All menus are panels with scripts that inherit from the Menu abstract class. 
 
 Menus in the Comments folder closely follow Reddit's comment and post structure.
 Reddit has comments, posts, and subreddits. Comments and posts are "votable things" since a user can vote on both of these items.
 All votable things are "created things", since they were created.
 Everything single Reddit object, including votable things and subreddits, are "things".
 The comment menu system follows this general structure. PostCommentMenu and ThingListMenu are two classes used to get and organzing the Reddit information. 
 
 A MenuController class keeps track of the number of menus loaded in order to handle enabling/disabling the player movement whenever a menu is loaded.
 In addition, the MenuController controls the instantiation of some common menus, such as the pause menu.

### Player

Most of the player's functionality comes through Unity's FirstPersonController script. However, this script lacked some functionality (such as disabling the keyboard input if we desire to keep the player stationary). Two scripts, MyFirstPersonController and MyMouseLook, extend the Unity scripts to give this functionality.

### Editor

Most scripts here are simple tests that were created at one point to test some feature. One script exists to invert normals on a selected object.
