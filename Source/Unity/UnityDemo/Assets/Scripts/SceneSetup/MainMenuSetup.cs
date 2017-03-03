/**MainMenuSetup.cs
* Caleb Whitman
* February 17, 2017
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


    /// <summary>
    /// Unactivates the player.
    /// </summary>
    protected override void setPlayerState()
    {
        GameInfo.instance.player.SetActive(false);
    }

    /// <summary>
    /// Loads the mainMenu.
    /// </summary>
    protected override void setUpScene()
    {

        GameInfo.instance.menuController.GetComponent<MainMenu>().loadMenu(false);
    }

}
