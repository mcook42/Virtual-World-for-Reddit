using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using RedditSharp.Things;
using System.Security.Authentication;

/**Caleb Whitman
 * calebrwhitman@gmail.com
 * Spring 2017
 */ 

/// <summary>
/// Handles traveling to a new subreddit dome.
/// </summary>
class SubredditDometoSubredditDomeTransition : SceneTransition
{

	/// <summary>
	/// Travels to the new subreddit dome.
	/// </summary>
	/// <param name="newCenter">New center.</param>
    public void goToDome(string newCenter)
    {

		//clear the door pop up menu.
		GameInfo.instance.menuController.GetComponent<MenuController> ().clearMenus ();
		activateLoadingScreen ();

		try{
			if (SubredditDomeState.instance.init (newCenter)) {
				transferInfo ();
				SceneManager.LoadScene ("SubredditDome");
			}
			else {
				GameInfo.instance.menuController.GetComponent<MenuController> ().unLoadLoadingMenu ();
				GameInfo.instance.menuController.GetComponent<MenuController> ().loadErrorMenu("Could not load subreddit");
			}
		}
		catch(ServerDownException s) {
			
			GameInfo.instance.menuController.GetComponent<MenuController> ().unLoadLoadingMenu ();
			GameInfo.instance.menuController.GetComponent<MenuController> ().loadFatalErrorMenu(s.Message);

		}
    }

	/// <summary>
	/// Travel to the front subreddit
	/// </summary>
	public void goToFront()
	{
		activateLoadingScreen ();
		SubredditDomeState.instance.initFront ();
		transferInfo ();
		SceneManager.LoadScene ("SubredditDome");

	}

	/// <summary>
	/// Travel to /r/all
	/// </summary>
	public void goToAll()
	{
		activateLoadingScreen ();
		SubredditDomeState.instance.initAll ();
		transferInfo ();
		SceneManager.LoadScene ("SubredditDome");

	}

	/// <summary>
	/// Travel to the house if the player is logged in.
	/// </summary>
	public void goToHouse()
	{
		activateLoadingScreen ();
		SubredditDomeState.instance.initHouse ();
		transferInfo ();
		SceneManager.LoadScene ("SubredditDome");

	}

	/// <summary>
	/// Clears the state of this scene.
	/// </summary>
	protected override void transferInfo()
	{
		SubredditDomeState.instance.loadNew = true;
		clearCurrentState ();
	}


}
