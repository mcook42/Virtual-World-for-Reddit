using System;
using UnityEngine;
using RedditSharp.Things;
using System.Net;
using UnityEngine.UI;

/**Caleb Whitman
 * calebrwhitman@gmail.com
 * Spring 2017
 */ 

/// <summary>
/// An abstract class that represents a votable object.
/// </summary>
public abstract class VotableInfo : CreatedInfo, LoginObserver
{

	//Thing we are representing. Hides the parent thing.
	protected new VotableThing thing;

	#region LoginObserver
	/// <summary>
	/// Sets or deactivates the action panel.
	/// </summary>
	/// <param name="login">If set to <c>true</c> login.</param>
	public void notify(bool login)
	{
		if (actionPanel != null) {
			if (login)
				actionPanel.SetActive (true);
			else
				actionPanel.SetActive (false);
		}

		thing.UpdateReddit (WorldState.instance.reddit);

	}

	public void Start()
	{
		WorldState.instance.redditRetriever.register (this);
	}

	public void OnDestroy()
	{
		WorldState.instance.redditRetriever.unRegister (this);
	}

	#endregion

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
		if (WorldState.instance.reddit.User != null) {
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
				WorldState.instance.menuController.GetComponent<MenuController> ().loadErrorMenu("Web Error: "+w.Message);
			}
		} else {
			WorldState.instance.menuController.GetComponent<MenuController> ().loadErrorMenu("Log in to save.");
		}
	}

	/// <summary>
	/// Upvotes the comment.
	/// </summary>
	public void upvote()
	{
		if (WorldState.instance.reddit.User != null) {
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
				WorldState.instance.menuController.GetComponent<MenuController> ().loadErrorMenu("Web Error: "+w.Message);
			}
		} else {
			WorldState.instance.menuController.GetComponent<MenuController> ().loadErrorMenu("Log in to upvote.");
		}
	}

	/// <summary>
	/// Downvotes the comment.
	/// </summary>
	public void downvote()
	{
		if (WorldState.instance.reddit.User != null) {
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
				WorldState.instance.menuController.GetComponent<MenuController> ().loadErrorMenu("Web Error: "+w.Message);
			}
		} else {
			WorldState.instance.menuController.GetComponent<MenuController> ().loadErrorMenu("Log in to downvote.");
		}
	}

	/// <summary>
	/// Converts the reddit markdown into a string that unity can understand.
	/// </summary>
	/// <returns>A string that can be reconized by Unity.</returns>
	/// <param name="markdown">string with Reddit markdown.</param>
	public string convertRedditMarkdown(string markdown)
	{

		return null;
	}
}


