using System;
using UnityEngine;
using RedditSharp.Things;

/// <summary>
/// Represents Created Things. 
/// </summary>
public abstract class CreatedInfo : MonoBehaviour
{
	//Thing we are represenintg
	protected CreatedThing thing { get; set; }

	//text fields
	public GameObject author;
	public GameObject time;
	public GameObject upvotes;

}


