/**GameInfo.cs
* Caleb Whitman
* January 28, 2017
*/

using System.Collections;
using System.Collections.Generic;
using RedditSharp;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

/// <summary>
/// A Singleton that stores commonly used gameObjects and variables.
/// This includes things like the Player, a Reddit object, and server communication code.
/// </summary>
public class GameInfo : MonoBehaviour {

    public static GameInfo instance = null;

    public Reddit reddit {

        get {
            if (reddit == null)
                return getRedditObject();
            else
                return reddit;
        }
    }

	public GameObject player;

    public GameObject menuController;

    public GameObject keyController;

    /// <summary>
    /// If the singleton class has not been created, then it instantiates it.
    /// Otherwise, this does nothing.
    /// </summary>
    public void Awake () 
	{
        
		if (instance == null) 
		{
			DontDestroyOnLoad (gameObject);
			instance = this;
            
		} 
		else if (instance != this) 
		{
			//ensures that only on object of this type is present at all times
			Destroy(gameObject);
		}
	}

    public Reddit getRedditObject()
    {
        return null;
    }

    /// <summary>
    /// Enables or disables the cursor.
    /// </summary>
    /// <param name="cursorLock">If false the cursor will appear. If true the cursor will disappear.</param>
    public void setCursorLock(bool cursorLock)
    {
        MouseLook mouseLook = GameInfo.instance.player.GetComponent<MyRigidbodyFirstPersonController>().mouseLook;
        mouseLook.SetCursorLock(cursorLock);
    }
}
	