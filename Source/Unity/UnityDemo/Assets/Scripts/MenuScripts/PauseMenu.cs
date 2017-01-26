/* PauseMenu.cs
 * Author: Caleb Whitman
 * January 20, 2016
 * 
 * A class with functions that can send the player into the pause menu. 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PauseMenu : MonoBehaviour {


	public GameObject pauseMenuCanvas;


	//Make sure the pauseMenuCanvas is not active upon start up.
	void Start () {
		Time.timeScale = 1;

		pauseMenuCanvas.SetActive (false);
	}
	
	//Opens the pause menu when the escape key is pressed.
	void Update () {
		if (Input.GetKeyDown ("escape")) {


			if (Time.timeScale != 0) {
				
				//making the cursorr appear.
				MouseLook mouseLook = GameInfo.info.player.GetComponent<MyRigidbodyFirstPersonController> ().mouseLook;
				mouseLook.SetCursorLock (false);

				//pausing time
				Time.timeScale = 0;

				//displaying the pause menu
				pauseMenuCanvas.SetActive (true);
			}
		}
		
	}

	//Exists the application. This will note work within the editor.
	public void quit(){
		
		Application.Quit ();

	}

	//Resumes the applicaton.
	public void resume()
	{
		//hiding the cursor.
		MouseLook mouseLook = GameInfo.info.player.GetComponent<MyRigidbodyFirstPersonController> ().mouseLook;
		mouseLook.SetCursorLock (true);

		//restarting time
		Time.timeScale = 1;

		//hiding the pause menu
		pauseMenuCanvas.SetActive (false);
	}
}
