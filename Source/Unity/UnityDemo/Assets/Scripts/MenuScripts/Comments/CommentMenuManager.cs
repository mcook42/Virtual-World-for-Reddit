using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RedditSharp.Things;
using System;
using UnityEngine.UI;

/// <summary>
/// Creates the comments.
/// </summary>
public class CommentMenuManager : MonoBehaviour {

	public GameObject commentPrefab;
	public GameObject title;
	public GameObject loadMoreButton;
	public GameObject loadMorePanel;



	public Post post { get; set; }

	private readonly int topLevelCommentsLoaded = 5;
	private IEnumerator commentEnumerator;



	/// <summary>
	/// Loads the Comments and sets them up.
	/// </summary>
	void Start()
	{
		commentEnumerator = post.EnumerateComments ().GetEnumerator ();
		commentEnumerator.MoveNext ();
		loadMoreButton.GetComponent<Button> ().onClick.AddListener (() => loadMore ());
		loadMore ();

	}
		

	/// <summary>
	/// Initializes the comments.
	/// </summary>
	/// <returns>The number of top level comments initialized.</returns>
	/// <param name="parent">Parent comments are attached to.</param>
	/// <param name="comments">Comments.</param>
	/// <param name="startDepth">Depth of comment tree thus far.</param>
	public int initializeComments(GameObject parent,Comment parentComment,int startDepth)
	{
		
		Comment[] comments = parentComment.Comments.ToArray();
		return initializeComments (parent,comments, startDepth);


	}

	/// <summary>
	/// Initializes the comments.
	/// </summary>
	/// <returns>The number of top level comments initialized.</returns>
	/// <param name="parent">Parent comments are attached to.</param>
	/// <param name="comments">Comments.</param>
	/// <param name="startDepth">Depth of comment tree thus far.</param>
	public int initializeComments(GameObject parent, Comment[] comments, int startDepth)
	{
		int count = 0;
		for( int i=0;i<comments.Count();i++) {
			GameObject commentObject = initializeComment (parent, comments [i], startDepth);
			count++;


			var secondComments = comments [i].Comments.ToArray();
			commentObject.GetComponent<CommentInfo> ().childPanel.SetActive (true);
			initializeComments (commentObject.GetComponent<CommentInfo> ().childPanel, secondComments, startDepth + 1);

		}

		return count;
	}
		

	/// <summary>
	/// Initializes the comment.
	/// </summary>
	/// <param name="comment">Comment.</param>
	/// <param name="depth">Depth in comment tree.</param>
	/// <returns> The initialized Comment.</returns>
	public GameObject initializeComment(GameObject parent,Comment comment, int depth)
	{
		GameObject commentObject = Instantiate (commentPrefab) as GameObject;
		commentObject.transform.SetParent (parent.transform);
		CommentInfo info = commentObject.GetComponent<CommentInfo> ();
		info.Init (depth, comment);
		loadMorePanel.transform.SetAsLastSibling ();
		return commentObject;
	}
		
	/// <summary>
	/// Loads topCommentsLoaded more comments.
	/// </summary>
	public void loadMore()
	{
		List<Comment> comments = new List<Comment> ();
		for (int i = 0; i < topLevelCommentsLoaded; i++) {
			if (commentEnumerator.Current == null)
				break;
			else
				comments.Add ((Comment)commentEnumerator.Current);
			commentEnumerator.MoveNext ();

		}
		//no more comments to load.
		if (commentEnumerator.Current == null)
			loadMorePanel.SetActive (false);
			
		initializeComments (this.gameObject,comments.ToArray(),0);
	}
}
