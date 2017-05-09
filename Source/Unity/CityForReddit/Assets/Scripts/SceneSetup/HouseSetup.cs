using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**Caleb Whitman
 * calebrwhitman@gmail.com
 * Spring 2017
 */ 

/// <summary>
/// Loads the inside of the house.
/// </summary>
public class HouseSetup :SceneSetUp {
	

	/// <summary>
	/// Loads and instantiates all objects required for the scene.
	/// </summary>
	protected override void setUpScene(){


	}

	/// <summary>
	/// Sets the players state once the scene is loaded.
	/// </summary>
	protected override void setPlayerState(){

		WorldState.instance.player.transform.position = new Vector3 (22, -3, 0);
	}

}
