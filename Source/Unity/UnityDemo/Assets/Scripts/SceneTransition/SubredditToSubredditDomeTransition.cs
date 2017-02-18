using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

class SubredditToSubredditDomeTransition: SceneTransition
{
    //This will be called whenever something collides with this object.
    void OnMouseDown()
    {
        activateLoadingScreen();
        transferInfo();
        SceneManager.LoadScene("SubredditDome");

    }

    protected override void transferInfo()
    {
        SubredditSceneState.instance.clear();
        SubredditDomeState.instance.activateCenterBuilding();
        GameInfo.instance.menuController.GetComponent<LocationPanel>().unLoadMenu();

        

    }
}

