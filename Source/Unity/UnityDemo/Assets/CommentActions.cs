using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the actions on comments.
/// A menu will appear to tell the user if the action
/// was successful or not.
/// </summary>
public class CommentActions : MonoBehaviour {

	public GameObject commentInfo;



	public void Reply(){
		if (transform.parent.transform.parent.GetComponent<CommentInfo>().comment == null)
			Debug.Log ("what");
		transform.parent.transform.parent.GetComponent<CommentInfo> ().reply ();
	}

	public void Save()
	{
		Debug.Log ("Save");
	}

	public void Upvote()
	{
		Debug.Log ("Upvote");
	}

	public void Downvote()
	{
		Debug.Log ("Downvote");
	}

	public void LoadMore()
	{
		
	}

}
