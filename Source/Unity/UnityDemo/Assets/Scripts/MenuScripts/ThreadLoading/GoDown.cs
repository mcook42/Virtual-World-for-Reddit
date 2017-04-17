/**GoDown.cs
* Caleb Whitman
* February 17, 2017
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Causes the previous 25 threads to load.
/// </summary>
public class GoDown : MonoBehaviour {

    public GameObject SubredditSceneSetup;

    /// <summary>
    /// Loads the previous 25 threads.
    /// </summary>
    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        GameInfo.instance.menuController.GetComponent<MenuController>().loadLoadingMenu();
        SubredditSceneSetup.GetComponent<SubredditSceneSetup>().loadThreads(-25, true);
        Debug.Log("Going Down!");
        GameInfo.instance.menuController.GetComponent<MenuController>().unLoadLoadingMenu();
    }
}
