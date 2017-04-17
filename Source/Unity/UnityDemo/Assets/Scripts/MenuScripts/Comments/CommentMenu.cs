using System;
using UnityEngine;
using RedditSharp.Things;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// An Abstract class with methods for organizing the comments.
/// </summary>
public abstract class CommentMenu : TempMenu
{
	public GameObject commentPrefab;
	public GameObject title;
	public GameObject loadMoreButton;
	public GameObject loadMorePanel;

	public GameObject content;

	protected readonly int topLevelCommentsLoaded = 5;
	protected 	IEnumerator commentEnumerator;


	/// <summary>
	/// Initializes the comments.
	/// </summary>
	/// <returns>The comments.</returns>
	/// <param name="parent">Parent.</param>
	/// <param name="comments">Comments.</param>
	/// <param name="startDepth">Start depth.</param>
	public abstract int initializeComments (GameObject parent, Comment[] comments, int startDepth);


	/// <summary>
	/// Initializes the comment.
	/// </summary>
	/// <param name="comment">Comment.</param>
	/// <param name="depth">Depth in comment tree.</param>
	/// <returns> The initialized Comment.</returns>
	public abstract GameObject initializeComment(GameObject parent,Comment comment, int depth);

	/// <summary>
	/// Loads more comments.
	/// </summary>
	public void loadMore(){
		bool commentsLeft = true;
		List<Comment> comments = new List<Comment> ();
		for (int i = 0; i < topLevelCommentsLoaded; i++) {
			if (commentEnumerator.MoveNext ())
				comments.Add ((Comment)commentEnumerator.Current);
			else {
				commentsLeft = false;
				break;
			}


		}
		//no more comments to load.
		if (!commentsLeft)
			loadMorePanel.SetActive (false);

		initializeComments (content,comments.ToArray(),0);


	}

	/// <summary>
	/// Destroys this menu.
	/// </summary>
	public void close()
	{
		Destroy (gameObject);
	}
}


