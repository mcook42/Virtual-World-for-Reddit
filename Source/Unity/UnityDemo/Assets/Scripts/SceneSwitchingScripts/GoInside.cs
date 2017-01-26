/**GoInside.cs
 * Author: Caleb Whitman
 * January 17, 2016
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



		//Saves the building the player is going into
		GameInfo.info.saveCurrentBuilding (gameObject.transform.parent.gameObject);


		//Load the Inside Scenes. All other scenes are automatically deleted
		SceneManager.LoadScene ("Inside");


	}
}
