using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

/**Caleb Whitman
 * calebrwhitman@gmail.com
 * Spring 2017
 */ 

/// <summary>
/// Loads the SubredditDome from the Subreddit.
/// </summary>
class SubredditToSubredditDomeTransition: SceneTransition
{

    protected override void transferInfo()
    {
        activateLoadingScreen();
		Quaternion buildingRotation = SubredditDomeState.instance.currentSubreddit.transform.localRotation;
		SubredditDomeState.instance.playerSpawnRotation = Quaternion.Euler(new Vector3 (buildingRotation.x, buildingRotation.y+180, buildingRotation.z));


		SubredditDomeState.instance.currentSubreddit = null;
        SceneManager.LoadScene("SubredditDome");

    }
}

