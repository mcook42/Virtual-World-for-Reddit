/**SubredditSceneState.cs
 * Author: Caleb Whitman
 * January 29, 2017
 * 
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using RedditSharp;

/// <summary>
/// Holds all information related to the subreddit scene.
/// </summary>
class SubredditSceneState : SceneState<SubredditSceneState>
{
    //The building that holds all of the subreddit information.
    public GameObject currentSubreddit = null;

    public  RedditSharp.Things.Sort sortingMethod= RedditSharp.Things.Sort.Hot;
    public int firstThreadLoaded = 1;


    /// <summary>
    /// Should never be called.
    /// </summary>
    public SubredditSceneState() {
        
    }

    /// <summary>
    /// Stores the current subreddit building gameObject.
    /// </summary>
    /// <param name="currentSubreddit">The building gameObject entered.</param>
    public void init(GameObject currentSubreddit)
    {
        this.currentSubreddit = currentSubreddit;
        currentSubreddit.SetActive(false);
    }

    /// <summary>
    /// TODO
    /// Sets all held gameObjects to null.
    /// Does not destroy the gameObjects on the chance that they will be needed again.
    /// </summary>
    public override void clear()
    {
        //currentSubreddit = null;
    }

    /// <summary>
    /// Destroys all gameObjects and resets all values.
    /// </summary>
    public override void reset()
    {
        if (currentSubreddit != null)
            GameObject.Destroy(currentSubreddit);
    }

    
}
