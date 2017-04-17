using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

class SubredditToSubredditDomeTransition: SceneTransition
{

    protected override void transferInfo()
    {
        activateLoadingScreen();
		Quaternion buildingRotation = SubredditSceneState.instance.currentSubreddit.transform.localRotation;
		SubredditDomeState.instance.playerSpawnRotation = Quaternion.Euler(new Vector3 (buildingRotation.x, buildingRotation.y+180, buildingRotation.z));


        SubredditSceneState.instance.clear();
        SceneManager.LoadScene("SubredditDome");

    }
}

