/**DoorPopUp.cs
* Caleb Whitman
* February 17, 2017
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**Caleb Whitman
 * calebrwhitman@gmail.com
 * Spring 2017
 */ 

/// <summary>
/// Causes a small door popup to appear when walking up to a door.
/// </summary>
public class DoorPopUp : MonoBehaviour {


	public GameObject doorPopUpPrefab;
	private GameObject doorPopUp;

    void OnTriggerEnter(Collider other)
    {
		doorPopUp = Instantiate (doorPopUpPrefab);
		doorPopUp.transform.SetParent (GameInfo.instance.menuController.GetComponent<MenuController> ().canvas.transform,false);

    }

    void OnTriggerExit(Collider other)
    {
		Destroy (doorPopUp);
    }
}
