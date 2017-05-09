/**Caleb Whitman
 * calebrwhitman@gmail.com
 * Spring 2017
 */ 
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

/// <summary>
/// Instantiates the main menu.
/// </summary>
public class MainMenuSetup : SceneSetUp {


	public GameObject mainMenuPrefab;


    /// <summary>
    /// Unactivates the player.
    /// </summary>
    protected override void setPlayerState()
    {
        WorldState.instance.player.SetActive(false);
    }

    /// <summary>
    /// Loads the mainMenu.
    /// </summary>
    protected override void setUpScene()
    {

		Instantiate (mainMenuPrefab);
    }

}
