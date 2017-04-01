/**KeyController.cs
 * Caleb Whitman
 * February 1, 2017
 * 
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedditSharp;
using UnityEngine;


/// <summary>
/// Controls key inputs besides the player keys.
/// </summary>
public class KeyController : MonoBehaviour {

	
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
}
