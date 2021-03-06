﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using RedditSharp.Things;

/**Caleb Whitman
 * calebrwhitman@gmail.com
 * Spring 2017
 */ 

/// <summary>
/// Loads the SubredditDome from the main menu.
/// </summary>
class MainToSubredditDomeTransition : SceneTransition
{

    public void clickPlay()
    {
        transferInfo();
    }

    /// <summary>
    /// Transfers the player to the outsideWorld. This involves activating some objects in GameInfo.
    /// </summary>
    protected override void transferInfo()
    {
        activateLoadingScreen();

		SubredditDomeState.instance.initFront ();
		SceneManager.LoadScene("SubredditDome");

        WorldState.instance.player.SetActive(true);
        WorldState.instance.menuController.SetActive(true);
    }
}

