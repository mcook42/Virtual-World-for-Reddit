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

	public static GameInfo info;



	public Vector3 outsidePlayerPosition;
	public GameObject player;

	//records whether or not we are transitioning from the inside of a building to the outside.
	//if we are, then the script FirstPersonController will reset the players rotation to the rotation stored in currentBuilding.
	public bool inToOutTransition;

	public CurrentBuilding currentBuilding =new CurrentBuilding();


	//The center of the loaded chunks
	public Point loadedCenterChunk;

	//The chunk that lies at Unity coordinate position (0,0)
	public Point worldCenterChunk;


	//TODO add position for thread room.


	//called first thing no matter what
	void Awake () 
	{
		if (info == null) 
		{
			DontDestroyOnLoad (gameObject);
			info = this;
			outsidePlayerPosition=new Vector3(0,1,-4);
			loadedCenterChunk = new Point (1, 1);
			worldCenterChunk = new Point (1, 1);
			inToOutTransition = false;
	
		} 
		else if (info != this) 
		{
			//ensures that only on object of this type is present at all times
			Destroy(gameObject);
		}
	}



	/*
	 * Records the players position in the outside world.
	 * The buidlingRotation is rotated by 180 degrees in the Y direction before being saved.
	 */ 
	public void savePlayerPosition(Quaternion buildingRotation, Point buildingPosition, string subredditId, string subredditName)
	{

		Vector3 rot = buildingRotation.eulerAngles;
		rot = new Vector3(rot.x,rot.y+180,rot.z);
		this.currentBuilding.buildingRotation = Quaternion.Euler(rot);
		this.currentBuilding.subredditId = subredditId;
		this.currentBuilding.buildingPosition = buildingPosition;
		this.currentBuilding.subredditName = subredditName;

	}

	/* Resets the player's position to that of the outside world.
	 */ 
	public void resetPlayerPosition()
	{
		
		player.transform.rotation = new Quaternion(currentBuilding.buildingRotation.x,currentBuilding.buildingRotation.y,currentBuilding.buildingRotation.z,currentBuilding.buildingRotation.w);
		inToOutTransition = true;
	

	}


}

public class CurrentBuilding{
	public string subredditId;
	public string subredditName;
	public Quaternion buildingRotation;
	public Point buildingPosition;

	public CurrentBuilding()
	{
		subredditId = "";
		subredditName = "";
		buildingPosition = new Point (0, 0);
		buildingRotation = Quaternion.Euler (new Vector3 (0, 0, 0));

	}

};