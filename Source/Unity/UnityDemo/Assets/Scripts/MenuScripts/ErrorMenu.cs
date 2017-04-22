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
public class ErrorMenu : Menu{

	/// <summary>
	/// Sets the error message.
	/// </summary>
	/// <param name="error">Error.</param>
	public void init(string error)
	{
		transform.Find("Text").GetComponent<Text>().text = error;
	}

    /// <summary>
    /// Unloads the menu.
    /// </summary>
    public void resume()
    {
		Destroy (gameObject);
    }
}
