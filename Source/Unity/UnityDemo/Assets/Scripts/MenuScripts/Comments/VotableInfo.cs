using System;
using UnityEngine;
using RedditSharp.Things;
using System.Net;
using UnityEngine.UI;

/// <summary>
/// An abstract class that represents a votable object.
/// Holds the similar methods that both CommentInfo and PostInfo share.
/// </summary>
public abstract class VotableInfo :MonoBehaviour, LoginObserver
{
	protected VotableThing thing { get; set; }

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

	//text fields
	public GameObject author;
	public GameObject time;
	public GameObject upvotes;

	//text fields
	public GameObject body;

	//reply menu
	public GameObject replyMenuPrefab;

	//buttons
	public GameObject actionPanel;
	public GameObject replyButton;
	public GameObject saveButton;
	public GameObject upvoteButton;
	public GameObject downvoteButton;

	/// <summary>
	/// Initializes the buttons.
	/// </summary>
	protected abstract void initializeButtons();

	/// <summary>
	/// Adds a new child to this item.
	/// </summary>
	public abstract void addChild (Comment comment);

	/// <summary>
	/// Saves the comment if it is saved. Unsaves it if it isn't.
	/// </summary>
	public void toggleSave()
	{
		if (GameInfo.instance.reddit.User != null) {
			try{
				if(thing.Saved)
				{
					thing.Unsave();
					saveButton.GetComponentInChildren<Text>().text="Save";
				}
				else
				{
					thing.Save ();
					saveButton.GetComponentInChildren<Text>().text="Unsave";
				}
			}
			catch(WebException w) {
				GameInfo.instance.menuController.GetComponent<MenuController> ().loadErrorMenu("Web Error: "+w.Message);
			}
		} else {
			GameInfo.instance.menuController.GetComponent<MenuController> ().loadErrorMenu("Log in to save.");
		}
	}

	/// <summary>
	/// Upvotes the comment.
	/// </summary>
	public void upvote()
	{
		if (GameInfo.instance.reddit.User != null) {
			try{
				if(thing.Vote==VotableThing.VoteType.Upvote)
				{
					thing.SetVote(VotableThing.VoteType.None);
				}
				else
				{
					thing.Upvote();
				}

				upvoteButton.GetComponentInChildren<Text> ().text = (thing.Vote == VotableThing.VoteType.Upvote) ? "Upvoted!" : "Upvote";

			}
			catch(WebException w) {
				GameInfo.instance.menuController.GetComponent<MenuController> ().loadErrorMenu("Web Error: "+w.Message);
			}
		} else {
			GameInfo.instance.menuController.GetComponent<MenuController> ().loadErrorMenu("Log in to upvote.");
		}
	}

	/// <summary>
	/// Downvotes the comment.
	/// </summary>
	public void downvote()
	{
		if (GameInfo.instance.reddit.User != null) {
			try{
				if(thing.Vote==VotableThing.VoteType.Downvote)
				{
					thing.SetVote(VotableThing.VoteType.None);
				}
				else
				{
					thing.Downvote();
				}

				downvoteButton.GetComponentInChildren<Text> ().text = (thing.Vote == VotableThing.VoteType.Downvote) ? "Downvoted!" : "Downvote";

			}
			catch(WebException w) {
				GameInfo.instance.menuController.GetComponent<MenuController> ().loadErrorMenu("Web Error: "+w.Message);
			}
		} else {
			GameInfo.instance.menuController.GetComponent<MenuController> ().loadErrorMenu("Log in to downvote.");
		}
	}

}


