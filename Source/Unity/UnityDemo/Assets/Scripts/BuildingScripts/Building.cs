/**Building.cs
 * Author: Caleb Whitman
 * January 17, 2017
 * 
 * A script attached to every building gameObject.
 * This object holds all of the relevent building information.
 * 
 * The information can be accessed by calling GetComponent<Building>() on the building gameObject this script is attached to.
 */



using System;
using UnityEngine;


public class Building: MonoBehaviour
{
	 	
	public string subredditId;
	public string subredditName;

	public Point position;

}


