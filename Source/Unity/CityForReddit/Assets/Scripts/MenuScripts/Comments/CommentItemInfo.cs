using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedditSharp.Things;
using UnityEngine.UI;

/**Caleb Whitman
 * calebrwhitman@gmail.com
 * Spring 2017
 */ 

/// <summary>
/// Displays the comment as an item in a list. Action buttons are missing and post and subreddit are displayed.
/// </summary>
public class CommentItemInfo : CreatedInfo {

	public GameObject postTitle;
	public GameObject viewButton;
	public GameObject author;
	public GameObject time;

	public GameObject body;

	public GameObject postCommentMenuPrefab;

	/// <summary>
	/// Initialize the fields.
	/// </summary>
	/// <param name="comment">Comment.</param>
	public void init(Comment comment)
	{
		base.thing = comment;

		postTitle.GetComponent<Text> ().text = comment.LinkTitle;
		body.GetComponent<Text> ().text = comment.Body;
		author.GetComponent<Text> ().text = comment.Author;
		time.GetComponent<Text> ().text = getTimeCreated (comment.CreatedUTC);
		upvotes.GetComponent<Text> ().text = "Upvotes: "+comment.Upvotes;

	}

	/// <summary>
	/// Brings up the postCommentMenu for the comment.
	/// </summary>
	public void view()
	{
		GameObject postCommentMenu = Instantiate (postCommentMenuPrefab);

		postCommentMenu.GetComponent<PostCommentMenu> ().Init ((Post)WorldState.instance.reddit.GetThingByFullname(((Comment)thing).LinkId));
	}

}
