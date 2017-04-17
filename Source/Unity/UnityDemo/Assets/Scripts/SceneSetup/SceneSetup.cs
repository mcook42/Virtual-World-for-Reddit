/**SceneSetup.cs
 * Caleb Whitman
 * January 28, 2017
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// SceneSetup is an abstract class used to define methods which SceneSetup Scripts will use.
/// SceneSetup Scripts are scripts which are responsible for loading new objects into a World Scene and then modifying those objects as the player interacts with the world.
/// </summary>
public abstract class SceneSetUp: MonoBehaviour  {


    /// <summary>
    /// Grabs data from the Server or Reddit.
    /// Instantiates all objects using the data.
    /// Sets the player's state.
    /// Removes the loading screen.
    /// </summary>
    protected void Start()
    {
		setCurrentState ();
        setUpScene();
        setPlayerState();
        deactivateLoadingScreen();
    }

	/// <summary>
	/// Stores the appropriate state for the scene in GameInfo.
	/// </summary>
	protected abstract void setCurrentState();

    /// <summary>
    /// Loads and instantiates all objects required for the scene.
    /// </summary>
    protected abstract void setUpScene();

    /// <summary>
    /// Sets the players state once the scene is loaded.
    /// </summary>
    protected abstract void setPlayerState();

    /// <summary>
    /// Removes the loading screen.
    /// </summary>
    private void deactivateLoadingScreen()
    {
       GameInfo.instance.menuController.GetComponent<MenuController>().unLoadLoadingMenu();

    }


}
