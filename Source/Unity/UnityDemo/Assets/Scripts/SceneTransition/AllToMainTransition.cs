/**AllToMainTransition.cs
* Caleb Whitman
* January 31, 2017
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AllToMainTransition : SceneTransition
{

    /// <summary>
    /// Transitions to the main menu
    /// </summary>
    public void loadMainMenu()
    {
        activateLoadingScreen();
        transferInfo();
        SceneManager.LoadScene("MainMenu");
        
        
    }

    protected override void transferInfo()
    {
        
        SubredditSceneState.instance.reset();
        SubredditDomeState.instance.reset();

        GameInfo.instance.menuController.GetComponent<PauseMenu>().resume();
        GameInfo.instance.keyController.SetActive(false);
        GameInfo.instance.menuController.GetComponent<LocationPanel>().unLoadMenu();
    }
}
