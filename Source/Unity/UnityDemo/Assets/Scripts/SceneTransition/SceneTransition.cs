/**SceneTransition.cs
 * Caleb Whitman
 * January 28, 2017
 * 
 */

 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// SceneTransistion classes handle the transition of information between scenes.
/// These scripts are responsible for detecting when a player enters a new scene,
/// opening up a loading screen,
/// unpopulating the old scene,
/// and transfering any needed information to the next scene.
/// </summary>
public abstract class SceneTransition : MonoBehaviour
{

    public void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
            transferInfo();
    }

    /// <summary>
    /// Puts the player into a loading screen.
    /// </summary>
    protected void activateLoadingScreen()
    {
        GameInfo.instance.menuController.GetComponent<LoadingPanel>().loadPanel();
    }

	/// <summary>
	/// Calls the clear method on the SceneState in gameInfo.
	/// </summary>
	protected void clearCurrentState()
	{
		if (GameInfo.instance.currentState != null)
			GameInfo.instance.currentState.clear ();
	}

    /// <summary>
    /// Transfers information from one scene to the next.
    /// </summary>
	protected abstract void transferInfo();

    
   

}

