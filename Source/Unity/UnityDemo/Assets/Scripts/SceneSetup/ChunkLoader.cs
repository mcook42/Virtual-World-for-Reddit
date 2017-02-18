/**ChunkLoader.cs
 * Author: Caleb Whitman
 * January 17, 2017
 * 
 * Loads and destroys new chunks based on player movement.
 * A chunk is a collection of buildings/scenary in the game world. 
 * As the player moves around the outside world, new chunks are loaded/unloaded.
 * This ensure that only a handful of buildings have to be loaded at a time.
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChunkLoader : SceneSetUp {


	//Center of the loaded chunks is stored in GameInfo.

	//Number of chunks loaded at a time. The number of actual chunks loaded is (2*chunksLoaded+1)^2 since we load the chunks in a square centered around the player.
	[Range(1,10)]
	public int chunksToLoad; 

	//How many chunks away from the border the player must be until new chunks load.
	[Range(1,5)]
	public float loadingThreshold;


	//number of chunks in a row
	private int chunksInRow; 

	//actual length of the chunk in Unity coordinates
	private int chunkLength; 


	private Dictionary<Point,Chunk> chunks;


    /// <summary>
    ///  Loads the chunks and repositions the player in the world.
    ///If the player has just exited a building, then the chunks around that building location will be loaded.
    ///Otherwise, the player is started at building(0,0).
    /// </summary>
    protected override void setUpScene()
    {
        

        chunksInRow = chunksToLoad * 2 + 1;

        chunks = new Dictionary<Point, Chunk>(new Point());
        chunkLength = Chunk.buildingFootprint * Chunk.buildingsInRow;

        if (SubredditSceneState.instance.currentSubreddit == null)
        {
            teleportToBuilding(new Point(0, 0));
        }
        else
        {
           // teleportToBuilding(SubredditSceneState.instance.currentSubreddit.GetComponent<Subreddit>().position);
        }
    }

    protected override void setPlayerState()
    {
        //TODO
    }



    ///<summary>Checks the players position relative to the center chunk.
    ///If the player is loadingThreshold away from the center, then the chunks are shifted.</summary>
    void Update () {

        
		float positionThreshold = loadingThreshold * chunkLength;
		float x_distance = (GameInfo.instance.player.transform.position.x) - ((OutsideState.instance.loadedCenterChunk.x-OutsideState.instance.worldCenterChunk.x) * chunkLength);
		float z_distance = (GameInfo.instance.player.transform.position.z) - ((OutsideState.instance.loadedCenterChunk.z-OutsideState.instance.worldCenterChunk.z) * chunkLength);


		if (x_distance > positionThreshold) {

			int old_x = OutsideState.instance.loadedCenterChunk.x - chunksInRow / 2;
			OutsideState.instance.loadedCenterChunk.x++;
			int new_x = OutsideState.instance.loadedCenterChunk.x + chunksInRow / 2;

			repositionChunksX(old_x, new_x);


		} else if (x_distance < -positionThreshold) {

			int old_x = OutsideState.instance.loadedCenterChunk.x + chunksInRow / 2;
			OutsideState.instance.loadedCenterChunk.x --;
			int new_x = OutsideState.instance.loadedCenterChunk.x  - chunksInRow / 2;

			repositionChunksX(old_x, new_x);

		}

		if (z_distance > positionThreshold) {
			int old_z = OutsideState.instance.loadedCenterChunk.z  - chunksInRow / 2;
			OutsideState.instance.loadedCenterChunk.z++;
			int new_z = OutsideState.instance.loadedCenterChunk.z + chunksInRow / 2;

			repositionChunksZ(old_z, new_z);

		} else if (z_distance < -positionThreshold) {
			int old_z = OutsideState.instance.loadedCenterChunk.z + chunksInRow / 2;
			OutsideState.instance.loadedCenterChunk.z--;
			int new_z = OutsideState.instance.loadedCenterChunk.z - chunksInRow / 2;

			repositionChunksZ(old_z, new_z);

		}


		
	}

    ///<summary>Loads the chunks in the center of the world.</summary>
    void loadChunks ()
	{
		for (int x = -chunksInRow / 2; x <= chunksInRow / 2; x++) {
			for (int z = -chunksInRow / 2; z <= chunksInRow / 2; z++) {
				Point chunkLocation = new Point (OutsideState.instance.loadedCenterChunk.x + x, OutsideState.instance.loadedCenterChunk.z + z);
				Chunk chunk = new Chunk (chunkLocation.x, chunkLocation.z, (chunkLocation.x - OutsideState.instance.worldCenterChunk.x) * chunkLength, (chunkLocation.z - OutsideState.instance.worldCenterChunk.z) * chunkLength);
				chunks.Add (chunkLocation, chunk);
			}
		}
	}

    ///<summary>Destroys all chunks in the world.</summary>
    void destroyChunks(){

		foreach(KeyValuePair<Point, Chunk> entry in chunks)
		{
			Destroy (entry.Value.getParent());
		}

		chunks.Clear ();

	}


    ///<summary>Delete all chunks with coordinates (old_x, z) and then create new chunks with coordinates (new_x,z).</summary>  
    private void repositionChunksX (int old_x, int new_x)
	{
		//add new chunks
		for (int z = -chunksInRow / 2; z <= chunksInRow / 2; z++) {
			Point location = new Point (new_x, OutsideState.instance.loadedCenterChunk.z + z);
			Chunk chunk = new Chunk (location.x, location.z, (location.x-OutsideState.instance.worldCenterChunk.x) * chunkLength, (location.z-OutsideState.instance.worldCenterChunk.z) * chunkLength);
			chunks.Add (location, chunk);
		}
		//destroy old chunks
		for (int z = -chunksInRow / 2; z <= chunksInRow / 2; z++) {
			Point old_point = new Point (old_x, OutsideState.instance.loadedCenterChunk.z + z);
			Destroy (chunks [old_point].getParent ());
			chunks.Remove (old_point);
		}
	}


	  ///<summary>Delete all chunks with coordinates (x, old_z) and then create new chunks with coordinates (x,new_z).</summary>
	private void repositionChunksZ (int old_z, int new_z)
	{
		//add new chunks
		for (int x = -chunksInRow / 2; x <= chunksInRow / 2; x++) {
			Point location = new Point ( OutsideState.instance.loadedCenterChunk.x + x,new_z);
			Chunk chunk = new Chunk (location.x, location.z, (location.x - OutsideState.instance.worldCenterChunk.x) * chunkLength, (location.z - OutsideState.instance.worldCenterChunk.z) * chunkLength);
			chunks.Add (location, chunk);
		}

		//destroy old chunks
		for (int x = -chunksInRow / 2; x <= chunksInRow / 2; x++) {
			Point old_point = new Point (OutsideState.instance.loadedCenterChunk.x + x,old_z);
			Destroy (chunks [old_point].getParent ());
			chunks.Remove (old_point);
		}
    }

	 ///<summary>Teleports to the building.</summary> 
     ///<param name="building">The location of the building to teleport to.</param>
	public void teleportToBuilding(Point building)
    {

        destroyChunks();

        //Here, rounded up integer division is used. 
        OutsideState.instance.loadedCenterChunk.x = OutsideState.instance.worldCenterChunk.x = (building.x + Chunk.buildingsInRow - 1) / Chunk.buildingsInRow;
        OutsideState.instance.loadedCenterChunk.z = OutsideState.instance.worldCenterChunk.z = (building.z + Chunk.buildingsInRow - 1) / Chunk.buildingsInRow;

        loadChunks();

        Vector3 buildingPosition = new Vector3();
        Quaternion buildingRotation = new Quaternion();


        Subreddit[] buildings;
        buildings = chunks[OutsideState.instance.loadedCenterChunk].getParent().GetComponentsInChildren<Subreddit>();

        /*
        *Note: the current method itterates through the buildings in a chunk in order to find the building position.
        If there are a lot of buildings within a chunk, it may be better to implement a faster method to get buildings 
        (such as using GetChild(i) or creating a Dictionary of buildings in Chunk).
        The current method is advantageous in that it is not dependent on the structure of buildings in Chunk and it only requires
         each buidling to be stored once*/
        foreach (Subreddit build in buildings)
        {
            if (true)//build.position.Equals(building))
            {
                buildingPosition = build.gameObject.transform.position;
                buildingRotation = build.gameObject.transform.rotation;
                break;
            }
        }


        //sets the player one building footprint away, in front of the building

        setPlayerState(buildingPosition, buildingRotation);

    }

    private void setPlayerState(Vector3 buildingPosition, Quaternion buildingRotation)
    {
        Vector3 newPlayerPosition = ((new Vector3((float)Math.Cos(-buildingRotation.eulerAngles.y), GameInfo.instance.player.transform.position.y, (float)Math.Sin(-buildingRotation.eulerAngles.y))) * (Chunk.buildingFootprint / 2)) + buildingPosition;

        newPlayerPosition.y = GameInfo.instance.player.transform.position.y;



        GameInfo.instance.player.transform.position = newPlayerPosition;
    }


}
	


