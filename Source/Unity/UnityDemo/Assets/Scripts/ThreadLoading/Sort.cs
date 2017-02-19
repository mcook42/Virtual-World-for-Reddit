
/**Sort.cs
* Caleb Whitman
* February 17, 2017
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.EventSystems;


/// <summary>
/// Controls the menu to sort the threads.
/// </summary>
public class Sort: Menu<Sort> {

    public GameObject subredditSceneSetup;

    /// <summary>
    /// Unactivates the panel.
    /// </summary>
    /// <param name="sortingMethod"></param>
    void onClick(RedditSharp.Things.Sort sortingMethod)
    {
        
        GameInfo.instance.menuController.GetComponent<LoadingPanel>().loadPanel();
        unLoadMenu();
        subredditSceneSetup.GetComponent<SubredditSceneSetup>().loadThreads(sortingMethod);
        Debug.Log("Sorting!");
        MouseLook mouseLook = GameInfo.instance.player.GetComponent<MyRigidbodyFirstPersonController>().mouseLook;
        mouseLook.SetCursorLock(true);
        GameInfo.instance.menuController.GetComponent<LoadingPanel>().unLoadMenu();


    }

    /// <summary>
    /// Activates the panel.
    /// </summary>
    public void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        loadMenu(true);
        var sortMethods = Enum.GetValues(typeof(RedditSharp.Things.Sort));
        var buttons = instance.GetComponentsInChildren<Button>();


        for (int i = 0; i < 4; i++)
        {
            RedditSharp.Things.Sort sort = (RedditSharp.Things.Sort)sortMethods.GetValue(i);
            buttons[i].onClick.AddListener(() => onClick(sort));
        }
    }


}
