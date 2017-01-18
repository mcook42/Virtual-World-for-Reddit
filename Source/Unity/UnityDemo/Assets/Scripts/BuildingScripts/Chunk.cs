/**Chunk.cs
 * Author: Caleb Whitman
 * January 17, 2017
 * 
 * Represents a single chunk in the world.
 */



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Chunk 
{

	//the parent object for all gameobjects within the chunk.
	private GameObject parent=null;

	[Range(1,100)]
	public static readonly int buildingsInRow=3; //The number of actual buildings loaded is buildingsInChunk^2 

	[Range(1,1000)]
	public static readonly int buildingFootprint=10;



	/*
	 * Loads and instantiates the appropriate buildings.
	 * TODO may need to first assign the gameobjects to a scene and load in background
	 */
	public Chunk (int chunkX, int chunkZ,float parentX,float parentZ)
	{
		

		parent = new GameObject();
		parent.transform.position = new Vector3 (parentX, 0, parentZ);

		int halfRow=buildingsInRow/2;
		int centerBuildingX = chunkX * buildingsInRow;
		int centerBuildingZ = chunkZ * buildingsInRow;
		for (int x = 0; x < buildingsInRow; x++) {
			for (int z = 0; z < buildingsInRow; z++) {

			
				GameObject building = instantiateBuilding (centerBuildingX + x - halfRow, centerBuildingZ + z - halfRow);
				building.transform.parent = parent.transform;
				building.transform.localPosition = new Vector3 (buildingFootprint*x, 5, buildingFootprint*z);
			}
			

		}

	}

	/*
	 * Gets the building at position x,z.
	 * In the future this will make a call to the server.
	 */ 
	GameObject instantiateBuilding(int x, int z)
	{
		
		UnityEngine.Object prefab= AssetDatabase.LoadAssetAtPath ("Assets/Prefabs/Buildings/BlueBuilding.prefab", typeof(GameObject));
		GameObject building = GameObject.Instantiate(prefab) as GameObject;
		var name = building.transform.Find ("Name").GetComponent<TextMesh> ();
		name.text = "Building: (" + x + "," + z+")";

		return building;


	}

	/*
	 *  Returns the parent of objects withint the scene.
	 */ 
	public GameObject getParent()
	{
		return parent;
	}



}


