/**GoInside.cs
 * Author: Caleb Whitman
 * October 29, 2016
 * 
 * This script is applied to the door prefab on the house.
 * 
 * It will take you from the ouside to the inside.
 */

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoInside : MonoBehaviour {


	//This will be called whenever something collides with this object.
	void OnTriggerEnter(Collider other) {
		

			//Load the Inside Scenes. All other scenes are automatically deleted
			SceneManager.LoadScene ("Inside");


		//Find the player object.
		GameObject player = GameObject.Find("Player");
		PlayerController playerControl = (PlayerController) player.GetComponent(typeof(PlayerController));

		//Saves the player's position outside.
		//This is used to when the player goes back outside.
		playerControl.SetOutsidePosition (player.transform.position-(new Vector3(0,0,2)));

		//Reset the player to the center of the map.
		GameObject.Find ("Player").transform.position = new Vector3 (0, 1.5f, 0);

	}
}
