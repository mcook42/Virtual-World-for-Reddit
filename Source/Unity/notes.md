#Notes.md

This document contains various notes related to how the Unity project is organized.

First, all of the information related to this project is either contained within a scene, a folder, or a script.

###Folders

 - Fonts: Contains fonts.
 
 - Materials: All objects have an attached material which defines things like color and texture.
 
 - Textures: Contains textures.
 
 - Physics Materials: Physics materials are used to specify physics attributes. For example, friction can be specified in a physics material.
 
 - Plugins: All outside code goes here. 
 
 - Scenes: Contains scenes.
 
 - Scripts: Contains scripts.
 
 - Prefabs: Predefined objects, like a building.
 
 - Buildings: The buildings we are using.
 
 - LoadTest: A sample of scripts and prefabs used to test how long it takes unity to load a certain number of objects.

###Scenes

 All objects, except the ones in main, are completely destroyed on a scene change.
 
 - Main contains all objects that exist in all scenes. For example, the camera always exists. These objects have a DontDestroyOnLoad script which keeps them freom being destroyed on a scene change.
 
 - Outside contains all outside ojects, like buildings.
 
 - Inside contains the room inside the building.
 
 - House contains the house scene.
 
 - LoadTest: A blank testing scene only used to run load tests.
 
 - DontDestroyOnLoad is a special scene which exists only while the game is running. It contains all of the objects within main.
 
###Menus

 All Menus inherit from the Menu<T> object and are managed by the menuController object. The typical process for creating a menu is to get the menuController object, 
 get the appropriate script on that object, and then calling loadMenu(bool pause). Unloaded the menu follows the same process except one called unLoadeMenu() in the end.
 
 
###Random Notes:

These are my own personal notes about certain Unity features. I post these notes on the chance that they will benefit others.

####Lighting

 - Lightmaps turn the lighting information into object textures. This lowers computation costs at run time. This only works for static objects.

####Text

 - In order to great non gui text, you have to create a custom font. The default fonts are always drawn on the front of the view. 

####Storing data between scenes
 
 - First do, PlayerPrefs.set[Int/Float/String]("name","object"); then can do PlayerPrefs.get[Int/Float/String]("name); This is not secure. Only use for non-important data.

 - Create an object with the DontDestroyOnLoad script. Put all values within it that we want to keep between scenes. Right now everything within Main has this script on it.
 
 - Store everything in static variables.
 
####Finding objects

 - There are a variety of GetComponent methods to find and get objects. 
 
  - Overall, the best stategy seems to be to NOT get data from other objects. Unity is not very well designed for communicating between objects. Especially ones in different scenes.
  
  - The camera in any scene can be accessed using the variable camera.
  
  - Objects can be given tags. Then, all objects with that tag can be found using GameObject.FindGameObjectWithTag
  
  - One person online suggests loading the most commonly used prefabs into a singleton so that they are easy to find.
  
####Creating/Loading Large Worlds

 - We will have to load the city in pieces.
   
   - The city will be divided up into chunks. Each chunk will be loaded as needed. 

 - LOD (level of detail, located as LOD group under Rendering under Add Component) describes how much detail is rendered based on distance. As you move farther away from an object, you can make the object become less detailed. This increases performance since Unity will not need to render every single detail when the player is far away. You need to create multiple, similar objects for each level of detail.
 
 - LOD can be set through scripts or through an interactive editor. The scripts give you more control.
 
 - Occulsion culling only draws objects that are in the view of the camera. It will not draw objects that are behind other objects.
 
    - Occulsion culling divides the world into cells. Most objects should ideally be around the size of the cells or only slightly larger. It you don't create any occulsion areas, the occulsion will be applied to the entire scene. If occulsion areas are created, occulsion is only applied to those areas.
   
    - Objects can be marked as either occludees or occulers. Occludees are objects that are expected to be blocked by other objects and occulers are the objects doing the blocking. Many objects are both.
   
    - Occulsion areas must be created for moving objects.
   
    - Occulsion baking should be set to PVS only for our application. This only occules static objects and is used when there are a small number of dynamic objects.
  
    - Occulsion portals are used on doors and can be dynamically open and shut depending on whether or not the door is open or shut.
   
    - Camera view distance will influence occulsion distance. Camera distance can be changed after occulsion baking
   
 - Resources.Load loads resources from the resources folder. Might use this to load in the buildings.
 
 - Frustum culling draws everything within the camera's view, even if it is obscured by an other object.
 
 
