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


    //This will be called whenever something collides with this object.
    void OnMouseDown()
    {
        activateLoadingScreen();
        transferInfo();

    }

    protected override void transferInfo()
    {
        SubredditSceneState.instance.clear();
       // var name = GameInfo.instance.locationText.GetComponent<UnityEngine.UI.Text>();
       // name.text = "";

        SceneManager.LoadScene("Outside");

    }
}
