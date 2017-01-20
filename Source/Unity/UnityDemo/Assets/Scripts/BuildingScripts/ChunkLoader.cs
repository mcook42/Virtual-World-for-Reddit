/**ChunkLoader.cs
 * Author: Caleb Whitman
 * January 17, 2017
 * 
 * Loads and destroys new chunks based on player movement.
 * 
 * Chunks position is calculated using the center of the chunk. Chunk (0,0) has its center at (0,0).
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ChunkLoader : MonoBehaviour {


	//Center of the chunks is stored in Game Info

	//Number of chunks loaded at a time. The number of actual chunks loaded is (2*chunksLoaded+1)^2 since we load the chunks in a square centered around the player.
	[Range(1,10)]
	public int chunksToLoad; 

	//how far away the player must be from the center chunk until new chunks load.
	[Range(1,5)]
	public float loadingThreshold;



	private int chunksInRow; //number of chunks in a row
	private int chunkLength; //actual lenght of hte chunk in Unity coordinates

	//Size of neighborhoods in the z direction.
	public  readonly int neighborhoodSize=10;



	private Dictionary<Point,Chunk> chunks;


	// Use this for initialization
	void Start () {


		chunksInRow = chunksToLoad * 2 + 1;

		chunks = new Dictionary<Point, Chunk> (new PointComparer());
		chunkLength = Chunk.buildingFootprint * Chunk.buildingsInRow;


		loadChunks ();


	}


	
	/** Checks the players position relative to the center chunk.
	*If the player is loadingThreshold away from the center, then the chunks are shifted.
	*/
	void Update () {

		if (Input.GetKeyDown ("t"))
			teleportToBuilding (new Point (10, 10));

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
		for (int x = -chunksInRow / 2; x <= chunksInRow / 2; x++) {
			for (int z = -chunksInRow / 2; z <= chunksInRow / 2; z++) {
				Point chunkLocation = new Point (GameInfo.info.loadedCenterChunk.x + x, GameInfo.info.loadedCenterChunk.z + z);
				Destroy (chunks [chunkLocation].getParent ());
				chunks.Remove (chunkLocation);
			}
		}

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
	 */ 
	public void teleportToBuilding(Point building)
	{

		destroyChunks ();

		GameInfo.info.loadedCenterChunk.x = GameInfo.info.worldCenterChunk.x = building.x / Chunk.buildingsInRow;
		GameInfo.info.loadedCenterChunk.z=GameInfo.info.worldCenterChunk.z = building.z / Chunk.buildingsInRow;

		loadChunks ();


	}


		
}
	



public class PointComparer: EqualityComparer<Point>
{
	public override bool Equals(Point p1, Point p2)
	{
		return (p1.x == p2.x) && (p1.z == p2.z);
	}

	public override int GetHashCode(Point p)
	{
		int hCode = p.x *7 + p.z; //TODO make this better
		return hCode.GetHashCode();
	}


}