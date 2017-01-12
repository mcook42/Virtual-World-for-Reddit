/**WriteTimeLoaded.cs
 * Author: Caleb Whitman
 * Jan 4, 2017
 * 
 * Once the scene is loaded, this writes out how long it took to load.
 */



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WriteTimeLoaded : MonoBehaviour {


	// Use this for initialization
	void Start () {
		Debug.Log ("Time to load scene="+Time.realtimeSinceStartup);

	}
	

}
