/* GameInfo.cs
 * Author: Caleb Whitman
 * January 18, 2016
 * 
 * A singleton class that stores the player's position in the world (building position and rotation, chunk location, and subreddit Id), the player gameObject, 
 * and subreddit/thread information.
 * This information is used to transfer data between scenes and sucessfully restore a player's position after they exit a building.
 * 
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour {

	public static GameInfo info=null;


	public GameObject player;

	//records whether or not we are transitioning from the inside of a building to the outside.
	//if we are, then the script FirstPersonController will reset the players rotation to the rotation stored in currentBuilding.
	public bool inToOutTransition;

	//The building the player enters. This is null when the player is in the outside world.
	public GameObject currentBuilding=null;

	//The center of the loaded chunks
	public Point loadedCenterChunk;

	//The chunk that lies at Unity coordinate position (0,0)
	public Point worldCenterChunk;


	//TODO add position for thread room.


	/*If the singleton class has not been created, then it instantiates it.
	 * Otherwise, this does nothing.
	*/ 
	public void Awake () 
	{
		if (info == null) 
		{
			DontDestroyOnLoad (gameObject);
			info = this;
			loadedCenterChunk = new Point (0, 0);
			worldCenterChunk = new Point (0, 0);
			inToOutTransition = false;
	
		} 
		else if (info != this) 
		{
			//ensures that only on object of this type is present at all times
			Destroy(gameObject);
		}
	}




	/*
	 * Saves the building that the player went inside.
	 */ 
	public void saveCurrentBuilding(GameObject building)
	{
		//here we have to seperate the building from the chunk parent.
		building.transform.parent = null;

		if (currentBuilding != null)
			Destroy (currentBuilding);
		
		currentBuilding = building;
		currentBuilding.SetActive (false);

		DontDestroyOnLoad (currentBuilding);
	}

	/* Resets the player's position to that of the outside world.
	 * Currently does nothing. In the future will be implmented to set up the players rotatation upon exiting a building.
	 */ 
	public void resetPlayerPosition()
	{
		
	}


}
	