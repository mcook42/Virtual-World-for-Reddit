/**fatalErrorMenu.cs
* Caleb Whitman
* February 17, 2017
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A text menu that gives the user the option to quit or return to the main menu. Meant for fatal errors.
/// </summary>
public class FatalErrorMenu : Menu<FatalErrorMenu> {

    /// <summary>
    /// Loads the menu with the error message.
    /// </summary>
    /// <param name="error">The error message.</param>
    public void loadMenu(string error)
    {
        base.loadMenu(true);
       FatalErrorMenu.instance.transform.Find("Text").GetComponent<Text>().text=error;
        GameInfo.instance.keyController.SetActive(false);
		GameInfo.instance.fatalError = true;
       
    }

    /// <summary>
    /// Loads the main menu.
    /// </summary>
	public void mainMenu()
    {
        unLoadMenu();
        GameInfo.instance.menuController.GetComponent<AllToMainTransition>().loadMainMenu();
		GameInfo.instance.fatalError = false;
    }
}
