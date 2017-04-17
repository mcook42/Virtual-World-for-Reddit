using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using RedditSharp.Things;
using System.Security.Authentication;

class SubredditDometoSubredditDomeTransition : SceneTransition
{


    public void goToDome(string newCenter)
    {
		activateLoadingScreen ();
		if (SubredditDomeState.instance.init (newCenter)) {
			transferInfo ();
			SceneManager.LoadScene ("SubredditDome");
		}
		else {
			GameInfo.instance.menuController.GetComponent<MenuController> ().unLoadLoadingMenu ();
			GameInfo.instance.menuController.GetComponent<MenuController> ().loadErrorMenu("Could not load subreddit");
		}
    }

	public void goToFront()
	{
		activateLoadingScreen ();
		SubredditDomeState.instance.initFront ();
		transferInfo ();
		SceneManager.LoadScene ("SubredditDome");

	}

	public void goToAll()
	{
		activateLoadingScreen ();
		SubredditDomeState.instance.initAll ();
		transferInfo ();
		SceneManager.LoadScene ("SubredditDome");

	}

	public void goToHouse()
	{
		activateLoadingScreen ();
		SubredditDomeState.instance.initHouse ();
		transferInfo ();
		SceneManager.LoadScene ("SubredditDome");

	}

	protected override void transferInfo()
	{
		clearCurrentState ();
	}


}
