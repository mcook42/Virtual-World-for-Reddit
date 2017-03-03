using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RedditSharp.Things;
using System;

/// <summary>
/// Creates the comments.
/// </summary>
public class CommentSetup : MonoBehaviour {

	public GameObject commentPrefab;
	public GameObject title;


	public Post post { get; set; }

	//Values to determine how many comments are loaded at a time. Will be messed with.
	public int topCommentsLoaded;
	public int secondCommentsLoaded;

	/// <summary>
	/// Loads the Comments and sets them up.
	/// </summary>
	void Start()
	{
		topCommentsLoaded = 2;
		secondCommentsLoaded = 0;
		initializeComments (this.gameObject,post, 0, 0);

	}

	/// <summary>
	/// Initializes the comments.
	/// </summary>
	/// <returns>The number of top level comments initialized.</returns>
	/// <param name="parent">Parent comments are attached to.</param>
	/// <param name="comments">Comments.</param>
	/// <param name="startDepth">Depth of comment tree thus far.</param>
	/// <param name="startingNumber">Where to start getting the comments from comments.</param>
	public int initializeComments(GameObject parent,Post parentComment,int startDepth,int startingNumber)
	{

		Comment[] comments = parentComment.Comments.Take (topCommentsLoaded+startingNumber).ToArray();
		return initializeComments (parent,comments, startDepth, startingNumber);

	}

	/// <summary>
	/// Initializes the comments.
	/// </summary>
	/// <returns>The number of top level comments initialized.</returns>
	/// <param name="parent">Parent comments are attached to.</param>
	/// <param name="comments">Comments.</param>
	/// <param name="startDepth">Depth of comment tree thus far.</param>
	/// <param name="startingNumber">Where to start getting the comments from comments.</param>
	public int initializeComments(GameObject parent,Comment parentComment,int startDepth,int startingNumber)
	{
		
		Comment[] comments = parentComment.Comments.Take (topCommentsLoaded+startingNumber).ToArray();
		return initializeComments (parent,comments, startDepth, startingNumber);


	}

	/// <summary>
	/// Initializes the comments.
	/// </summary>
	/// <returns>The number of top level comments initialized.</returns>
	/// <param name="parent">Parent comments are attached to.</param>
	/// <param name="comments">Comments.</param>
	/// <param name="startDepth">Depth of comment tree thus far.</param>
	/// <param name="startingNumber">Where to start getting the comments from comments.</param>
	public int initializeComments(GameObject parent, Comment[] comments, int startDepth, int startNumber)
	{
		int count = 0;
		for( int i=startNumber;i<comments.Count();i++) {
			
			if (comments [i].Created.Year != 1) { //TODO figure out what is causing this.
				GameObject commentObject = initializeComment (parent, comments [i], startDepth, secondCommentsLoaded);
				count++;

				var secondComments = comments [i].Comments.Take (secondCommentsLoaded).ToArray();
				commentObject.GetComponent<CommentInfo> ().childPanel.SetActive (true);
				for(int j=0; j<secondComments.Count();j++){
					if (secondComments[j].Created.Year != 1)
						initializeComment (commentObject.GetComponent<CommentInfo>().childPanel, secondComments[j], startDepth + 1,0);

				}
			}


		}

		return count;
	}
		

	/// <summary>
	/// Initializes the comment.
	/// </summary>
	/// <param name="comment">Comment.</param>
	/// <param name="depth">Depth in comment tree.</param>
	/// <returns> The initialized Comment.</returns>
	public GameObject initializeComment(GameObject parent,Comment comment, int depth,int childrenLoaded)
	{
		GameObject commentObject = Instantiate (commentPrefab) as GameObject;
		commentObject.transform.SetParent (parent.transform);
		CommentInfo info = commentObject.GetComponent<CommentInfo> ();
		info.Init (depth, comment,childrenLoaded);
		return commentObject;
	}
		
}
