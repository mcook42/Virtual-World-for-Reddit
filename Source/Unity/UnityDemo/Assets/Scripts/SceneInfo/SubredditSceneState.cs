/**Caleb Whitman
 * calebrwhitman@gmail.com
 * Spring 2017
 */ 


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using RedditSharp;

/// <summary>
/// Holds all information related to the Subreddit scene.
/// </summary>
class SubredditSceneState : SceneStateSingleton<SubredditSceneState>
{
    //The building that holds all of the Subreddit information.
    public GameObject currentSubreddit = null;
    public GameObject subbredditSetup = null;

    public  RedditSharp.Things.Sort sortingMethod= RedditSharp.Things.Sort.Hot;
    public int firstThreadLoaded = 1;



    /// <summary>
    /// Stores the current Subreddit building gameObject.
    /// </summary>
    /// <param name="currentSubreddit">The building gameObject entered.</param>
    public void init(GameObject currentSubreddit)
    {
        this.currentSubreddit = currentSubreddit;
        currentSubreddit.SetActive(false);
    }
		

    /// <summary>
    /// Destroys all gameObjects and resets all values.
    /// </summary>
    public override void reset()
    {
        subbredditSetup = null;
        if (currentSubreddit != null)
            GameObject.Destroy(currentSubreddit);
    }

    
}
