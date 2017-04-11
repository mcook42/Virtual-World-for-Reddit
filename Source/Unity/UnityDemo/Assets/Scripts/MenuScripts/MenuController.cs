/* MenuController.cs
 * Author: Caleb Whitman
 * January 20, 2016
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using System.Linq;

/// <summary>
///  A class that holds the canvas. This class also contains a few menu functions that don't belong to any particular menu.
/// </summary>
public class MenuController : MonoBehaviour {

    public GameObject canvas;

	//number of menus currently loaded. This is used to tell if the player is still in the menu screen.
	public int menusLoaded = 0;

	void Update () {
		if(Input.GetKeyDown("escape"))
		{
			GameInfo.instance.menuController.GetComponent<PauseMenu>().pause();
		}

		if(Input.GetKeyDown("e"))
		{
			GameInfo.instance.menuController.GetComponent<FatalErrorMenu>().loadMenu("You can't press that");
		}

		if(Input.GetKeyDown("r"))
		{
			GameInfo.instance.menuController.GetComponent<Sort>().OnMouseDown();
		}

		if (Input.GetKeyDown ("c")) {

			var sub = GameInfo.instance.reddit.GetSubreddit ("/r/AskReddit");
			var posts = sub.Hot.Take (1);
			var post = posts.ToArray () [0];
			if (post == null) {
				Debug.Log ("here");
			}

			GameInfo.instance.menuController.GetComponent<CommentMenu> ().loadPostMenu (post);
		}

		if(Input.GetKeyDown("m"))
		{
			GameInfo.instance.menuController.GetComponent<MapMenu>().loadMenu(true);
		}

		if (Input.GetKeyDown ("l")) {
			GameInfo.instance.menuController.GetComponent<LogInMenu>().loadMenu(true);
		}
	}


    /// <summary>
    /// Exists the application. This will not work within the editor.
    /// </summary>
    public void quit(){
		
		Application.Quit ();

	}


}
