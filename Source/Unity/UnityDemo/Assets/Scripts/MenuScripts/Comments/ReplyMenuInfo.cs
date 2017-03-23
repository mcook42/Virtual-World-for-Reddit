using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedditSharp.Things;
using UnityEngine.UI;

public class ReplyMenuInfo : MonoBehaviour {

	public GameObject title;
	public GameObject input;
	public Comment comment { get; set; }

	public void init(Comment comment)
	{
		this.comment = comment;
	}

}
