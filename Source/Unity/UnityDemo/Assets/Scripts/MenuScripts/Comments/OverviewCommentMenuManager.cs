using System;
using UnityEngine;
using RedditSharp.Things;
using System.Collections;
using System.Security.Authentication;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// Overview comment menu manager.
/// </summary>
public class OverviewCommentMenuManager:CommentMenuManager
{



	public void Init()
	{
		if (GameInfo.instance.reddit.User == null)
			throw new AuthenticationException ("Log in");
		commentEnumerator = GameInfo.instance.reddit.User.Overview.GetEnumerator ();
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
	public override int initializeComments (GameObject parent, Comment[] comments, int startDepth){ 
		int count = 0;
		for( int i=0;i<comments.Count();i++) {
			GameObject commentObject = initializeComment (parent, comments [i],0);
			count++;

			var secondComments = comments [i].Comments.ToArray();
			commentObject.GetComponent<CommentInfo> ().childPanel.SetActive (true);
			initializeComments (commentObject.GetComponent<CommentInfo> ().childPanel, secondComments,0);

		}

		return count;

	}


	/// <summary>
	/// Initializes the comment.
	/// </summary>
	/// <param name="comment">Comment.</param>
	/// <param name="depth">Depth in comment tree.</param>
	/// <returns> The initialized Comment.</returns>
	public override GameObject initializeComment(GameObject parent,Comment comment, int depth){ 
		GameObject commentObject = Instantiate (commentPrefab) as GameObject;
		commentObject.transform.SetParent (parent.transform);
		CommentInfo info = commentObject.GetComponent<CommentInfo> ();
		info.OverviewInit (comment);
		loadMorePanel.transform.SetAsLastSibling ();
		return commentObject;}



}


