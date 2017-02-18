/**LocationPanel.cs
* Caleb Whitman
* February 17, 2017
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A location text that exists at the top of the screen.
/// </summary>
public class LocationPanel : Menu<LocationPanel> {

    /// <summary>
    /// Puts the text on the top of the screen.
    /// </summary>
    /// <param name="location">The location message.</param>
    public void loadPanel(string location)
    {
        base.loadPanel();
        instance.transform.Find("Text").GetComponent<Text>().text = location;
    }

    /// <summary>
    /// Change the location text at the top of the screen.
    /// </summary>
    /// <param name="location"></param>
    public void changeText(string location)
    {
        instance.transform.Find("Text").GetComponent<Text>().text = location;
    }
}
