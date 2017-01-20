/**SetInsidePlayerPosition.cs
 * Author: Caleb Whitman
 * Jan 13, 2017
 * 
 *Resets the players outside position to the one in gameInfo.
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOutsidePlayerPosition : MonoBehaviour {


	void Awake() {
		GameInfo.info.player.transform.position = GameInfo.info.outsidePlayerPosition+new Vector3(0,0,-2);
		GameInfo.info.resetPlayerPosition ();
	}

}
