/**errorMenu.cs
* Caleb Whitman
* February 17, 2017
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A simple text menu with a single button to resume the game. Meant for small, nonfatal errors.
/// </summary>
public class ErrorMenu : Menu<ErrorMenu>{

    /// <summary>
    /// Loads the menu with the error message.
    /// </summary>
    /// <param name="error">The error message.</param>
    public void loadMenu(string error)
    {
        base.loadMenu(false);
        instance.transform.Find("Text").GetComponent<Text>().text = error;

    }

    /// <summary>
    /// Unloads the menu.
    /// </summary>
    public void resume()
    {
        unLoadMenu();
    }
}
