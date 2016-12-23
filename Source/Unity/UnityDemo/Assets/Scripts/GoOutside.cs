/**GoOutside.cs
 * Author: Caleb Whitman
 * Dec 23, 2016
 * 
 * This script is applied to the door prefab on the house.
 * 
 * It will take you from the ouside to the inside.
 */
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoOutside : MonoBehaviour {


	//This will be called whenever something collides with this object.
	void OnTriggerEnter(Collider other) {

		GameInfo.info.currentSubreddit = "";

		//Load the OUside Scene. All other scenes are automatically deleted
		SceneManager.LoadScene ("Outside");


		//Find the player
		GameObject player = GameObject.Find("Player");
		PlayerController playerControl = (PlayerController) player.GetComponent(typeof(PlayerController));

		//Set the player's position so that they are outside of the house they entered.
		playerControl.resetOutsidePosition ();



	}
}
