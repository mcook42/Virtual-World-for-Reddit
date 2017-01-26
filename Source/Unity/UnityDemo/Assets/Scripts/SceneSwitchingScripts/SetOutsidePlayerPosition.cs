/**SetInsidePlayerPosition.cs
 * Author: Caleb Whitman
 * Jan 13, 2017
 * 
 * Resets the players outside position to the one in gameInfo.
 * Currently ChunkLoader takes care of most of the positioning in its Start function.
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOutsidePlayerPosition : MonoBehaviour {


	void Start() {

		GameInfo.info.resetPlayerPosition ();
	}

}
