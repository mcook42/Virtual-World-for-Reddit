/**DoorPopUp.cs
* Caleb Whitman
* February 17, 2017
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Causes a small door popup to appear when walking up to a door.
/// </summary>
public class DoorPopUp : MonoBehaviour {

    public GameObject popUpPrefab;

    private GameObject popUpInstance=null;

    void OnTriggerEnter(Collider other)
    {
        popUpInstance = Instantiate(popUpPrefab);
    }

    void OnTriggerExit(Collider other)
    {
        Destroy(popUpInstance);
    }
}
