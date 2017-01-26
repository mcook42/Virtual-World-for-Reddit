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
using UnityEditor;
using System;

public class ChunkLoader : MonoBehaviour {


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


	/**
	 * Loads the chunks and repositions the player in the world.
	 * If the player has just exited a building, then the chunks around that building location will be loaded.
	 * Otherwise, the player is started at building (0,0).
	 */ 
	void Start () {


		chunksInRow = chunksToLoad * 2 + 1;

		chunks = new Dictionary<Point, Chunk> (new Point());
		chunkLength = Chunk.buildingFootprint * Chunk.buildingsInRow;

		if (GameInfo.info.currentBuilding == null) {
			teleportToBuilding (new Point (0, 0));
		} else {
			teleportToBuilding (GameInfo.info.currentBuilding.GetComponent<Building> ().position);
		}


	}


	
	/** Checks the players position relative to the center chunk.
	*If the player is loadingThreshold away from the center, then the chunks are shifted.
	*/
	void Update () {


		float positionThreshold = loadingThreshold * chunkLength;
		float x_distance = (GameInfo.info.player.transform.position.x) - ((GameInfo.info.loadedCenterChunk.x-GameInfo.info.worldCenterChunk.x) * chunkLength);
		float z_distance = (GameInfo.info.player.transform.position.z) - ((GameInfo.info.loadedCenterChunk.z-GameInfo.info.worldCenterChunk.z) * chunkLength);


		if (x_distance > positionThreshold) {

			int old_x = GameInfo.info.loadedCenterChunk.x - chunksInRow / 2;
			GameInfo.info.loadedCenterChunk.x++;
			int new_x = GameInfo.info.loadedCenterChunk.x + chunksInRow / 2;

			repositionChunksX(old_x, new_x);


		} else if (x_distance < -positionThreshold) {

			int old_x = GameInfo.info.loadedCenterChunk.x + chunksInRow / 2;
			GameInfo.info.loadedCenterChunk.x --;
			int new_x = GameInfo.info.loadedCenterChunk.x  - chunksInRow / 2;

			repositionChunksX(old_x, new_x);

		}

		if (z_distance > positionThreshold) {
			int old_z = GameInfo.info.loadedCenterChunk.z  - chunksInRow / 2;
			GameInfo.info.loadedCenterChunk.z++;
			int new_z = GameInfo.info.loadedCenterChunk.z + chunksInRow / 2;

			repositionChunksZ(old_z, new_z);

		} else if (z_distance < -positionThreshold) {
			int old_z = GameInfo.info.loadedCenterChunk.z + chunksInRow / 2;
			GameInfo.info.loadedCenterChunk.z--;
			int new_z = GameInfo.info.loadedCenterChunk.z - chunksInRow / 2;

			repositionChunksZ(old_z, new_z);

		}


		
	}

	/* Loads the chunks in the center of the world.
	 * The values loadedCenterChunk and worldCenterChunk in GameInfo are used.
	 */ 
	void loadChunks ()
	{
		for (int x = -chunksInRow / 2; x <= chunksInRow / 2; x++) {
			for (int z = -chunksInRow / 2; z <= chunksInRow / 2; z++) {
				Point chunkLocation = new Point (GameInfo.info.loadedCenterChunk.x + x, GameInfo.info.loadedCenterChunk.z + z);
				Chunk chunk = new Chunk (chunkLocation.x, chunkLocation.z, (chunkLocation.x - GameInfo.info.worldCenterChunk.x) * chunkLength, (chunkLocation.z - GameInfo.info.worldCenterChunk.z) * chunkLength);
				chunks.Add (chunkLocation, chunk);
			}
		}
	}

	/* Destroys all chunks in the world.
	 */ 
	void destroyChunks(){

		foreach(KeyValuePair<Point, Chunk> entry in chunks)
		{
			Destroy (entry.Value.getParent());
		}

		chunks.Clear ();

	}

	/**
	 * 
	 *  Delete all chunks with coordinates (old_x, z) and then create new chunks with coordinates (new_x,z).
	 */ 
	private void repositionChunksX (int old_x, int new_x)
	{
		//add new chunks
		for (int z = -chunksInRow / 2; z <= chunksInRow / 2; z++) {
			Point location = new Point (new_x, GameInfo.info.loadedCenterChunk.z + z);
			Chunk chunk = new Chunk (location.x, location.z, (location.x-GameInfo.info.worldCenterChunk.x) * chunkLength, (location.z-GameInfo.info.worldCenterChunk.z) * chunkLength);
			chunks.Add (location, chunk);
		}
		//destroy old chunks
		for (int z = -chunksInRow / 2; z <= chunksInRow / 2; z++) {
			Point old_point = new Point (old_x, GameInfo.info.loadedCenterChunk.z + z);
			Destroy (chunks [old_point].getParent ());
			chunks.Remove (old_point);
		}
	}

	/**
	 * 
	 *  Delete all chunks with coordinates (x, old_z) and then create new chunks with coordinates (x,new_z).
	 */ 
	private void repositionChunksZ (int old_z, int new_z)
	{
		//add new chunks
		for (int x = -chunksInRow / 2; x <= chunksInRow / 2; x++) {
			Point location = new Point ( GameInfo.info.loadedCenterChunk.x + x,new_z);
			Chunk chunk = new Chunk (location.x, location.z, (location.x - GameInfo.info.worldCenterChunk.x) * chunkLength, (location.z - GameInfo.info.worldCenterChunk.z) * chunkLength);
			chunks.Add (location, chunk);
		}

		//destroy old chunks
		for (int x = -chunksInRow / 2; x <= chunksInRow / 2; x++) {
			Point old_point = new Point (GameInfo.info.loadedCenterChunk.x + x,old_z);
			Destroy (chunks [old_point].getParent ());
			chunks.Remove (old_point);
		}
	}

	/*
	 * Teleports to the building.
	 * 
	 * Note: the current method itterates through the buildings in a chunk in order to find the building position.
	 * If there are a lot of buildings within a chunk, it may be better to implement a faster method to get buildings 
	 * (such as using GetChild(i) or creating a Dictionary of buildings in Chunk).
	 * The current method is advantageous in that it is not dependent on the structure of buildings in Chunk and it only requires
	 * each buidling to be stored once.
	 */ 
	public void teleportToBuilding(Point building)
	{
		
		destroyChunks ();

		//Here, rounded up integer division is used. 
		GameInfo.info.loadedCenterChunk.x = GameInfo.info.worldCenterChunk.x = (building.x+Chunk.buildingsInRow-1) / Chunk.buildingsInRow;
		GameInfo.info.loadedCenterChunk.z=GameInfo.info.worldCenterChunk.z = (building.z+Chunk.buildingsInRow-1) / Chunk.buildingsInRow;
		Debug.Log (GameInfo.info.loadedCenterChunk.x+","+GameInfo.info.loadedCenterChunk.z);
		loadChunks ();

		Vector3 buildingPosition=new Vector3();
		Quaternion buildingRotation=new Quaternion();


		Building[] buildings;
		buildings=chunks [GameInfo.info.loadedCenterChunk].getParent ().GetComponentsInChildren<Building> ();

		foreach(Building build in buildings)
		{
			if (build.position.Equals (building)) {
				buildingPosition = build.gameObject.transform.position;
				buildingRotation = build.gameObject.transform.rotation;
			}
		}

	
		//sets the player one building footprint away, in front of the building

		Vector3 newPlayerPosition = ((new Vector3( (float) Math.Cos(-buildingRotation.eulerAngles.y),GameInfo.info.player.transform.position.y, (float)Math.Sin(-buildingRotation.eulerAngles.y)))*(Chunk.buildingFootprint/2))+buildingPosition;

		newPlayerPosition.y = GameInfo.info.player.transform.position.y;


	
		GameInfo.info.player.transform.position = newPlayerPosition;

	}


		
}
	


