using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using RedditSharp.Things;

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

		SubredditDomeState.instance.init("/r/AskReddit");
		SceneManager.LoadScene("SubredditDome");

        GameInfo.instance.player.SetActive(true);
        GameInfo.instance.menuController.SetActive(true);
        //TODO change key state

    }
}

