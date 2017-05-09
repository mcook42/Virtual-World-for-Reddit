/**Caleb Whitman
 * calebrwhitman@gmail.com
 * Spring 2017
 */ 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A text menu that gives the user the option to quit or return to the main menu. Meant for fatal errors.
/// </summary>
public class FatalErrorMenu : Menu {

	/// <summary>
	/// Assigns the error message.
	/// </summary>
	/// <param name="error">Error.</param>
	public void init(string error)
	{
		transform.Find("Text").GetComponent<Text>().text=error;
		WorldState.instance.fatalError = false;
	}
		
    /// <summary>
    /// Loads the main menu.
    /// </summary>
	public void mainMenu()
    {
        
        WorldState.instance.menuController.GetComponent<ToMainTransition>().loadMainMenu();
		Destroy (gameObject);

    }
}
