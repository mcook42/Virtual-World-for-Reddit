using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

class MainToSubredditDomeTransition : SceneTransition
{
    public GameObject buildingPrefab=null;

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
        SubredditDomeState.instance.centerBuilding = Instantiate(buildingPrefab) as GameObject;
        DontDestroyOnLoad(SubredditDomeState.instance.centerBuilding);

        //give building values using the Building script
        var buildingAttributes = SubredditDomeState.instance.centerBuilding.GetComponent<Subreddit>();
        buildingAttributes.subredditId = "";
        buildingAttributes.subredditName = "center";

        //set the name on the front of the building
        var name = SubredditDomeState.instance.centerBuilding.transform.Find("Name").GetComponent<TextMesh>();
        name.text = buildingAttributes.subredditName;

        GameInfo.instance.player.SetActive(true);
        GameInfo.instance.menuController.SetActive(true);
        GameInfo.instance.keyController.SetActive(true);

    }
}

