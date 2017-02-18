/**OutsideToSubredditTransition.cs
* Caleb Whitman
* January 28, 2017
*/ 


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the transition from the Outside Scene to the Subreddit Building scene.
/// Put on the door of a building.
/// </summary>
public class OutsideToSubredditTransition : SceneTransition {

    //This will be called whenever something collides with this object.
    void OnMouseDown()
    {
        activateLoadingScreen();
        transferInfo();
        

    }

    /// <summary>
    /// Resets the SubredditSceneState, saves the building, and then loads the new scene.
    /// </summary>
    protected override void transferInfo()
    {
       
        activateLoadingScreen();

        //Saves the building the player is going into.
        saveCurrentBuilding(gameObject.transform.parent.gameObject);

        SceneManager.LoadScene("SubredditBuilding");


    }

 
    /// <summary>
    /// Saves the building that the player went inside.
    /// </summary>
    /// <param name="building">The building to be saved.</param>
    public void saveCurrentBuilding(GameObject building)
    {
        //here we have to seperate the building from the chunk parent.
        building.transform.parent = null;

        
        building.SetActive(false);

        DontDestroyOnLoad(building);
        SubredditSceneState.instance.init(building);
    }


}
