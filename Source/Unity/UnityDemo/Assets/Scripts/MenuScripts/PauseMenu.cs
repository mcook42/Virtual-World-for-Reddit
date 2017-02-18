/**PauseMenu.cs
* Caleb Whitman
* February 17, 2017
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A menu to pause the game.
/// </summary>
public class PauseMenu : Menu<PauseMenu> {



    /// <summary>
    /// Resumes the applicaton.
    /// </summary>
    public void resume()
    {
        unLoadMenu();

    }

    /// <summary>
    /// Pauses the game.
    /// </summary>
    public void pause()
    {
        loadMenu(true);
    }
}
