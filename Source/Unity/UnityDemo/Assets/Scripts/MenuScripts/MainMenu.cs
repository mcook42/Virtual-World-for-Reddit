/**MainMenu.cs
* Caleb Whitman
* February 17, 2017
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// The main menu for the game.
/// </summary>
class MainMenu : Menu<MainMenu>
{
  
    /// <summary>
    /// Starts the game.
    /// TODO: spawn player in different locations based on whether or not they are logged in.
    /// </summary>
    public void play()
    {
        unLoadMenu();
        GameInfo.instance.setCursorLock(true);
        GameInfo.instance.menuController.GetComponent<MainToSubredditDomeTransition>().clickPlay();
    }

    /// <summary>
    /// TODO
    /// </summary>
    public void login()
    {

    }
}

