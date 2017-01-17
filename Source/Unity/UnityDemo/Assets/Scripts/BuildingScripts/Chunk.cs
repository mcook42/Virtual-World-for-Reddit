/**Chunk.cs
 * Author: Caleb Whitman
 * October 29, 2016
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
	public static readonly int buildingsInChunk=1; //The number of actual buildings loaded is (2*buildingsInChunk-1)^2 

	[Range(1,1000)]
	public static readonly int buildingFootprint=9;



	/*
	 * Loads and instantiates the appropriate buildings.
	 * TODO may need to first assign the gameobjects to a scene and load in background
	 */
	public Chunk (int chunk_x, int chunk_y,float parent_x,float parent_z)
	{
		parent = GameObject.CreatePrimitive (PrimitiveType.Cube);
		parent.transform.position = new Vector3 (parent_x, 0, parent_z);
		parent.transform.localScale = new Vector3 (buildingFootprint-2, 1, buildingFootprint-2);
	}

	public GameObject getParent()
	{
		return parent;
	}



}


