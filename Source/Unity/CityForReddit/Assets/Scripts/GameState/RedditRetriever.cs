using System;
using Scope = RedditSharp.AuthProvider.Scope;
using RedditSharp.Things;
using RedditSharp;
using System.Net;
using System.Text;
using System.IO;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Collections.Generic;
using HtmlAgilityPack;
using System.Collections;
using System.Threading.Tasks;

/**Caleb Whitman
 * calebrwhitman@gmail.com
 * Spring 2017
 */ 


/// <summary>
/// A class with a bunch of methods and fields to get the needed Reddit Object.
/// Provides methods to get a logged in and non-logged in Reddit Object.
/// A non-logged in reddit is created on construction, just access the Reddit field.
/// Most of the methods in this class have the possibility to return some sort of web exception.
/// Updates LoginObservers whenever the user logs in.
/// </summary>
public class RedditRetriever:LoginObservable
{




	//For installed applications, the app_secret must always be an empty string.
	private string appSecret = "";

	//These values are all retrieved from the application account.
	private string userAgent = "User-Agent: UwyoSenDesign2017-InstalledApp:v0.0.1 (by /u/3DWorldForReddit)";
	private string appId = "pQnwWWwHYJGFnQ";
	private string appUri = "https://127.0.0.1:65010/authorize_callback";
	private Scope appCcopes = Scope.edit | Scope.flair | Scope.history | Scope.identity | Scope.modconfig | Scope.modflair | Scope.modlog | Scope.modposts | Scope.modwiki | Scope.mysubreddits | Scope.privatemessages | Scope.read | Scope.report | Scope.save | Scope.submit | Scope.subscribe | Scope.vote | Scope.wikiedit | Scope.wikiread;

	//This value will be created by the code before a request.
	private static string appState = "";

	//RedditSharp Stuff
	public Reddit reddit = null;
	public WebAgent webAgent;
	public AuthProvider authProvider;

	//Web Stuff
	HttpWebRequest request = null;
	CookieContainer cookies = new CookieContainer();
	private string cookieHeader = "";

	//URLs
	private readonly string logInUrl = "https://www.reddit.com/api/login/";
	private readonly string authorizeUrl = "https://ssl.reddit.com/api/v1/authorize";

	/// <summary>
	/// Initializes a new instance of the <see cref="RedditRetriever"/> class.
	/// Initializes the Reddit and web agent object.
	/// </summary>
	public RedditRetriever ()
	{
		//WebAgent has a bunch of functions and parameters used for making requests.
		//Most of the parameters are static and not set in the contructor. 
		//This means that a new WebAgent object is unstable until is is passed into AuthProvider.
		webAgent = new WebAgent();
		WebAgent.EnableRateLimit = true;
		//Allows many objects to be loaded in bursts. Draw back is that we have to wait longer between requests.
		WebAgent.RateLimit = WebAgent.RateLimitMode.Burst;


		reddit = getRedditObject ();

	}



	/// <summary>
	/// Connects to Reddit and returns the Reddit Object.
	/// If we are unable to connect to Reddit, then the FatalError menu will be instantiated and null will be returned.
	/// </summary>
	/// <returns>The Reddit Object if we are able to connect or null otherwise.</returns>
	public Reddit getRedditObject()
	{

		try
		{
			//Sets up the rest of the webAgent. authProvider isn't really even needed after this.
			authProvider = new AuthProvider(appId, appSecret, appUri, webAgent);

			//Create a new Reddit object to interface with. false indicates that there will be no user attached to the object.
			var reddit = new Reddit(webAgent, false);

			return reddit;
		}
		catch (System.Security.Authentication.AuthenticationException ae)
		{
			WorldState.instance.menuController.GetComponent<MenuController> ().loadFatalErrorMenu("Unable to authenticate with Reddit: "+ae.Message);
			return null;
		}
		catch (System.Net.WebException we)
		{
			WorldState.instance.menuController.GetComponent<MenuController> ().loadFatalErrorMenu("Unable to connect to Reddit Server: "+we.Message);
			return null;
		}

	}
		

	///<summary>Generates a random string of alphaNumberic characters.</summary>
	/// <param name="length">The lenght of the resulting string. </param> 
	/// <returns>A random string.</returns>
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

	/// <summary>
	/// Gets the login URL.
	/// </summary>
	/// <returns>The login URL.</returns>
	public string getLoginURL()
	{
		

		//This class handles all of the authentication requests.
		authProvider = new AuthProvider(appId, appSecret, appUri, webAgent);
		appState = CreateState (20);

		//This URL provides the user with a login page to login to our application.
		var authURL = authProvider.GetAuthUrl(appState, appCcopes, true);


		return authURL;

	}

	/// <summary>
	/// Gets most of the parameters used later in the post request to log the user in.
	/// The parameters will be in the format &param1=value...
	/// </summary>
	/// <returns>The post parameters</returns>
	/// <param name="loginURL">Login UR.</param>
	public string getPostParams(string url) 
	{

		string returnData = string.Empty;

		//Make the request to get the login destination webpage.
		request = (HttpWebRequest)WebRequest.Create(new Uri(url));
		request.Method = "GET";
		request.ContentType = "application/x-www-form-urlencoded";
		request.UserAgent = "Mozilla/5.0 (.NET CLR 2.0) "+userAgent;
		request.Referer = "https://www.reddit.com/";
		request.AllowAutoRedirect = true;
		request.KeepAlive = true;
		request.CookieContainer = cookies;

		//Actually make the request.
		using (HttpWebResponse response = (HttpWebResponse)request.GetResponse ()) {
			returnData = response.ResponseUri.Query;

		}

		//Replace the question mark at the start with an &
		returnData = "&"+returnData.Substring(1);


		return returnData;
	}


	/// <summary>
	/// Logs the user in and returns a JToken with the hash representing the user.
	/// Also adds elements to the Cookies which are used later.
	/// </summary>
	/// <returns>The in.</returns>
	/// <param name="username">Username.</param>
	/// <param name="password">Password.</param>
	/// <param name="postParams">Post parameters.</param>
	public JToken getUserJToken(string username, string password,string postParams)
	{

		string returnData = string.Empty;

		//Make the Request to log the user in.
		request = (HttpWebRequest)WebRequest.Create(new Uri(logInUrl+username));
		request.Method = "POST";
		request.ContentType = "application/x-www-form-urlencoded";
		request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; .NET CLR 2.0) Gecko/20100101";
		request.Referer = "https://www.reddit.com/";
		request.AllowAutoRedirect = true;
		request.KeepAlive = true;
		request.CookieContainer = cookies;

		StringBuilder postData = new StringBuilder();
		postData.Append("op=login");
		postData.Append(postParams);
		postData.Append(String.Format("&user={0}",username));
		postData.Append(String.Format("&passwd={0}",password));
		postData.Append("&api_type=json");

		//Writing the POST data to the stream.
		using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
			writer.Write(postData.ToString());

		//Getting back the response
		using (var response = (HttpWebResponse)request.GetResponse ()) {
			cookieHeader = response.Headers["Set-cookie"];
			cookies.Add (response.Cookies);
			//Read the json document that we retrieve after sending the request
			using (StreamReader reader = new StreamReader (response.GetResponseStream ()))
				returnData = reader.ReadToEnd ();
		}
		JToken outer = JToken.Parse(returnData);
		var inner = outer["json"];
		JToken data = inner["data"];
		return data;

	}

	/// <summary>
	/// Gets the app scope descritions for the Authroization menu. The scopes returned are only the ones in the app_scopes variable.
	/// </summary>
	/// <returns>A Dictionary where the scopes are the keys and the values are the descriptions.</returns>
	public Dictionary<string,string> getAppScopeDescriptions()
	{
		string json= string.Empty;

		//Make the request to get the login destination webpage.
		request = (HttpWebRequest)WebRequest.Create(new Uri("https://www.reddit.com/api/v1/scopes"));
		request.Method = "GET";
		request.UserAgent = "Mozilla/5.0 (.NET CLR 2.0) "+userAgent;
		request.Referer = "https://www.reddit.com/";
		request.AllowAutoRedirect = true;
		request.KeepAlive = true;
		request.CookieContainer=cookies;
		request.Headers.Add("Cookie", cookieHeader);


		//Actually make the request.
		using (HttpWebResponse response = (HttpWebResponse)request.GetResponse ()) {

			using (StreamReader streamerReader = new StreamReader(response.GetResponseStream ())) {
				json = streamerReader.ReadToEnd ();
			}

		}

		//Parsing the JSON.
		Dictionary<string,string> dict = new Dictionary<string,string> ();
		JToken scopes = JToken.Parse(json);

		string[] appScopeStrings = appCcopes.ToString ().Split (", ".ToCharArray (),StringSplitOptions.RemoveEmptyEntries);
		foreach (string scope in appScopeStrings) {
			dict.Add (scope, scopes [scope] ["description"].ToString());
		}

		return dict;

	}

	/// <summary>
	/// Returns the code used to get the access token.
	/// </summary>
	/// <returns>The code.</returns>
	/// <param name="data">Data.</param>
	/// <exception cref="WebException"></exception>
	public string getCode(JToken data,string postParams)
	{
		string returnData = string.Empty;

		//var postParamDic =  ParseQueryString (postParams);

		//Make the Request to log the user in.
		request = (HttpWebRequest)WebRequest.Create(new Uri(authorizeUrl));
		request.Method = "POST";
		request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; .NET CLR 2.0)";
		request.ContentType = "application/x-www-form-urlencoded";
		request.Referer = "https://ssl.reddit.com/";
		request.AllowAutoRedirect = true;
		request.KeepAlive = true;
		request.Timeout = 5000;

		//Setting the Cookies. Non-Mono platforms do this automatically.
		var cookieHeader = cookies.GetCookieHeader(new Uri("https://reddit.com"));
		request.Headers.Set("Cookie", cookieHeader);
		request.CookieContainer = cookies;

		//building the post data.
		var postData = new StringBuilder();
		postData.Append("client_id="+appId);
		postData.Append("&redirect_uri=https%3A%2F%2F127.0.0.1%3A65010%2Fauthorize_callback");
		postData.Append("&scope="+appCcopes.ToString().Replace(", ","+"));
		postData.Append(String.Format("&state={0}",appState));
		postData.Append("&response_type=code");
		postData.Append("&duration=permanent");
		postData.Append(String.Format("&uh={0}",data["modhash"]));
		postData.Append("&authorize=Allow");

		//Writing the POST data to the stream.
		using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
			writer.Write(postData.ToString());

		NameValueCollection codeAndState = null;
		HttpWebResponse response = null;
		try
		{
			//This will fail since our call back URI doesn't go anywhere.
			response = (HttpWebResponse)request.GetResponse();
		}
		//This will also be called since we our return uri doesn't actually connect to anything.
		catch(WebException w)
		{
			
			returnData = request.Address.Query;
			codeAndState = ParseQueryString (returnData);

			//Check to see if the exception indicated a success.
			if (codeAndState ["code"] == null || codeAndState ["state"] == null)
				throw w;
			
			request.Abort ();
		}
		finally{
			//close the response
			if (response != null)
				response.Close ();
		}

		//Check to make sure that the state didn't change.
		if (codeAndState ["state"] != appState)
			throw new WebException ("App state was changed!");
		

		return codeAndState["code"];

	}

	/// <summary>
	/// Authenticates the user for the given code.
	/// The authenticated user is stored in the reddit object.
	/// </summary>
	/// <param name="code">Code.</param>
	public void authenticateUser(string code)
	{
		var authToken = authProvider.GetOAuthToken (code);
		reddit = new Reddit (authToken);
		notifyObservers (true);

	}

	/// <summary>
	/// Logout the user.
	/// </summary>
	public void logout()
	{
		reddit = getRedditObject ();
		notifyObservers (false);
	}

	/// <summary>
	/// Parses the query string.
	/// </summary>
	/// <returns>The query string.</returns>
	/// <param name="s">S.</param>
	public NameValueCollection ParseQueryString(string s)
	{
		NameValueCollection nvc = new NameValueCollection ();

		// remove anything other than query string from url
		if (s.Contains ("?")) {
			s = s.Substring (s.IndexOf ('?') + 1);
		}

		foreach (string vp in Regex.Split(s, "&")) {
			string[] singlePair = Regex.Split (vp, "=");
			if (singlePair.Length == 2) {
				nvc.Add (singlePair [0], singlePair [1]);
			} else {
				// only one key with no value specified in query string
				nvc.Add (singlePair [0], string.Empty);
			}
		}

		return nvc;
	}

	#region LoginObserverable

	List<LoginObserver> observers = new List<LoginObserver> ();

	/// <summary>
	/// Register the specified observer.
	/// </summary>
	/// <param name="anObserver">An observer.</param>
	public void register (LoginObserver anObserver)
	{
		observers.Add (anObserver);
	}

	/// <summary>
	/// Unregister an observer.
	/// </summary>
	/// <param name="anObserver">An observer.</param>
	public void unRegister (LoginObserver anObserver)
	{
		observers.Remove (anObserver);

	}

	/// <summary>
	/// Notifies the ovservers that a user has logged in or out.
	/// </summary>
	/// <param name="login">Set to true when a user logs in and false when a user logs out.</param>
	private void notifyObservers(bool login)
	{
		foreach (var observer in observers) {

			observer.notify (login);
		}
	}

	#endregion

	/// <summary>
	/// Reloads the subreddit.
	/// </summary>
	/// <param name="displayName">Subreddit display name.</param>
	/// <returns>The subreddit reloaded</returns>
	public Subreddit reloadSubreddit(string displayName)
	{
		if (displayName == "Front Page")
			return reddit.FrontPage;
		if (displayName == "/r/all")
			return reddit.RSlashAll;
		return reddit.GetSubreddit (displayName);
	}

}


