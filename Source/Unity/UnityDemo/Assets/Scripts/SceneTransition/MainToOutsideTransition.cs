/**MainToOutsideTransition.cs
* Caleb Whitman
* January 31, 2017
*/


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainToOutsideTransition : SceneTransition
{


    public void clickPlay()
    {
        activateLoadingScreen();
        SceneManager.LoadScene("Outside");
        transferInfo();
        
    }

    /// <summary>
    /// Transfers the player to the outsideWorld. This involves activating some objects in GameInfo.
    /// </summary>
    protected override void transferInfo()
    {

        GameInfo.instance.player.SetActive(true);
        GameInfo.instance.menuController.SetActive(true);
        GameInfo.instance.keyController.SetActive(true);
    }
}
