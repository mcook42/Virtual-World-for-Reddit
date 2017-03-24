using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedditSharp.Things;
using UnityEngine.UI;
using System.Net;
using RedditSharp;

/// <summary>
/// Holds the information related to the comment.
/// </summary>
public class CommentInfo : MonoBehaviour, LoginObserver {
	
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

		GameInfo.instance.redditRetriever.register (this);

	}

	/// <summary>
	/// Initializes the buttons.
	/// </summary>
	public void initializeButtons()
	{
		replyButton.GetComponent<Button> ().onClick.AddListener (() => reply ());
		saveButton.GetComponent<Button> ().onClick.AddListener (() => toggleSave());
		saveButton.GetComponentInChildren<Text> ().text = comment.Saved ? "Unsave" : "Save";
		downvoteButton.GetComponent<Button> ().onClick.AddListener (() => downvote ());
		downvoteButton.GetComponentInChildren<Text> ().text = (comment.Vote == VotableThing.VoteType.Downvote) ? "Downvoted!" : "Downvote";
		upvoteButton.GetComponent<Button> ().onClick.AddListener (() => upvote ());
		upvoteButton.GetComponentInChildren<Text> ().text = (comment.Vote == VotableThing.VoteType.Upvote) ? "Upvoted!" : "Upvote";
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

	/// <summary>
	/// Brings up the reply menu.
	/// </summary>
	public void reply(){
		GameInfo.instance.menuController.GetComponent<ReplyMenu> ().loadMenu (comment);
	}

	/// <summary>
	/// Saves the comment if it is saved. Unsaves it if it isn't.
	/// </summary>
	public void toggleSave()
	{
		if (GameInfo.instance.reddit.User != null) {
			try{
				if(comment.Saved)
				{
					comment.Unsave();
					saveButton.GetComponentInChildren<Text>().text="Save";
				}
				else
				{
					comment.Save ();
					saveButton.GetComponentInChildren<Text>().text="Unsave";
				}
			}
			catch(WebException w) {
				GameInfo.instance.menuController.GetComponent<ErrorMenu> ().loadMenu ("Web Error: "+w.Message);
			}
		} else {
			GameInfo.instance.menuController.GetComponent<ErrorMenu> ().loadMenu ("Log in to save.");
		}
	}

	/// <summary>
	/// Upvotes the comment.
	/// </summary>
	public void upvote()
	{
		if (GameInfo.instance.reddit.User != null) {
			try{
				if(comment.Vote==VotableThing.VoteType.Upvote)
				{
					comment.SetVote(VotableThing.VoteType.None);
				}
				else
				{
					comment.Upvote();
				}

				upvoteButton.GetComponentInChildren<Text> ().text = (comment.Vote == VotableThing.VoteType.Upvote) ? "Upvoted!" : "Upvote";

			}
			catch(WebException w) {
				GameInfo.instance.menuController.GetComponent<ErrorMenu> ().loadMenu ("Web Error: "+w.Message);
			}
		} else {
			GameInfo.instance.menuController.GetComponent<ErrorMenu> ().loadMenu ("Log in to upvote.");
		}
	}

	/// <summary>
	/// Downvotes the comment.
	/// </summary>
	public void downvote()
	{
		if (GameInfo.instance.reddit.User != null) {
			try{
				if(comment.Vote==VotableThing.VoteType.Downvote)
				{
					comment.SetVote(VotableThing.VoteType.None);
				}
				else
				{
					comment.Downvote();
				}

				downvoteButton.GetComponentInChildren<Text> ().text = (comment.Vote == VotableThing.VoteType.Downvote) ? "Downvoted!" : "Downvote";

			}
			catch(WebException w) {
				GameInfo.instance.menuController.GetComponent<ErrorMenu> ().loadMenu ("Web Error: "+w.Message);
			}
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

	/// <summary>
	/// Sets or deactivates the action panel.
	/// </summary>
	/// <param name="login">If set to <c>true</c> login.</param>
	public void notify(bool login)
	{
		if (login)
			actionPanel.SetActive (true);
		else
			actionPanel.SetActive (false);

	}

	public void OnDestroy()
	{
		GameInfo.instance.redditRetriever.unRegister (this);

	}
}
