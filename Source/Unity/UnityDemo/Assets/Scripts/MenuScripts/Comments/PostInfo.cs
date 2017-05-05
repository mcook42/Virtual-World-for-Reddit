using System;
using RedditSharp.Things;
using UnityEngine;
using UnityEngine.UI;

/**Caleb Whitman
 * calebrwhitman@gmail.com
 * Spring 2017
 */ 

/// <summary>
/// Displays post information, like title, author, upvotes etc.
/// Attached to the post object.
/// </summary>
public class PostInfo : CreatedInfo
{

	/// GameObjects
	public GameObject title;
	public GameObject commentNum;
	public GameObject viewButton;
	public GameObject author;
	public GameObject time;


	public GameObject PostCommentMenuPrefab;
	/// <summary>
	/// Sets up all the information.
	/// </summary>
	/// <param name="post">Post.</param>
	public void init(Post post)
	{
		title.GetComponent<Text> ().text = post.Title;
		commentNum.GetComponent<Text> ().text = "Comment #:" + post.CommentCount;
		author.GetComponent<Text> ().text = post.AuthorName;
		time.GetComponent<Text> ().text = getTimeCreated (post.CreatedUTC);
		upvotes.GetComponent<Text> ().text = "Upvotes:"+post.Upvotes;

		thing = post;

	}
		
	/// <summary>
	/// Lets the user view the post.
	/// </summary>
	public void view (){

		PostCommentMenu setup = Instantiate(PostCommentMenuPrefab).GetComponentInChildren<PostCommentMenu> ();
		setup.Init ((Post)thing);
	}
}


