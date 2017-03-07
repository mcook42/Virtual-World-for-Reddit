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
		buildings = GameInfo.instance.server.getSubreddits (subredditName);
		center = buildings.Nodes [0];
		return true;
	}
		

    public override void clear()
    {
        
    }

    public override void reset()
    {
		buildings.Clear ();
    }

}


	
	

