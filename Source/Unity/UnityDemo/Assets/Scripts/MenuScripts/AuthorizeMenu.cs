using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Net;

/// <summary>
/// The menu to let the user allow our application to access their account. 
/// Is created after a user logs in.
/// </summary>
public class AuthorizeMenu : Menu<AuthorizeMenu>{




	public void loadMenu(JToken token,string postParams)
	{
		base.loadMenu (true);
		instance.GetComponent<AuthorizeMenuInfo> ().init (token, postParams);

	}

	/// <summary>
	/// Authorizes the user.
	/// </summary>
	public void allow()
	{
		try{
			var code = GameInfo.instance.redditRetriever.getCode (instance.GetComponent<AuthorizeMenuInfo>().token, instance.GetComponent<AuthorizeMenuInfo>().postParams);
			GameInfo.instance.redditRetriever.authenticateUser (code);
		}
		catch(WebException w) {
			GameInfo.instance.menuController.GetComponent<ErrorMenu> ().loadMenu ("Web Error: " + w.Message);
		}
		catch (System.Security.Authentication.AuthenticationException ae)
		{
			GameInfo.instance.menuController.GetComponent<ErrorMenu> ().loadMenu ("Authentication Error: " + ae.Message);

		}

		base.unLoadMenu ();

	}

	public void decline()
	{
		GameInfo.instance.menuController.GetComponent<AuthorizeMenu> ().unLoadMenu ();
	}

}
