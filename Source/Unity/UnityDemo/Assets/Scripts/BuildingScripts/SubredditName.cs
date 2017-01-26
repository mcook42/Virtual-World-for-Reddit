/**SubredditName.cs
 * Author: Caleb Whitman
 * Dec 23, 2016
 * 
 * Initializes the Subreddit name that appears at the top of the screen.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubredditName : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var name = GetComponent<Text> ();
		name.text=GameInfo.info.currentBuilding.GetComponent<Building>().subredditName;
	}
	

}
