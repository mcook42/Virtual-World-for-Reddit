using System;
using UnityEngine;
using RedditSharp.Things;
using UnityEngine.UI;
using System.Net;
using RedditSharp;

/**Caleb Whitman
 * calebrwhitman@gmail.com
 * Spring 2017
 */ 

/// <summary>
/// Allows the user to reply to a post or comment.
/// </summary>
public class ReplyMenu : Menu
{
	//objects in the menu
	public GameObject title;
	public GameObject input;

	//parent post or comment.
	public Comment comment { get; set; }
	public Post post {get; set;}


	private VotableInfo parent;


	/// <summary>
	/// Sets up a new reply menu that will reply to comment.
	/// </summary>
	/// <param name="comment">Comment to reply to.</param>
	/// <param name="parent">VotableInfo attached to parent comment.</param>
	public void init(Comment comment,VotableInfo parent)
	{
		this.parent = parent;
		this.comment = comment;
		title.GetComponent<Text> ().text = comment.Body;
	}

	/// <summary>
	/// Sets up a new reply menu that will reply to the post.
	/// </summary>
	/// <param name="post">Post to reply to.</param>
	/// <param name="parent">VotableInfo attached to parent post..</param>
	public void init(Post post, VotableInfo parent)
	{
		this.parent = parent;
		this.post = post;
		title.GetComponent<Text> ().text = post.SelfText;
	}
		
	/// <summary>
	/// Sends reply request to Reddit. If successful adds new comment to parent object. If failure, displays the error menu.
	/// </summary>
	public void reply()
	{
			try {
			if(comment!=null)
			{
				var newComment = comment.Reply (input.GetComponent<Text> ().text);
				parent.addChild(newComment);
			}
			else
			{
				var newComment = post.Comment (input.GetComponent<Text> ().text);
				parent.addChild(newComment);
			}
			} catch (WebException w) {
				GameInfo.instance.menuController.GetComponent<MenuController> ().loadErrorMenu("Web Error: " + w.Message);
			//Reddit restricts how often new users can comment.
			} catch (RateLimitException r) {
				GameInfo.instance.menuController.GetComponent<MenuController> ().loadErrorMenu("You are doing that too much. Try again in " + r.TimeToReset+".");
			}

		Destroy (gameObject);

	}

	/// <summary>
	/// Closes the menu.
	/// </summary>
	public void cancel()
	{
		Destroy (gameObject);
	}
}


