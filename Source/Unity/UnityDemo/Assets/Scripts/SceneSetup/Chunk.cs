/**Chunk.cs
 * Author: Caleb Whitman
 * January 17, 2017
 * 
 */



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// A chunk is a collection of buildings/scenary in the game world. 
/// As the player moves around the outside world, new chunks are loaded/unloaded.
/// </summary>
public class Chunk 
{

	//the parent object for all buildings/scenery within the chunk.
	private GameObject parent=null;

	//A chunk loads a square of buildings. The number of buildings within a chunk is buildingsInRow^2.
	[Range(1,100)]
	public static readonly int buildingsInRow=2; 

	//The building footprint is the size of the building lot, in Unity's length unit.
	[Range(1,1000)]
	public static readonly int buildingFootprint=20;


	private Point chunkLocation;

    /// <summary>
    /// Loads and instantiates the appropriate buildings.
    /// TODO may need to first assign the gameobjects to a scene and load in background
    /// </summary>
    /// <param name="chunkX">The X coordinate of the Chunk determined by ChunkLoader.</param>
    /// <param name="chunkZ">The Z coordinate of the Chunk determined by ChunkLoader.</param>
    /// <param name="parentX">The Unity X coordinate position of the location of the center of the chunk.</param>
    /// <param name="parentZ">The Unity Z coordinate position of the location of the center of the chunk.</param>
    public Chunk (int chunkX, int chunkZ,float parentX,float parentZ)
	{


		chunkLocation = new Point (chunkX, chunkZ);

		parent = new GameObject();
		parent.transform.position = new Vector3 (parentX, 0, parentZ);


		instantiateBuildings (parent);

	}


    /// <summary>
    /// Instantiates all the buildings in a square from (startX,endX) to (startZ, endZ)
    /// In the future this will make a call to the server.
    /// </summary>
    /// <param name="parent">The chunk the buildings are attached to.</param>
    /// <returns>The same parent passed in as a parameter.</returns>
    GameObject instantiateBuildings(GameObject parent)
	{
		int halfRow = buildingsInRow / 2;
		int centerBuildingX = chunkLocation.x * buildingsInRow;
		int centerBuildingZ = chunkLocation.z * buildingsInRow;

		//****TODO: In the future, this loop will make a call to the server in order to get the appropriate buildings.
		for (int x = 0; x < buildingsInRow; x++) {
			for (int z = 0; z < buildingsInRow; z++) {

				int buildingX = (centerBuildingX + x - halfRow);
				int buildingZ = centerBuildingZ + z - halfRow;

				GameObject building = GameObject.Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube)) as GameObject;

				//give building values using the Building script
				var buildingAttributes = building.GetComponent<Subreddit> (); 
				buildingAttributes.subredditId = "";
				//buildingAttributes.position = new Point (buildingX, buildingZ);
				buildingAttributes.subredditName = "Building: (" + buildingX + "," + buildingZ + ")";

				//set the name on the front of the building
				var name = building.transform.Find ("Name").GetComponent<TextMesh> ();
				name.text = buildingAttributes.subredditName;



				building.transform.parent = parent.transform;
				building.transform.localPosition = new Vector3 (buildingFootprint * x, 5, buildingFootprint * z);
			}
		}

		return parent;

	}

    /// <summary>
    /// Returns the parent gameobject for all of the buildings.
    /// </summary>
    /// <returns>The parent gameObject for all buildings in a Chunk.</returns>
    public GameObject getParent()
	{
		return parent;
	}



}


