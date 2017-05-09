using System;
using UnityEngine;
using RedditSharp.Things;

/**Caleb Whitman
 * calebrwhitman@gmail.com
 * Spring 2017
 */ 

/// <summary>
/// Represents Created Things. 
/// </summary>
public abstract class CreatedInfo : MonoBehaviour
{
	//Thing we are represenintg
	protected CreatedThing thing { get; set; }

	//text fields
	public GameObject upvotes;

	/// <summary>
	/// Returns the DateTime as a nicely formated string of the form: time hours/minutes ago.
	/// </summary>
	/// <returns>The time created.</returns>
	/// <param name="time">Time.</param>
	public string getTimeCreated(DateTime time)
	{
		var minutes = (int)(System.DateTime.UtcNow-time).TotalMinutes;
		if (minutes >= 60) {
			string s = ((int)(minutes / 60))==1 ? "" : "s";
			return ((int)(minutes / 60)) + " hour"+s+" ago";
		} else {
			string s = (minutes == 1) ? "" : "s";
			return minutes + " minute"+s+" ago";
		}
			
	}
}


