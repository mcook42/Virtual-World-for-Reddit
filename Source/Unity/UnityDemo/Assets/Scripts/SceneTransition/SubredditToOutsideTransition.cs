/* SubredditToOutsideTransition.cs
 * Author: Caleb Whitman
 * January 30, 2017
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubredditToOutsideTransition : SceneTransition {




    protected override void transferInfo()
    {
        activateLoadingScreen();
        SubredditSceneState.instance.clear();
       // var name = GameInfo.instance.locationText.GetComponent<UnityEngine.UI.Text>();
       // name.text = "";

        SceneManager.LoadScene("Outside");

    }
}
