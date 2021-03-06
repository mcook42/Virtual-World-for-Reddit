﻿/**Caleb Whitman
 * calebrwhitman@gmail.com
 * Spring 2017
 */ 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedditSharp;
using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using RedditSharp.Things;

/// <summary>
/// Holds the Subreddit Scene state information and uses this infromation to setup the subreddit scene.
/// </summary>
public class SubredditSceneSetup : SceneSetUp, LoginObserver
{


	public  RedditSharp.Things.Sort sortingMethod= RedditSharp.Things.Sort.Hot;
	public int firstThreadLoaded = 1;

	public GameObject sortMenuPrefab;

    public GameObject threads=null;

	#region LoginObserver
	/// <summary>
	/// Grabs data from the Server or Reddit.
	/// Instantiates all objects using the data.
	/// Sets the player's state.
	/// Removes the loading screen.
	/// Registers with the LoginObserver.
	/// </summary>
	new void Start()
	{
		base.Start ();
		WorldState.instance.redditRetriever.register (this);
	}

	/// <summary>
	/// Unregisters the object.
	/// </summary>
	void OnDestroy()
	{
		WorldState.instance.redditRetriever.unRegister (this);
	}

	/// <summary>
	/// Resets the Reddit object in the posts and the current subreddit.
	/// </summary>
	/// <param name="login">Resets the Reddit object in the posts.</param>
	public void notify(bool login)
	{
		foreach (Transform thread in threads.transform) {

			thread.GetComponent<BuildingThread> ().thread.UpdateReddit (WorldState.instance.reddit);
		}
	}

	#endregion


    /// <summary>
    /// TODO: Calls Reddit, gets the threads, and then adds the threads to the door.
    /// </summary>
    protected override void setUpScene()
    {
        loadThreads(firstThreadLoaded,sortingMethod,false);
    }

    /// <summary>
    /// Sets the player to (0,0,0).
    /// </summary>
    protected override void setPlayerState()
    {
        WorldState.instance.player.transform.position = new UnityEngine.Vector3(0, 1, 13);
    }

	/// <summary>
	/// Checks to see if the "e" key is pressed in order to bring up the sort menu.
	/// </summary>
	void Update()
	{
		if(WorldState.instance.menuController.GetComponent<MenuController>().menusLoaded==0)
		if (Input.GetKeyDown ("e")) {
			var sortMenu = Instantiate (sortMenuPrefab);
			sortMenu.GetComponent<SortMenu> ().init (this);
		}
	}

	/// <summary>
	/// Loads the threads.
	/// </summary>
	/// <param name="firstThreadLoaded">First thread loaded or the number of next threads to load if additive is true.</param>
	/// <param name="additive">If set to <c>true</c> then the first threadLoaded will be the number of next threads to load.</param>
    public void loadThreads(int firstThreadLoaded, bool additive)
    {

        loadThreads(firstThreadLoaded, sortingMethod, additive);
        
    }

	/// <summary>
	/// Loads the threads.
	/// </summary>
	/// <param name="sortingMethod">Sorting method.</param>
    public void loadThreads(RedditSharp.Things.Sort sortingMethod)
    {
        loadThreads(firstThreadLoaded, sortingMethod, false);
    }


	/// <summary>
	/// Loads the threads. 
	/// </summary>
	/// <param name="firstThreadLoaded">First thread loaded or the number of next threads to load if additive is true.</param>
	/// <param name="sortingMethod">Sorting method.</param>
	/// <param name="additive">If set to <c>true</c> then the first threadLoaded will be the number of next threads to load.</param>
    public void loadThreads(int firstThreadLoaded, RedditSharp.Things.Sort sortingMethod, bool additive)
    {
        if (additive)
        { this.firstThreadLoaded += firstThreadLoaded; }
        else
        { this.firstThreadLoaded = firstThreadLoaded; }

        if (firstThreadLoaded < 0)
            this.firstThreadLoaded = 0;


        this.sortingMethod = sortingMethod;

        
        Reddit reddit = WorldState.instance.reddit;
        if (reddit == null)
            return;
		RedditSharp.Things.Subreddit subreddit = SubredditDomeState.instance.currentSubreddit.GetComponent<BuildingInfo>().subreddit;


        RedditSharp.Things.Post[] post;
		int threadNum = threads.transform.childCount;

		try{
	        if (sortingMethod == RedditSharp.Things.Sort.Hot)
	            post = subreddit.Hot.Take(threadNum).ToArray();
	        else if (sortingMethod == RedditSharp.Things.Sort.New)
	            post = subreddit.New.Take(threadNum).ToArray();
	        else 
				post= subreddit.GetTop(RedditSharp.Things.FromTime.All).Take(threadNum).ToArray();
		}
		catch(Exception e) {
			WorldState.instance.menuController.GetComponent<MenuController> ().loadFatalErrorMenu (e.Message);
			return;
		}


        if (threads != null)
        {
            
            int i = 0;
            foreach (Transform thread in threads.transform)
            {
				if (i > threadNum - 1 || i>post.Count()-1) {
					break;
				}

				thread.GetComponent<BuildingThread>().thread = post[i];
                thread.GetComponent<BuildingThread>().threadName = post[i].Title;
                
				//set name
                var name = thread.Find("Name").GetComponent<TextMesh>();
                if (name != null)
                    name.text = ResolveTextSize(post[i].Title, 50);
				//change size based on number of lines
				int numLines = name.text.Split('\n').Length;
				float scaleFactor = Mathf.Max ((numLines - 2)/2, 1);
				name.transform.localScale = new Vector3 (name.transform.localScale.x, name.transform.localScale.y/scaleFactor, name.transform.localScale.z);
				//set background
				thread.FindChild ("Background").localScale = new Vector3 (0.4f,1.0f , 0.9f);
                var upvotes = thread.Find("UpVotes").GetComponent<TextMesh>();
                if (upvotes!= null)
                    upvotes.text = "Upvotes:"+post[i].Upvotes;

                i++;
            }
        }
    }


	/// <summary>
	/// Adds a newline to the text at linelength.
	/// Code from: http://answers.unity3d.com/questions/190800/wrapping-a-textmesh-text.html
	/// </summary>
	/// <returns>The resolved text.</returns>
	/// <param name="input">Text to be resolved.</param>
	/// <param name="lineLength">Line length in characters.</param>
    private string ResolveTextSize(string input, int lineLength)
    {

        // Split string by char " "         
        string[] words = input.Split(" "[0]);

        // Prepare result
        string result = "";

        // Temp line string
        string line = "";

        // for each all words        
        foreach (string s in words)
        {
            // Append current word into line
            string temp = line + " " + s;

            // If line length is bigger than lineLength
            if (temp.Length > lineLength)
            {

                // Append current line into result
                result += line + "\n";
                // Remain word append into new line
                line = s;
            }
            // Append current word into current line
            else
            {
                line = temp;
            }
        }

        // Append last line into result        
        result += line;

        // Remove first " " char
        return result.Substring(1, result.Length - 1);
    }


}

