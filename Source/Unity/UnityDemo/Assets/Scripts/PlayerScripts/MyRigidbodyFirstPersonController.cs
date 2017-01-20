/* MyRigidbodyFirstPersonController.cs
 * Author: Caleb Whitman
 * January 18, 2016
 * 
 * A derived class of RigidbodyFirstPersonController.cs (located in the Standard Assets folder) that allows for a manual rotation of the viewpoint.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;



public class MyRigidbodyFirstPersonController : RigidbodyFirstPersonController {

	public MyMouseLook myMouseLook=new MyMouseLook();

	protected override void Start()
	{
		mouseLook = myMouseLook;
		base.Start ();

	}

	//If the player has just left a building, then this method resets the view so that is is looking away from the buildings door.
	//Otherwise, it calls the parents RotateView method.
	protected override void RotateView()
	{
		if (GameInfo.info.inToOutTransition) {
			GameInfo.info.inToOutTransition = false;
			RotateViewManual (GameInfo.info.currentBuilding.buildingRotation);
		} else {
			base.RotateView ();
		}

	}

	/* Manaually rotates the view point.
	*/
	private void RotateViewManual(Quaternion playerRot)
	{
		myMouseLook.ManualRotate (playerRot);
	}


}
