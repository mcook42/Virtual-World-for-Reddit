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
using Stopwatch = System.Diagnostics.Stopwatch;
using RedditSharp;
using Scope = RedditSharp.AuthProvider.Scope;
using UnityEditor;
using System.Text;
using System.Net;


#pragma warning disable 0219,414, 168 //ignoring "variable not used" warnings.
public class RedditSharpTest  {


    //For installed applications, the app_secret must always be an empty string.
    private static string app_secret = "";

    //These values are all retrieved from the application account.
    private static string user_agent = "User-Agent: UwyoSenDesign2017-InstalledApp:v0.0.1 (by /u/3DWorldForReddit)";
    private static string app_id = "pQnwWWwHYJGFnQ";
    private static string app_uri = "https://127.0.0.1:65010/authorize_callback";
    private static Scope app_scopes = Scope.edit | Scope.flair | Scope.history | Scope.identity | Scope.modconfig | Scope.modflair | Scope.modlog | Scope.modposts | Scope.modwiki | Scope.mysubreddits | Scope.privatemessages | Scope.read | Scope.report | Scope.save | Scope.submit | Scope.subscribe | Scope.vote | Scope.wikiedit | Scope.wikiread;

    //These values will be retrieved from the code as needed.
    private static string app_code = "";
    private static string app_refresh = "";
    private static string app_state = "";

    //enter your own username and password here.
    private static string 	username="testUser34";
	private static string 	password="testUser34";

	[MenuItem ("RedditSharp/LoadFront")]
	public static void loadFront()
	{

        app_state = CreateState(20);

        try
        {


            NonLoginLoad();


        }
        catch (System.Security.Authentication.AuthenticationException ae)
        {
            System.Console.WriteLine("Login refused: " + ae.Message);
            return;
        }
        catch (System.Net.WebException we)
        {
            System.Console.WriteLine("Network error when connecting to reddit: " + we.Message);
            return;
        }


    }

    //Generates a random string of alphaNumberic characters.
    public static string CreateState(int length)
    {
        const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        StringBuilder res = new StringBuilder();
        System.Random rnd = new System.Random();
        while (0 < length--)
        {
            res.Append(valid[rnd.Next(valid.Length)]);
        }
        return res.ToString();
    }

    /*
  * Retrieves some subreddit data from Reddit using our application.
  */
    public static void NonLoginLoad()
    {
        //Web Agent handles all of the requests. The Constructor does abolutely nothing and so everything must be initialzed outside of it.
		var webAgent = new WebAgent();

		//Without this method the web agent will not work on Mono platforms. Including Unity.
		InitializeMonoWebAgent (webAgent);
		//This initialezes the rest fo the web agent. You don't really even have to use the authProvider after this.
        var authProvider = new AuthProvider(app_id, app_secret, app_uri, webAgent);

        //Create a new Reddit object to interface with. false indicates that there will be no user attached to the object.
        var reddit = new Reddit(webAgent,false);


        //Get Reddit info.
        var subreddit = reddit.GetSubreddit("/r/AskReddit");

        int i = 0;
        foreach (var post in subreddit.Hot)
        {
            Debug.Log(post.Title);

            if (i > 24)
                break;
            i++;
        }

    }

	/// <summary>
	/// Initializes the web agent for Mono Platforms..
	/// </summary>
	/// <param name="agent">Agent.</param>
	public static void InitializeMonoWebAgent(WebAgent agent)
	{
		ServicePointManager.ServerCertificateValidationCallback = (s, c, ch, ssl) => true;
		agent.Cookies = new CookieContainer();
	}

}
