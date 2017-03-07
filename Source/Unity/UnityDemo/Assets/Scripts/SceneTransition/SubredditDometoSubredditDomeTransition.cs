using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using RedditSharp.Things;

class SubredditDometoSubredditDomeTransition : SceneTransition
{
    string newCenter;
    public void goToDome(string newCenter)
    {
        this.newCenter = newCenter;
        transferInfo();
    }

    protected override void transferInfo()
    {
        activateLoadingScreen();
		if (SubredditDomeState.instance.loadBuildings (newCenter))
			SceneManager.LoadScene ("SubredditDome");
		else {
			GameInfo.instance.menuController.GetComponent<LoadingPanel> ().unLoadMenu ();
			GameInfo.instance.menuController.GetComponent<ErrorMenu> ().loadMenu ("Could not load subreddit");
		}
		


    }
}
