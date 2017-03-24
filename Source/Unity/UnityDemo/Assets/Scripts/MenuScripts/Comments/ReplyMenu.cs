using System;
using UnityEngine;
using RedditSharp.Things;
using UnityEngine.UI;
using System.Net;
using RedditSharp;


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
			try {
				var comment = instance.GetComponent<ReplyMenuInfo> ().comment.Reply (instance.GetComponent<ReplyMenuInfo> ().input.GetComponent<Text> ().text);
			} catch (WebException w) {
				GameInfo.instance.menuController.GetComponent<ErrorMenu> ().loadMenu ("Web Error: " + w.Message);
			//Reddit restricts how often new users can comment.
			} catch (RateLimitException r) {
				GameInfo.instance.menuController.GetComponent<ErrorMenu> ().loadMenu ("You are doing that too much. Try again in " + r.TimeToReset+".");
			}

		GameInfo.instance.menuController.GetComponent<ReplyMenu> ().unLoadMenu ();

	}

	public void cancel()
	{
		GameInfo.instance.menuController.GetComponent<ReplyMenu> ().unLoadMenu ();
	}
}


