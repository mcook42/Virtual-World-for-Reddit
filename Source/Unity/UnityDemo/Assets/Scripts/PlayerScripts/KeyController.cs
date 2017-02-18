/**KeyController.cs
 * Caleb Whitman
 * February 1, 2017
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls key inputs besides the player keys.
/// </summary>
public class KeyController : MonoBehaviour {

	
	void Update () {
		if(Input.GetKeyDown("escape"))
        {
            GameInfo.instance.menuController.GetComponent<PauseMenu>().pause();
        }

        if(Input.GetKeyDown("e"))
        {
            GameInfo.instance.menuController.GetComponent<FatalErrorMenu>().loadMenu("You can't press that");
        }

        if(Input.GetKeyDown("r"))
        {
            GameInfo.instance.menuController.GetComponent<ErrorMenu>().loadMenu("You can't press that");
        }
	}
}
