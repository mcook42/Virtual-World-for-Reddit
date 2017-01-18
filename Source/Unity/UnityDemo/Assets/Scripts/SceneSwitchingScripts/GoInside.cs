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

		//Remembers the name of the building
		GameInfo.info.currentSubreddit=gameObject.transform.parent.transform.Find("Name").GetComponent<TextMesh>().text;

		//Load the Inside Scenes. All other scenes are automatically deleted
		SceneManager.LoadScene ("Inside");


		//Saves the player's position outside.
		//This is used to when the player goes back outside.
		GameObject player = GameInfo.info.player;
		GameInfo.info.outsidePlayerPosition= player.transform.position-(new Vector3(0,0,2));


	}
}
