/**Caleb Whitman
 * calebrwhitman@gmail.com
 * Spring 2017
 */ 

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Loads the main menu.
/// </summary>
public class ToMainTransition : SceneTransition
{

    /// <summary>
    /// Transitions to the main menu
    /// </summary>
    public void loadMainMenu()
    {
        activateLoadingScreen();
        transferInfo();
        SceneManager.LoadScene("MainMenu");
        
        
    }

	/// <summary>
	/// Resets all scenes.
	/// </summary>
    protected override void transferInfo()
    {
		WorldState.instance.menuController.GetComponent<MenuController> ().clearMenus ();
        SubredditDomeState.instance.reset();
    }
}
