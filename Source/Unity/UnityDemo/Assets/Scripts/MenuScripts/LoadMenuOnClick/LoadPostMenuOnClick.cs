using System;
using UnityEngine;
using RedditSharp.Things;

/// <summary>
/// Load post menu on click.
/// </summary>
public class LoadPostMenuOnClick : LoadMenuOnClick
{
	public GameObject thread;

	/// <summary>
	/// Grabs the post object and gives it to the comment menu.
	/// </summary>
	public override void loadMenu()
	{
		Post post = thread.GetComponent<BuildingThread> ().thread;
		PostCommentMenu setup = menuInstance.GetComponentInChildren<PostCommentMenu> ();
		setup.Init (post);
	}
}


