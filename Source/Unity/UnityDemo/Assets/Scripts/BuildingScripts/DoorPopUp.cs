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
public class DoorPopUp : Menu<DoorPopUp> {



    void OnTriggerEnter(Collider other)
    {
        loadPanel();
    }

    void OnTriggerExit(Collider other)
    {
        unLoadMenu();
    }
}
