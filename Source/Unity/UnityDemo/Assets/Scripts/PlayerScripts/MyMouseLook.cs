/**Caleb Whitman
 * calebrwhitman@gmail.com
 * Spring 2017
 */ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

/// <summary>
/// A derived class of MouseLook.cs (in Standard Assets) that allows for a manual rotation of the view point.
/// </summary>
public class MyMouseLook : MouseLook{


    ///<summary>Manually rotates the position of the player and camera. </summary>
    ///<param name="character">The new position of the player.</param>
    public void ManualRotate(Quaternion character)
	{
		m_CharacterTargetRot = character;
	
	}
}
