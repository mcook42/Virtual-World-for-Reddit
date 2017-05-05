using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using Newtonsoft.Json.Linq;

/**Caleb Whitman
 * calebrwhitman@gmail.com
 * Spring 2017
 */ 

/// <summary>
/// A simple menu that gets a user's username and password before sending them to the authorization menu.
/// </summary>
public class LogInMenu : Menu {

	public InputField username;
	public InputField password;

	public GameObject authorizationPrefab;
	/// <summary>
	/// Cancel the log in.
	/// </summary>
	public void cancel()
	{
		Destroy (gameObject);

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


		}
		catch(WebException w) {
			GameInfo.instance.menuController.GetComponent<MenuController> ().loadErrorMenu("Web Error: " + w.Message);
			return;
		}
		catch (System.Security.Authentication.AuthenticationException ae)
		{
			GameInfo.instance.menuController.GetComponent<MenuController> ().loadErrorMenu("Authentication Error: " + ae.Message);
			return;
		}

		var authorizationMenu = Instantiate (authorizationPrefab);
		authorizationMenu.GetComponent<AuthorizeMenu>().init (token, postParams,user);

		Destroy (gameObject);

	}
}
