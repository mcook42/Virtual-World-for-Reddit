using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoUp : MonoBehaviour {

    public GameObject subredditSceneSetup;

    /// <summary>
    /// Loads 25 new Threads.
    /// </summary>
	void OnMouseDown()
    {
        GameInfo.instance.menuController.GetComponent<LoadingPanel>().loadPanel();
        subredditSceneSetup.GetComponent<SubredditSceneSetup>().loadThreads(25,true);
        Debug.Log("Going Up!");
        GameInfo.instance.menuController.GetComponent<LoadingPanel>().unLoadMenu();
    }
}
