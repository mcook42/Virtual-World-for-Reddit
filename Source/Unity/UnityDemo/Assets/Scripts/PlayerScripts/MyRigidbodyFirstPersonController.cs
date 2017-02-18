/* MyRigidbodyFirstPersonController.cs
 * Author: Caleb Whitman
 * January 18, 2016
 * 
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;


/// <summary>
/// A derived class of RigidbodyFirstPersonController.cs (located in the Standard Assets folder) that allows for a manual rotation of the viewpoint.
/// </summary>
public class MyRigidbodyFirstPersonController : RigidbodyFirstPersonController {

	public MyMouseLook myMouseLook=new MyMouseLook();

	protected override void Start()
	{
		mouseLook = myMouseLook;
		base.Start ();

	}

    /// <summary>
    /// If the player has just left a building, then this method resets the view so that is is looking away from the buildings door. Otherwise, it calls the parents RotateView method.
    /// </summary>
    protected override void RotateView()
	{
        base.RotateView();
        /*
		if (GameInfo.info.inToOutTransition) {
			GameInfo.info.inToOutTransition = false;
			RotateViewManual (GameInfo.info.currentBuilding.transform.localRotation);
		} else {
			base.RotateView ();
		}
        */

	}

    /// <summary>
    /// Manaually rotates the view point.
    /// </summary>
    /// <param name="playerRot">The new rotation of the player.</param>
    private void RotateViewManual(Quaternion playerRot)
	{
		myMouseLook.ManualRotate (playerRot);
	}


}
