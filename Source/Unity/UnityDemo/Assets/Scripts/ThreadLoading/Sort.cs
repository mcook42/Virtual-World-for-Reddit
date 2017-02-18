/**Sort.cs
* Caleb Whitman
* February 17, 2017
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
/// <summary>
/// Controls the menu to sort the threads.
/// </summary>
public class Sort: MonoBehaviour {

    public GameObject subredditSceneSetup;

    public GameObject sortingPrefab;
    private GameObject sortingCanvas;

    /// <summary>
    /// Creates the panel for the subreddit sort menu.
    /// </summary>
    void Start()
    {
        sortingCanvas = Instantiate(sortingPrefab);
        sortingCanvas.SetActive(false);
        foreach(Button button in sortingCanvas.GetComponentsInChildren<Button>())
        {
            button.onClick.AddListener(()=>onClick(RedditSharp.Things.Sort.New));
        }
    }

    /// <summary>
    /// Activates the panel.
    /// </summary>
	void OnMouseDown()
    {
        sortingCanvas.SetActive(true);

        GameInfo.instance.setCursorLock(false);

    }

    /// <summary>
    /// Unactivates the panel.
    /// </summary>
    /// <param name="sortingMethod"></param>
    void onClick(RedditSharp.Things.Sort sortingMethod)
    {
        sortingCanvas.SetActive(false);
        GameInfo.instance.menuController.GetComponent<LoadingPanel>().loadPanel();
        subredditSceneSetup.GetComponent<SubredditSceneSetup>().loadThreads(sortingMethod);
        Debug.Log("Sorting!");
        MouseLook mouseLook = GameInfo.instance.player.GetComponent<MyRigidbodyFirstPersonController>().mouseLook;
        mouseLook.SetCursorLock(true);
        GameInfo.instance.menuController.GetComponent<LoadingPanel>().unLoadMenu();


    }
}
