/**RedditSharpTest.cs
 * Caleb Whitman
 * November 22, 2016
 * 
 * Creates a menu item that goes to reddit and grabs the first 25 posts in /r/All. Prints these posts to the console.
 * Uses a version of RedditSharp that is compadable with the RedditAPI. 
 * The old version of RedditSharp was taken from https://github.com/waitingtocompile/HFYBotReborn
 * The code for RedditSharp was built into a library and put into Plugs/Resources. 
 * 
 * Currently, I am able to get RedditSharp to work if you pass in a Redditor's Username and Password.
 * I am not able to get RedditSharp to work with Oath2. Theoretically it should be simple, but all of my tests so far have failed.
 * Right now, I believe that I am using the various classes/methods innocorretly. 
 * The function of every class and method is documented (for the most part) but it is not completely clear how to put all of the various pieces together.
 */

using UnityEngine;
using System.Collections;
using RedditSharp;
using UnityEditor;
//ignoring variable not used warnings.
#pragma warning disable 414, 168 
public class RedditSharpTest  {



	private static string app_secret="";
	private static string user_agent = "User-Agent: UwyoSenDesign2017:v0.0.1 (by /u/mcook42)";
	private static string	app_id = "DNvP_RE1N9NqQg";
	private static string	app_uri = "https://127.0.0.1:65010/authorize_callback";
	private static string	app_scopes = "account creddits edit flair history identity livemanage modconfig modcontributors modflair modlog modothers modposts modself modwiki mysubreddits privatemessages read report save submit subscribe vote wikiedit wikiread";
	private static string	app_code = "3cV6G9Op2_3wVbXzOnU9NgEz8ns";
	private static string	app_refresh = "64281718-kVkekkFsh-YOeFKUmqlpZF0lCkE";

	//enter your own username and password here.
	private static string 	username="";
	private static string 	password="";

	[MenuItem ("RedditSharp/LoadFront")]
	public static void loadFront()
	{ 
		
		if (username == "" || password == "") {
			Debug.Log ("You need to enter a Reddit username and password in the script scripts/RedditSharpTest.cs\nSo far, I can only get RedditSharp to function if you pass in an username and password");
			return;
		}
		try{

			var reddit = new Reddit(username,password);

			var subreddit = reddit.RSlashAll;
			int stop=25;
			foreach(var post in subreddit.Hot)
			{
				Debug.Log(post.Subreddit.Name);
				if(stop<=0)
					break;
				stop--;
			}


			//Code that does not work.
			//This code is coppied directly from the github page (in Program.cs), yet it fails to login.
			//Either I am using it wrong or RedditSharp does not possess the capabilites to do what I want it to do.
			//var webAgent=new WebAgent();
			//WebAgent.UserAgent=user_agent;
			//var authProvider = new AuthProvider (app_id,app_secret, app_uri,webAgent);
			//var authTokenString = authProvider.GetOAuthToken(username, password);
		
	

		}
		catch (System.Security.Authentication.AuthenticationException ae)
		{
			Debug.Log("Login refused: "+ae.Message);
			return;
		}
		catch (System.Net.WebException we)
		{
			Debug.Log("Network error when connecting to reddit: "+we.Message);
			return;
		}




	}
		
}
