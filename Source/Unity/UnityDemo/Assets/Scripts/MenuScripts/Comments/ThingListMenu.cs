using System;
using UnityEngine;
using RedditSharp.Things;
using System.Collections;
using System.Security.Authentication;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// A menu that displays a list of Reddit things.
/// </summary>
public class ThingListMenu:ThingMenu
{


	/// <summary>
	/// Sets up the menu to load objects from things.
	/// </summary>
	/// <param name="things">Reddit objects to load from..</param>
	public void init(IEnumerator things)
	{
		if (GameInfo.instance.reddit.User == null)
			throw new AuthenticationException ("Not logged in.");
		thingEnumerator = things;
		loadMoreButton.GetComponent<Button> ().onClick.AddListener (() => loadMore ());
		loadMore ();
	}

	/// <summary>
	/// Initializes the comments.
	/// </summary>
	/// <returns>The comments.</returns>
	/// <param name="parent">Parent.</param>
	/// <param name="comments">Comments.</param>
	/// <param name="startDepth">Start depth.</param>
	public override int initializeThings (GameObject parent, Thing[] things, int startDepth){ 
		int count = 0;
		for( int i=0;i<things.Count();i++) {
			initializeThing (parent, things [i],0);
			count++;

		}

		return count;
	}


	/// <summary>
	/// Initializes the thing.
	/// </summary>
	/// <param name="thing">Thing to initialize.</param>
	/// <param name="depth">Depth in comment tree.</param>
	/// <returns> The initialized Thing.</returns>
	public override GameObject initializeThing(GameObject parent,Thing thing, int depth){ 

		GameObject thingObject = null;

		if (thing is Comment) {
			thingObject = Instantiate (commentPrefab) as GameObject;
			thingObject.transform.SetParent (parent.transform);
			CommentInfo info = thingObject.GetComponent<CommentInfo> ();
			info.listInit ((Comment)thing);
		}

		if (thing is Post) {
			thingObject = Instantiate (postPrefab) as GameObject;
			thingObject.transform.SetParent (parent.transform);
			PostInfo info = thingObject.GetComponent<PostInfo> ();
			info.init((Post)thing);
		}

		loadMorePanel.transform.SetAsLastSibling ();
		return thingObject; }

}


