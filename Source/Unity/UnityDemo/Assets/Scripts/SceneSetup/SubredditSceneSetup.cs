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


    /// <summary>
    /// TODO: Calls Reddit, gets the threads, and then adds the threads to the door.
    /// </summary>
    protected override void setUpScene()
    {
        loadThreads(SubredditSceneState.instance.firstThreadLoaded,SubredditSceneState.instance.sortingMethod,false);

        GameInfo.instance.menuController.GetComponent<LocationPanel>().loadPanel(SubredditSceneState.instance.currentSubreddit.GetComponent<Subreddit>().subredditName);
    }

    /// <summary>
    /// Sets the player to (0,0,0).
    /// </summary>
    protected override void setPlayerState()
    {
        GameInfo.instance.player.transform.position = new UnityEngine.Vector3(0, 1, 0);
    }

    public void loadThreads(int firstThreadLoaded, bool additive)
    {

        loadThreads(firstThreadLoaded, SubredditSceneState.instance.sortingMethod, additive);
        
    }

    public void loadThreads(RedditSharp.Things.Sort sortingMethod)
    {
        loadThreads(SubredditSceneState.instance.firstThreadLoaded, sortingMethod, false);
    }


    public void loadThreads(int firstThreadLoaded, RedditSharp.Things.Sort sortingMethod, bool additive)
    {
        if (additive)
        { SubredditSceneState.instance.firstThreadLoaded += firstThreadLoaded; }
        else
        { SubredditSceneState.instance.firstThreadLoaded = firstThreadLoaded; }

        if (SubredditSceneState.instance.firstThreadLoaded < 0)
            SubredditSceneState.instance.firstThreadLoaded = 0;


        SubredditSceneState.instance.sortingMethod = sortingMethod;

        if (threads != null)
        {

            int i = SubredditSceneState.instance.firstThreadLoaded;

            foreach (Transform thread in threads.transform)
            {
                //Add call to reddit here.
                thread.GetComponent<Thread>().thread = null;
                thread.GetComponent<Thread>().threadName = "Thread" + i;

                //set the name on the Thread
                var name = thread.Find("Name").GetComponent<TextMesh>();
                if(name!=null)
                 name.text = "Thread " + i+", Sorting: "+sortingMethod;
                i++;
            }
        }
    }



}

