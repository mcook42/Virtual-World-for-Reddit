/**Caleb Whitman
 * calebrwhitman@gmail.com
 * Spring 2017
 */ 

using UnityEngine;
using System.Collections;

/// <summary>
/// Every time a new scene is loaded, all previous scenes and the objects in them are distroyed.
/// Any object with this script attached will never be distroyed upon a scene transition.
/// </summary>
public class DontDestroyOnLoad : MonoBehaviour {

	// This will always be called, even if the script is not enabled.
	// This is called right before Start().
	void Awake()
	{
		//sets up the property so that the oject is not destroyed onload. 
		DontDestroyOnLoad (gameObject);



	}
}
