#Notes.md

This document contains various notes related to how the Unity project is organized.

First, all of the information related to this project is either contained within a scene, a folder, or a script.

###Folders

 - Fonts: Contains fonts.
 
 - Materials: All objects have an attached material which defines things like color and texture.
 
 - Textures: Contains textures.
 
 - Physics Materials: Physics materials are used to specify physics attributes. For example, friction can be specified in a physics material.
 
 - Plugins: All outside code goes here. Right now this just contains Redditsharp.
 
 - Scenes: Contains scenes.
 
 - Scripts: Contains scripts.
 
 - Prefabs: Predefined objects, like a building.
 
 - Buildings: The buildings we are using.
 
 - Rune Assets: A sample of some free buildings. Used for testing.
 
 - Streets: A sample of some free streets. Used for testing.

###Scenes

 All objects, except the ones in main, are completely destroyed on a scene change.
 
 - Main contains all objects that exist in all scenes. For example, the camera always exists. This objects have a DontDestroyOnLoad script which keeps them freom being destroyed on a scene change.
 
 - Outside contains all outside ojects, like buildings.
 
 - Inside contains all inside objects, like rooms inside buildings.
 
 - DontDestroyOnLoad is a special scenes which exists only while the game is running. It contains all of the objects within main.
 
 
###Random Notes:

####Storing data between scenes
 
 - First do, PlayerPrefs.set[Int/Float/String]("name","object"); then can do PlayerPrefs.get[Int/Float/String]("name); This is not secure. Only use for non-important data.

 - Create an object with the DontDestroyOnLoad script. Put all values within it that we want to keep between scenes. Right now everything within Main has this script on it.
 
####Finding objects

 - There are a variety of GetComponent methods to find and get objects. 
 
  - Overall, the best stategy seems to be to NOT get data from other objects. Unity is not very well designed for communicating between objects. Especially ones in different scenes.
  
  - The camera in any scene can be accessed using the variable camera.
  
####Creating/Loading Large Worlds

 - LOD (level of detail, located as LOD group under Rendering under Add Component) describes how much detail is rendered based on distance. As you move farther away from an object, you can make the object become less detailed. This increases performance since Unity will not need to render every single detail when the player is far away. You need to create multiple, similar objects for each level of detail.
 
 - LOD can be set through scripts or through an interactive editor. The scripts give you more control.
 
 - We will need to load the buildings in chunks. In other words, the entire city will not be loaded at once. Still working on this.
