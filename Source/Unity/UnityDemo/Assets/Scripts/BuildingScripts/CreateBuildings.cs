/**
 * OBSOLETE. 
 * The ChunkLoader and Chunk scripts now take care of buidling generation. This script still exists as a reference, but will eventually be removed.
 * 
 * 
 */



/**CreateBuildings.cs
 * Author: Caleb Whitman
 * Jan 16, 2017
 * 
 * Creates buildings and sets them up around the area.
 * Following tutorial used: https://www.youtube.com/watch?v=xkuniXI3SEE
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System;
using System.Reflection;

[Obsolete("CreateBuildings has been replaced by ChunkLoader.")]
public class CreateBuildings : MonoBehaviour {


	//	Add a chunckLoader class, need to store current chunk position and status

	//Will hold our buildings. 
	public GameObject[] buildings;

	//used to space buildings apart.
	public int buildingFootprint=10;
	public int neighborhoodLengthX = 3;
	public int neighborhoodLengthZ=3;

	// Use this for initialization
	void Awake () {
	

		for (int x = 0; x < 10; x++) {
			for (int z = 0; z < 10; z++) {
				//Creates a new building
				GameObject building;
				if (x > 5) {
					building = Instantiate (buildings [0]);
				}
				else {
					building = Instantiate (buildings [1]);
				}
				//sets the buildings position
				building.transform.position = new Vector3 (x * buildingFootprint, 5, z*buildingFootprint);
				//Altering the name of the cube
				var name = building.transform.Find ("Name").GetComponent<TextMesh> ();
				name.text = "Building: (" + x + "," + z+")";


			}
		}

	}




}

