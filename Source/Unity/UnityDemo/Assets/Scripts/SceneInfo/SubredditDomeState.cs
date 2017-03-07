using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedditSharp.Things;
using Newtonsoft.Json.Linq;
using GenericGraph;

public class SubredditDomeState : SceneState<SubredditDomeState> {

	public Graph<Subreddit> buildings { get; set; }
	public Node<Subreddit> center;

	/// <summary>
	/// Loads the buildings if the given subreddit exists.
	/// </summary>
	/// <returns><c>true</c>, if buildings was loaded, <c>false</c> otherwise.</returns>
	/// <param name="subredditName">Subreddit name.</param>
	public bool loadBuildings(string subredditName)
	{
		Graph<Subreddit> temp;
		temp = GameInfo.instance.server.getSubreddits (subredditName);

		if (temp == null) {
			
			Subreddit centerSub;
			try{
				centerSub = GameInfo.instance.reddit.GetSubreddit (subredditName);
			}
			catch(System.Net.WebException we) {
				GameInfo.instance.menuController.GetComponent<FatalErrorMenu>().loadMenu("Unable to connect to Reddit Server: "+we.Message);
				return false;
			}

			if (centerSub == null)
				return false;

			temp = new Graph<Subreddit> ();
			temp.AddNode (centerSub);
		} 
			buildings = temp;
			center = buildings.Nodes [0];
			return true;

	}
		

    public override void clear()
    {
        
    }

    public override void reset()
    {
		if(buildings!=null)
		buildings.Clear ();
    }

}


	
	

