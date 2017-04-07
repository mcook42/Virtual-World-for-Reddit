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
		Vector3 buildingPosition = SubredditSceneState.instance.currentSubreddit.transform.localPosition;
		Quaternion buildingRotation = SubredditSceneState.instance.currentSubreddit.transform.localRotation;
		SubredditDomeState.instance.playerSpawnPoint = buildingPosition + buildingRotation * (new Vector3 (SubredditDomeSetup.buildingFootprint, 0, 0));
		SubredditDomeState.instance.playerSpawnPoint = new Vector3 (SubredditDomeState.instance.playerSpawnPoint.x, 1, SubredditDomeState.instance.playerSpawnPoint.z);
		SubredditDomeState.instance.playerSpawnRotation = Quaternion.Euler(new Vector3 (buildingRotation.x, buildingRotation.y+180, buildingRotation.z));


        SubredditSceneState.instance.clear();
        GameInfo.instance.menuController.GetComponent<LocationPanel>().unLoadMenu();
        SceneManager.LoadScene("SubredditDome");

    }
}

