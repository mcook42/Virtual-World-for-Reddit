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
        SceneManager.LoadScene("SubredditDome");

		List<String> subreddit = new List<String> ();
		subreddit.Add ("/r/askscience");

		Subreddit center = GameInfo.instance.server.getSubreddits(subreddit)[0];
        SubredditDomeState.instance.loadBuildings(center);

        GameInfo.instance.player.SetActive(true);
        GameInfo.instance.menuController.SetActive(true);
        GameInfo.instance.keyController.SetActive(true);

    }
}

