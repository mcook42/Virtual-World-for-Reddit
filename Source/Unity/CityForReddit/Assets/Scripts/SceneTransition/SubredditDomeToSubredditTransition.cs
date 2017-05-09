using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

/**Caleb Whitman
 * calebrwhitman@gmail.com
 * Spring 2017
 */ 

/// <summary>
/// Loads the inside of a building from the SubredditDome.
/// </summary>
class SubredditDomeToSubredditTransition : SceneTransition
{



    /// <summary>
    /// Resets the SubredditSceneState, saves the building, and then loads the new scene.
    /// </summary>
    protected override void transferInfo()
    {
        activateLoadingScreen();

        //Saves the building the player is going into.
        saveCurrentBuilding(gameObject.transform.parent.gameObject);
		SubredditDomeState.instance.playerSpawnPoint = WorldState.instance.player.transform.position;
		WorldState.instance.menuController.GetComponent<MenuController> ().clearMenus ();
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
		SubredditDomeState.instance.currentSubreddit = building;
        
    }

}


