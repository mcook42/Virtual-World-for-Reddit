using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using Newtonsoft.Json.Linq;

public class LogInMenu : Menu<LogInMenu> {

	public InputField username;
	public InputField password;

	/// <summary>
	/// Cancel the log in.
	/// </summary>
	public void cancel()
	{
		GameInfo.instance.menuController.GetComponent<LogInMenu> ().unLoadMenu ();

	}

	/// <summary>
	/// Logs the user in. If an error is encountered the ErrorMenu is displayed.
	/// If the user exists, then an authentication menu will appear to log them in.
	/// </summary>
	public void LogIn()
	{
		string user = username.text;
		string pass = password.text;
		var r = GameInfo.instance.redditRetriever;
		JToken token = null;
		string postParams = "";
		try{
			var loginURL = r.getLoginURL();
			postParams = r.getPostParams (loginURL);
			token = r.getUserJToken (user, pass, postParams);

			if(token==null)
				throw new System.Security.Authentication.AuthenticationException("Username/Password not reconized.");
			Debug.Log(r.getAppPermissionHTML());


		}
		catch(WebException w) {
			GameInfo.instance.menuController.GetComponent<ErrorMenu> ().loadMenu ("Web Error: " + w.Message);
			return;
		}
		catch (System.Security.Authentication.AuthenticationException ae)
		{
			GameInfo.instance.menuController.GetComponent<ErrorMenu> ().loadMenu ("Authentication Error: " + ae.Message);
			return;
		}


		GameInfo.instance.menuController.GetComponent<LogInMenu> ().unLoadMenu ();
		GameInfo.instance.menuController.GetComponent<AuthorizeMenu> ().loadMenu (token,postParams);


	}
}
