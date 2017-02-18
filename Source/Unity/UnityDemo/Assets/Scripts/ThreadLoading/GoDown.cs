﻿/**GoDown.cs
* Caleb Whitman
* February 17, 2017
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Causes the previous 25 threads to load.
/// </summary>
public class GoDown : MonoBehaviour {

    public GameObject subredditSceneSetup;

    /// <summary>
    /// Loads the previous 25 threads.
    /// </summary>
    void OnMouseDown()
    {
        GameInfo.instance.menuController.GetComponent<LoadingPanel>().loadPanel();
        subredditSceneSetup.GetComponent<SubredditSceneSetup>().loadThreads(-25, true);
        Debug.Log("Going Down!");
        GameInfo.instance.menuController.GetComponent<LoadingPanel>().unLoadMenu();
    }
}
