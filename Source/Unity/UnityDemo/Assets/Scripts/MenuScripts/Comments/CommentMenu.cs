using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedditSharp.Things;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Security.Authentication;

/// <summary>
/// Creates the Comment Menu. Attached to the menuController object. Once created, control of the content of the menu is handled by CommentMenuManager.
/// </summary>
public class CommentMenu :Menu<CommentMenu> {

	public GameObject thread;
	/// <summary>
	/// The basic menu load. Simply loads the menu and makes the cursor appear.
	/// </summary>
	/// <param name="post">The post where the comments are stored.</param>
	public void loadPostMenu(Post post)
	{
		base.loadMenu (true);
		PostCommentMenuManager setup= instance.GetComponentInChildren<PostCommentMenuManager> ();
		setup.Init (post);
	}

	/// <summary>
	/// Loads the overview for the logged in user.
	/// </summary>
	public void loadOverviewMenu()
	{
		base.loadMenu (true);
		OverviewCommentMenuManager setup = instance.GetComponentInChildren<OverviewCommentMenuManager> ();

		try{setup.Init ();}
		catch(AuthenticationException) {
			GameInfo.instance.menuController.GetComponent<ErrorMenu> ().loadMenu ("User not logged in.");
			unLoadMenu ();
		}



	}

	/// <summary>
	/// Loads the Menu based on the current thread.
	/// </summary>
	public void OnMouseDown()
	{
		if (EventSystem.current.IsPointerOverGameObject())
			return;

		if (gameObject.name == "Overview")
			loadOverviewMenu ();
		else
		{
			Post post = thread.GetComponent<BuildingThread> ().thread;
			loadPostMenu (post);
		}

	}
}
