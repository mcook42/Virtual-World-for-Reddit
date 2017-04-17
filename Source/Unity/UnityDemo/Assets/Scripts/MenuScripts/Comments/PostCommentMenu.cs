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
public class PostCommentMenu : CommentMenu {

	public GameObject descriptionPrefab;

	public Post post { get; set; }


	public void Init(Post post){
		this.post = post;
		title.GetComponent<Text> ().text = post.Title;
		commentEnumerator = post.EnumerateComments ().GetEnumerator ();
		loadMoreButton.GetComponent<Button> ().onClick.AddListener (() => loadMore ());
		instantiateDescription ();
		loadMore ();
	}

	/// <summary>
	/// Instantiates the description.
	/// </summary>
	private void instantiateDescription()
	{
		GameObject description = Instantiate (descriptionPrefab, content.transform);
		description.GetComponent<PostInfo> ().Init (post,this);
	}

	/// <summary>
	/// Initializes the comment.
	/// </summary>
	/// <param name="comment">Comment.</param>
	/// <param name="depth">Depth in comment tree.</param>
	/// <returns> The initialized Comment.</returns>
	public override GameObject initializeComment(GameObject parent,Comment comment, int depth)
	{
		GameObject commentObject = Instantiate (commentPrefab) as GameObject;
		commentObject.transform.SetParent (parent.transform);
		CommentInfo info = commentObject.GetComponent<CommentInfo> ();
		info.PostInit (depth, comment,this,post);
		loadMorePanel.transform.SetAsLastSibling ();
		return commentObject;
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
	public override int initializeComments(GameObject parent, Comment[] comments, int startDepth)
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
		


}
