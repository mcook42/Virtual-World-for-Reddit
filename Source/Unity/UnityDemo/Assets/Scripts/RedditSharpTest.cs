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
 * I am not able to get RedditSharp to work with Oath2. 
 * The code successfuly obtains an access token but then crashes when trying to instantiate a Reddit object.
 */

using UnityEngine;
using System.Collections;
using Stopwatch=System.Diagnostics.Stopwatch;
using RedditSharp;
using Scope =RedditSharp.AuthProvider.Scope;
using UnityEditor;


#pragma warning disable 414, 168 //ignoring "variable not used" warnings.
public class RedditSharpTest  {


	//enter the app secret here
	private static string 	app_secret="";

	private static string 	user_agent = "User-Agent: UwyoSenDesign2017:v0.0.1 (by /u/mcook42)";
	private static string	app_id = "DNvP_RE1N9NqQg";
	private static string	app_uri = "https://127.0.0.1:65010/authorize_callback";
	private static string	app_code = "KxpXC7clBaDYsBPU1E3JJlq77j8";
	private static string	app_refresh = "64281718-kVkekkFsh-YOeFKUmqlpZF0lCkE";
	private static string 	app_state="533swmzldjwu9a5"; 
	private static Scope 	app_scopes = Scope.edit | Scope.flair | Scope.history | Scope.identity |Scope.modconfig |Scope.modflair | Scope.modlog| Scope.modposts | Scope.modwiki|Scope.mysubreddits | Scope.privatemessages |Scope.read|Scope.report|Scope.save|Scope.submit|Scope.subscribe|Scope.vote|Scope.wikiedit|Scope.wikiread;

	//enter your own username and password here.
	private static string 	username="testUser34";
	private static string 	password="";

	[MenuItem ("RedditSharp/LoadFront")]
	public static void loadFront()
	{ 
		/*
		if (username == "" || password == "") {
			Debug.Log ("You need to enter a Reddit username and password in the script scripts/RedditSharpTest.cs\nSo far, I can only get RedditSharp to function if you pass in an username and password");
			return;
		}
		try{


			var reddit = new Reddit(username,password);
			var timer =new Stopwatch();
			timer.Start();

			var subreddit = reddit.RSlashAll;
			int stop=25;
			foreach(var post in subreddit.Hot)
			{
				Debug.Log(post.Subreddit.Name);
				if(stop<=0)
					break;
				stop--;
			}

			timer.Stop();
			Debug.Log("Seconds taken: " + (timer.ElapsedMilliseconds/1000) );

*/

			//Code that does not work.
			//This code is coppied almost directly from the github page (in Program.cs)
			//The code throws a NullPointerException when the Reddit object is encountered


		try{
			if (app_secret=="") {
				Debug.Log ("You need to enter the app_secret in the script scripts/RedditSharpTest.cs\nRemove this upon uploading to github.");
				return;
			}
			var webAgent=new WebAgent();
			WebAgent.UserAgent=user_agent;
			var authProvider = new AuthProvider (app_id,app_secret, app_uri,webAgent);

			var authTokenString = authProvider.GetOAuthToken(username, password);

			//copy and paste this url into a browser. Click accept. Copy the code in the returned url and use it as the app_code.
			//var authURL=authProvider.GetAuthUrl(app_state,app_scopes,true); Debug.Log(authURL);

			var authToken = authProvider.GetOAuthToken(app_code);
			Debug.Log(authToken);

			
			var reddit = new Reddit(authToken);//code crashes right here.


			var timer =new Stopwatch();
			timer.Start();

			var subreddit = reddit.RSlashAll;
			int stop=25;
			foreach(var post in subreddit.Hot)
			{
				Debug.Log(post.Subreddit.Name);
				if(stop<=0)
					break;
				stop--;
			}

			timer.Stop();
			Debug.Log("Seconds taken: " + (timer.ElapsedMilliseconds/1000) );





	
	



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
