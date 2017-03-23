using System;
using UnityEngine;
using RedditSharp.Things;
using UnityEngine.UI;
using System.Net;


public class ReplyMenu :Menu<ReplyMenu>
{
	public void loadMenu(Comment comment)
	{
		base.loadMenu (true);
		instance.GetComponent<ReplyMenuInfo> ().comment = comment;
		instance.GetComponent<ReplyMenuInfo> ().title.GetComponent<Text> ().text = comment.Body;

	}

	public void reply()
	{
		try{
			instance.GetComponent<ReplyMenuInfo> ().comment.Reply(instance.GetComponent<ReplyMenuInfo>().input.GetComponent<Text>().text);
		}
		catch(WebException w) {
			GameInfo.instance.menuController.GetComponent<ErrorMenu> ().loadMenu ("Web Error: "+w.Message);
		}
		GameInfo.instance.menuController.GetComponent<ReplyMenu> ().unLoadMenu ();

	}

	public void cancel()
	{
		GameInfo.instance.menuController.GetComponent<ReplyMenu> ().unLoadMenu ();
	}
}


