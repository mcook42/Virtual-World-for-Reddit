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
using Graph;
using AssemblyCSharp;

/// <summary>
/// A Singleton that stores commonly used gameObjects and variables.
/// This includes things like the Player, a Reddit object, and server communication code.
/// </summary>
public class GameInfo : MonoBehaviour {



	#region RedditSharp

	public RedditRetriever redditRetriever= new RedditRetriever();
	public Reddit reddit { get { return redditRetriever.reddit; } }
	public WebAgent webAgent {get {return redditRetriever.webAgent;}}
    #endregion

	#region Server
	public Server server = new Server();

	#endregion



    public static GameInfo instance = null;

    public GameObject player;

    public GameObject menuController;

	public SceneState currentState { get; set; }

	public Graph<Subreddit> map= null;

	public bool fatalError { get; set; }

    /// <summary>
    /// Initializes the Reddit Object and loads the main menu scene.
    /// </summary>
    public void Awake () 
	{

		fatalError = false;

		initializeMap ();
        
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
	/// Initializes the map.
	/// </summary>
	public void initializeMap()
	{
			map =server.getMap();

	}
		

    /// <summary>
    /// Enables or disables the cursor.
    /// </summary>
    /// <param name="cursorLock">If false the cursor will appear. If true the cursor will disappear.</param>
    public void setCursorLock(bool cursorLock)
    {
		if (GameInfo.instance.player != null) {
			MouseLook mouseLook = GameInfo.instance.player.GetComponent<MyRigidbodyFirstPersonController> ().mouseLook;
			mouseLook.SetCursorLock (cursorLock);
		}
    }





}
	