/**GameInfo.cs
* Caleb Whitman
* January 28, 2017
*/

using System.Collections;
using System.Collections.Generic;
using RedditSharp;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using Scope = RedditSharp.AuthProvider.Scope;
using System.Text;
using System;
using UnityEngine.SceneManagement;
using System.Net;
using RedditSharp.Things;

/// <summary>
/// A Singleton that stores commonly used gameObjects and variables.
/// This includes things like the Player, a Reddit object, and server communication code.
/// </summary>
public class GameInfo : MonoBehaviour {

    #region RedditSharp
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

    private Reddit backendReddit = null;

    public Reddit reddit {

        get {
            if (backendReddit == null)
                return backendReddit=getRedditObject();
            else
                return backendReddit;
        }
    }

	public IWebAgent webAgent;

    /// <summary>
    /// Connects to Reddit and returns the Reddit Object.
    /// If we are unable to connect to Reddit  Object, then the fatalError menu will be called and null will be returned.
    /// </summary>
    /// <returns>The Reddit Object if we are able to connect or null otherwise..</returns>
    public Reddit getRedditObject()
    {
       
        try
        {

            //WebAgent handles all http requests. The constructor does nothing and all fields must be set else where. 
             webAgent = new WebAgent();

			//Allows the webAgent to be used on Mono platforms, including Unity.
			InitializeMonoWebAgent(webAgent);

			//Sets up the rest of the webAgent. authProvider isn't really even needed after this.
            var authProvider = new AuthProvider(app_id, app_secret, app_uri, webAgent);

            //Create a new Reddit object to interface with. false indicates that there will be no user attached to the object.
			var reddit = new Reddit(webAgent, false);

            return reddit;
        }
        catch (System.Security.Authentication.AuthenticationException ae)
        {
            menuController.GetComponent<FatalErrorMenu>().loadMenu("Unable to authenticate with Reddit: "+ae.Message);
            return null;
        }
        catch (System.Net.WebException we)
        {
            menuController.GetComponent<FatalErrorMenu>().loadMenu("Unable to connect to Reddit Server: "+we.Message);
            return null;
        }
      
    }

	/// <summary>
	/// Initializes the web agent for Mono Platforms.
	/// </summary>
	/// <param name="agent">Agent.</param>
	public static void InitializeMonoWebAgent(IWebAgent agent)
	{
		ServicePointManager.ServerCertificateValidationCallback = (s, c, ch, ssl) => true;
		agent.Cookies = new CookieContainer();
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

    #endregion

	#region Server
	public Server server = new Server();

	#endregion



    public static GameInfo instance = null;

    public GameObject player;

    public GameObject menuController;

    public GameObject keyController;

    /// <summary>
    /// Initializes the Reddit Object and loads the main menu scene.
    /// </summary>
    public void Awake () 
	{
		backendReddit = getRedditObject ();
        
        if (instance == null) 
		{
			DontDestroyOnLoad (gameObject);
			instance = this;
            
		} 
		else if (instance != this) 
		{
			//ensures that only on object of this type is present at all times
			Destroy(gameObject);
		}

        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
    }



    /// <summary>
    /// Enables or disables the cursor.
    /// </summary>
    /// <param name="cursorLock">If false the cursor will appear. If true the cursor will disappear.</param>
    public void setCursorLock(bool cursorLock)
    {
        MouseLook mouseLook = GameInfo.instance.player.GetComponent<MyRigidbodyFirstPersonController>().mouseLook;
        mouseLook.SetCursorLock(cursorLock);
    }




	#region Server



	#endregion

}
	