/**Caleb Whitman
 * calebrwhitman@gmail.com
 * Spring 2017
 */ 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// The main menu for the game.
/// </summary>
class MainMenu : LogInButtonObserver
{
	public GameObject creditsPrefab=null;

    /// <summary>
    /// Starts the game.
    /// TODO: spawn player in different locations based on whether or not they are logged in.
    /// </summary>
    public void play()
    {
		
        GameInfo.instance.setCursorLock(true);
		var transition = GetComponent<MainToSubredditDomeTransition> ();
		transition.clickPlay ();
		Destroy (gameObject);
    }

	/// <summary>
	/// Opens the credits menu.
	/// </summary>
	public void credits()
	{
		Instantiate (creditsPrefab);

	}


}

