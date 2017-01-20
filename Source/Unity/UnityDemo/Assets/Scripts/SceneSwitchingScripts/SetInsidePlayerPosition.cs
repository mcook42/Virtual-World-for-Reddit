/**SetInsidePlayerPosition.cs
 * Author: Caleb Whitman
 * Jan 13, 2017
 * 
 *Sets the player position to the correct spot inside the room.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInsidePlayerPosition : MonoBehaviour {


	void Awake() {
		GameInfo.info.player.transform.position = new Vector3 (0, 1, 0);
	}
	

}
