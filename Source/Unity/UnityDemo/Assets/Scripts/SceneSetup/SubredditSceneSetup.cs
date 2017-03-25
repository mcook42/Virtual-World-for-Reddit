/**SubredditSceneSetup.cs
 * Author: Caleb Whitman
 * January 29, 2017
 * 
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedditSharp;
using UnityEngine;

/// <summary>
/// Takes the scene threadsParent and initializes the thread doors.
/// </summary>
class SubredditSceneSetup : SceneSetUp
{

    public GameObject threads=null;

	protected override void setCurrentState()
	{
		GameInfo.instance.currentState = SubredditSceneState.instance;
	}
    /// <summary>
    /// TODO: Calls Reddit, gets the threads, and then adds the threads to the door.
    /// </summary>
    protected override void setUpScene()
    {
        SubredditSceneState.instance.subbredditSetup = this.gameObject;
        loadThreads(SubredditSceneState.instance.firstThreadLoaded,SubredditSceneState.instance.sortingMethod,false);
		GameInfo.instance.menuController.GetComponent<LocationPanel>().loadPanel(SubredditSceneState.instance.currentSubreddit.transform.GetComponent<BuildingInfo>().subreddit.DisplayName);
    }

    /// <summary>
    /// Sets the player to (0,0,0).
    /// </summary>
    protected override void setPlayerState()
    {
        GameInfo.instance.player.transform.position = new UnityEngine.Vector3(0, 1, 0);
    }

	/// <summary>
	/// Loads the threads.
	/// </summary>
	/// <param name="firstThreadLoaded">First thread loaded or the number of next threads to load if additive is true.</param>
	/// <param name="additive">If set to <c>true</c> then the first threadLoaded will be the number of next threads to load.</param>
    public void loadThreads(int firstThreadLoaded, bool additive)
    {

        loadThreads(firstThreadLoaded, SubredditSceneState.instance.sortingMethod, additive);
        
    }

	/// <summary>
	/// Loads the threads.
	/// </summary>
	/// <param name="sortingMethod">Sorting method.</param>
    public void loadThreads(RedditSharp.Things.Sort sortingMethod)
    {
        loadThreads(SubredditSceneState.instance.firstThreadLoaded, sortingMethod, false);
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
        { SubredditSceneState.instance.firstThreadLoaded += firstThreadLoaded; }
        else
        { SubredditSceneState.instance.firstThreadLoaded = firstThreadLoaded; }

        if (SubredditSceneState.instance.firstThreadLoaded < 0)
            SubredditSceneState.instance.firstThreadLoaded = 0;


        SubredditSceneState.instance.sortingMethod = sortingMethod;

        
        Reddit reddit = GameInfo.instance.reddit;
        if (reddit == null)
            return;
		RedditSharp.Things.Subreddit subreddit = reddit.GetSubreddit("/r/"+SubredditSceneState.instance.currentSubreddit.GetComponent<BuildingInfo>().subreddit.DisplayName);


        RedditSharp.Things.Post[] post;
        if (sortingMethod == RedditSharp.Things.Sort.Hot)
            post = subreddit.Hot.Take(12).ToArray();
        else if (sortingMethod == RedditSharp.Things.Sort.New)
            post = subreddit.New.Take(12).ToArray();
        else 
            post= subreddit.GetTop(RedditSharp.Things.FromTime.All).Take(12).ToArray();




        if (threads != null)
        {
            
            int i = 0;
            foreach (Transform thread in threads.transform)
            {
				thread.GetComponent<BuildingThread>().thread = post[i];
                thread.GetComponent<BuildingThread>().threadName = post[i].Title;

                
                var name = thread.Find("Name").GetComponent<TextMesh>();
                if (name != null)
                    name.text = ResolveTextSize(post[i].Title, 55);

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

