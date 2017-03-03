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
	public int childrenLoaded { get; set; }

	//children
	public GameObject childPanel;

	//text fields
	public GameObject author;
	public GameObject time;
	public GameObject upvotes;
	public GameObject body;

	//buttons
	public GameObject actionPanel;
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
	public void Init(int childDepth,Comment comment, int childrenLoaded)
	{
		this.comment = comment;
		this.childDepth = childDepth;
		this.childrenLoaded = childrenLoaded;



		string dotPadding = "";
		int childDepthLimit = 1;
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

	}

	public void reply(){
		Debug.Log ("Reply");
	}

	public void save()
	{
		Debug.Log ("Save");
	}

	public void upvote()
	{
		Debug.Log ("Upvote");
	}

	public void downvote()
	{
		Debug.Log ("Downvote");
	}

	/// <summary>
	/// Loads more comments according to the settings in CommentSetup.
	/// </summary>
	public void loadMore()
	{
		childPanel.SetActive (true);
		childrenLoaded+=GetComponentInParent<CommentSetup>().initializeComments (childPanel,comment, childDepth + 1, childrenLoaded);
	}


}
