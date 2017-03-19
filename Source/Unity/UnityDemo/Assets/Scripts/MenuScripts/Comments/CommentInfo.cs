using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedditSharp.Things;
using UnityEngine.UI;

/// <summary>
/// Holds the information related to the comment.
/// </summary>
public class CommentInfo : MonoBehaviour {
	
	public Comment comment { get; set;}
	public int childDepth { get; set;}

	//children
	public GameObject childPanel;

	//text fields
	public GameObject author;
	public GameObject time;
	public GameObject upvotes;
	public GameObject body;

	//buttons
	public GameObject actionPanel;
	public GameObject loadMorePanel;
	public GameObject replyButton;
	public GameObject saveButton;
	public GameObject upvoteButton;
	public GameObject downvoteButton;
	public GameObject loadMoreButton;

	/// <summary>
	/// Creates the comment.
	/// </summary>
	/// <param name="childDepth">Child depth.</param>
	/// <param name="comment">Comment.</param>
	public void Init(int childDepth,Comment comment)
	{
		this.comment = comment;
		this.childDepth = childDepth;

		string dotPadding = "";
		int childDepthLimit = 2;
		if (childDepth <= childDepthLimit) {
			GetComponent<VerticalLayoutGroup> ().padding.left = (childDepth ==0) ? 10 : 60;
		}
		else {
			dotPadding = new string('-', (childDepth - childDepthLimit));
		}

		author.GetComponent<Text> ().text = dotPadding+comment.Author;
		time.GetComponent<Text> ().text = comment.Created.ToString();
		upvotes.GetComponent<Text> ().text = "Upvotes:"+comment.Upvotes;
		body.GetComponent<Text> ().text = comment.Body;


		initializeButtons ();


	}

	/// <summary>
	/// Initializes the buttons.
	/// </summary>
	public void initializeButtons()
	{
		replyButton.GetComponent<Button> ().onClick.AddListener (() => reply ());
		saveButton.GetComponent<Button> ().onClick.AddListener (() => save());
		downvoteButton.GetComponent<Button> ().onClick.AddListener (() => downvote ());
		upvoteButton.GetComponent<Button> ().onClick.AddListener (() => upvote ());
		loadMoreButton.GetComponent<Button> ().onClick.AddListener (() => loadMore ());

		//no need for these buttons if the user isn't logged in.
		if (GameInfo.instance.reddit.User == null) {
			actionPanel.SetActive (false);
		}

		//Only need More button is there are more comments to load.
		if (comment.More == null) {
			loadMorePanel.SetActive (false);
		}

	}

	public void reply(){
		Debug.Log ("Reply");
	}

	public void save()
	{
		if (GameInfo.instance.reddit.User != null) {
			comment.Save ();
		} else {
			GameInfo.instance.menuController.GetComponent<ErrorMenu> ().loadMenu ("Log in to save.");
		}
	}

	public void upvote()
	{
		if (GameInfo.instance.reddit.User != null) {
			comment.Upvote();
		} else {
			GameInfo.instance.menuController.GetComponent<ErrorMenu> ().loadMenu ("Log in to upvote.");
		}
	}

	public void downvote()
	{
		if (GameInfo.instance.reddit.User != null) {
			comment.Downvote ();
		} else {
			GameInfo.instance.menuController.GetComponent<ErrorMenu> ().loadMenu ("Log in to downvote.");
		}
	}

	/// <summary>
	/// Loads more comments according to the settings in CommentSetup.
	/// </summary>
	public void loadMore()
	{
		var more = comment.More;
		List<Comment> comments = new List<Comment> ();
		CommentMenuManager post = GetComponentInParent<CommentMenuManager> ();
		more.ParentId = post.post.FullName;
		foreach (Thing thing in more.Things()) {
			comments.Add ((Comment)thing);
		}
		loadMorePanel.SetActive (false);
		childPanel.SetActive (true);
		post.initializeComments (childPanel,comments.ToArray(), childDepth + 1);
	}


}
