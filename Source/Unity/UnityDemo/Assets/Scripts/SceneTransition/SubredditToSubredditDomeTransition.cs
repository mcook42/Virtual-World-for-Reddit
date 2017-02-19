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
        SubredditSceneState.instance.clear();
        SubredditDomeState.instance.activateCenterBuilding();
        GameInfo.instance.menuController.GetComponent<LocationPanel>().unLoadMenu();
        SceneManager.LoadScene("SubredditDome");



    }
}

