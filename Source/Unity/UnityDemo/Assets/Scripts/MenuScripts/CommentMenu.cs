using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedditSharp.Things;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// The Menu for viewing and interacting with comments.
/// </summary>
public class CommentMenu :Menu<CommentMenu> {

	public GameObject thread;
	/// <summary>
	/// The basic menu load. Simply loads the menu and makes the cursor appear.
	/// </summary>
	/// <param name="post">The post where the comments are stored.</param>
	public void loadMenu(Post post)
	{
		base.loadMenu (true);
		CommentSetup setup= instance.GetComponentInChildren<CommentSetup> ();
		setup.post = post;
		setup.title.GetComponent<Text> ().text = post.Title;
	}

	/// <summary>
	/// Loads the Menu based on the current thread.
	/// </summary>
	public void OnMouseDown()
	{
		if (EventSystem.current.IsPointerOverGameObject())
			return;

		Post post = thread.GetComponent<BuildingThread> ().thread;
		loadMenu (post);

	}
}
