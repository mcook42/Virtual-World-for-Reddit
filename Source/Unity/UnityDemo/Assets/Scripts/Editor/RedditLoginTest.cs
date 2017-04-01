using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System;
using System.Text;
using UnityEditor;

public class RedditLoginTest : MonoBehaviour {


	private static CookieContainer cookies;

	/// <summary>
	/// Just Login the user TestUser34 and print the result to Debug.
	/// </summary>
	[MenuItem("RedditLoginTest/LoginTestUser")]
	public static void TestLogin()
	{

		RedditRetriever r = new RedditRetriever ();

		var loginURL = r.getLoginURL();
		var postParams = r.getPostParams (loginURL);
		var loginToken = r.getUserJToken ("testUser34", "testUser34", postParams);
		Debug.Log (loginToken);
		var code = r.getCode (loginToken, postParams);
		Debug.Log (r.getCode(loginToken,postParams));
		r.authenticateUser (code);
		Debug.Log (r.reddit.User.FullName);


	}





}
