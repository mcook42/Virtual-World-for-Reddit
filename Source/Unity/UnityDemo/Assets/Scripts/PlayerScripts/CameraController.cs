/**CameraController.cs
 * Author: Caleb Whitman
 * October 29, 2016
 * 
 * Lets the camera follow the character while keeping it stable.
 */

using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	//Holds the player game object.
	//Since this is public, it is visible in Unity and can be modified in Unity.
	//We initialize this field in Unity.
	public GameObject player;

	//Where the camera is in relation to the player.
	private Vector3 offset;

	//Called upon initialization.
	void Start(){

		//Find the offect we set when we set up the scene.
		offset = transform.position - player.transform.position;

	}

	//The last update to be called. Called after all physics calculations are done.
	void LateUpdate()
	{
		//Make sure the camera is still in the same position relative to the player.
		transform.position = player.transform.position + offset;

	}

}
