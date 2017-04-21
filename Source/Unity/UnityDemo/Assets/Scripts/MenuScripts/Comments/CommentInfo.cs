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
public class CommentInfo : VotableInfo {

	public int childDepth { get; set;}

	public Post post { get; set; }

	//children
	public GameObject childPanel;
	  

	//buttons
	public GameObject loadMorePanel;
	public GameObject loadMoreButton;

	//menuManager
	private ThingMenu menuManager;

	/// <summary>
	/// Creates the comment formatted for comments displayed in posts.
	/// </summary>
	/// <param name="childDepth">Child depth.</param>
	/// <param name="comment">Comment.</param>
	public void PostInit(int childDepth,Comment comment,ThingMenu menuManager, Post post)
	{
		this.post = post;
		this.thing = comment;
		this.childDepth = childDepth;
		this.menuManager = menuManager;


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
	/// Adds a new child to this comment.
	/// </summary>
	/// <param name="comment">Comment.</param>
	public override void addChild (Comment comment)
	{
		var newComment = menuManager.initializeThing (childPanel, comment, childDepth + 1);
		newComment.transform.SetAsFirstSibling ();
	}


	/// <summary>
	/// Initalization for the listmenu.
	/// </summary>
	/// <param name="comment">Comment.</param>
	public void listInit(Comment comment)
	{
		this.thing = comment;
		author.GetComponent<Text> ().text = comment.Author;
		time.GetComponent<Text> ().text = comment.Created.ToString();
		upvotes.GetComponent<Text> ().text = "Upvotes:"+comment.Upvotes;
		body.GetComponent<Text> ().text = comment.Body;

		loadMorePanel.SetActive (false);
		actionPanel.SetActive (false);

	}
	/// <summary>
	/// Initializes the buttons.
	/// </summary>
	protected override void  initializeButtons()
	{
		replyButton.GetComponent<Button> ().onClick.AddListener (() => reply ());
		saveButton.GetComponent<Button> ().onClick.AddListener (() => toggleSave());
		saveButton.GetComponentInChildren<Text> ().text = thing.Saved ? "Unsave" : "Save";
		downvoteButton.GetComponent<Button> ().onClick.AddListener (() => downvote ());
		downvoteButton.GetComponentInChildren<Text> ().text = (thing.Vote == VotableThing.VoteType.Downvote) ? "Downvoted!" : "Downvote";
		upvoteButton.GetComponent<Button> ().onClick.AddListener (() => upvote ());
		upvoteButton.GetComponentInChildren<Text> ().text = (thing.Vote == VotableThing.VoteType.Upvote) ? "Upvoted!" : "Upvote";
		loadMoreButton.GetComponent<Button> ().onClick.AddListener (() => loadMore ());

		//no need for these buttons if the user isn't logged in.
		if (GameInfo.instance.reddit.User == null) {
			actionPanel.SetActive (false);
		}

		//Only need More button is there are more comments to load.
		if (((Comment)thing).More == null) {
			loadMorePanel.SetActive (false);
		}

	}

	/// <summary>
	/// Brings up the reply menu.
	/// </summary>
	public void reply(){
		
		var replyMenu = Instantiate(replyMenuPrefab);
		replyMenu.GetComponent<ReplyMenu>().init((Comment)thing,this);

	}
		
	/// <summary>
	/// Loads more comments according to the settings in CommentSetup.
	/// </summary>
	public void loadMore()
	{
		var more = ((Comment)thing).More;
		List<Comment> comments = new List<Comment> ();
		more.ParentId = post.FullName;
		foreach (Thing comment in more.Things()) {
			comments.Add ((Comment)comment);
		}

		if (comments.Count > 0) {
			loadMorePanel.SetActive (false);
			childPanel.SetActive (true);
			menuManager.initializeThings (childPanel, comments.ToArray (), childDepth + 1);
		}
	}



}
