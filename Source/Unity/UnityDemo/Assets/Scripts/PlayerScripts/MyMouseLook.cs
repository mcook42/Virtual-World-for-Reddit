/* MyMouseLook.cs
 * Author: Caleb Whitman
 * January 18, 2016
 * 
 * A derived class of MouseLook.cs (in Standard Assets) that allows for a manual rotation of the view point.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class MyMouseLook : MouseLook{

     /** 	 
	 *  Manually rotates the position of the player and camera. 
	 */ 
	public void ManualRotate(Quaternion character)
	{
		m_CharacterTargetRot = character;
	
	}
}
