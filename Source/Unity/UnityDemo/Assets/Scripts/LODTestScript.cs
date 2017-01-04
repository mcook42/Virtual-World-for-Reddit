/**GoInside.cs
 * Author: Caleb Whitman
 * Jan 3, 2017
 * 
 * This script is used to mess with the LOD on the LODTest object.
 * I followed this tutorial: https://www.youtube.com/watch?v=IzlU_xvTK3Y
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LODTestScript : MonoBehaviour {

	public float[] ranges; //distances where one LOD is switched out to another
	public GameObject[] models; //The LOD models

	private int currentLevel=0; //the how far we are from the object

	// Use this for initialization
	void Start () {

		foreach(GameObject model in models)
		{
			//turn off each model
			model.SetActive (false);

		}
	}
	
	// Update is called once per frame
	void Update () {
		//get distance between LODTest and camera
		var distance = Vector3.Distance (Camera.main.transform.position, transform.position);
		var level = ranges.Length-1;

		//determining if we need to chance models
		for(int i=0;i < ranges.Length;i++) {
			if (distance < ranges[i]) {
				level = i;
				break;
			}
		}
			
		if (currentLevel != level) {
			//turning off current model
			models [currentLevel].SetActive (false);
			//turning on next model
			currentLevel = level;
			models [currentLevel].SetActive (true);
		}
	}
}
