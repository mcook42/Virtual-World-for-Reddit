using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedditSharp.Things;
using Newtonsoft.Json.Linq;
using System.Linq;
using Graph;
using RedditSharp;

public class SubredditDomeState : SceneStateSingleton<SubredditDomeState> {

	public Node<Subreddit> center;
	public bool house { get; set; } //If the house is too be loaded in.

	/// <summary>
	/// Init the state.
	/// </summary>
	/// <param name="centerSubreddit">Center subreddit.</param>
	/// <returns>True if successfully initialized, false otherwise. </returns>
	public bool init(string centerSubreddit)
	{
		if (System.Text.RegularExpressions.Regex.IsMatch (centerSubreddit, "(/r/)?front", System.Text.RegularExpressions.RegexOptions.None))
			return loadFront ();
		else
			return loadBuildings (centerSubreddit);

	}

	/// <summary>
	/// Loads the front scene with the house in the center.
	/// </summary>
	/// <returns><c>true</c>, if house was inited, <c>false</c> otherwise.</returns>
	public void initHouse()
	{
		if (GameInfo.instance.reddit.User != null)
			loadFront ();
		house = true;

	}

	/// <summary>
	/// Loads the front scene with the front subreddits in the center.
	/// </summary>
	public void initFront()
	{
		loadFront ();
	}

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

		GameInfo.instance.map.AddGraph (temp);
		center = GameInfo.instance.map.getNode (temp.Nodes [0].Value);

		layoutMap ();

		return true;

	}

	/// <summary>
	/// Loads the front.
	/// </summary>
	/// <returns><c>true</c>, if front was loaded, <c>false</c> otherwise.</returns>
	/// <param name="house">If set to <c>true</c> house will be loaded.</param>
	public bool loadFront()
	{

		Graph<Subreddit> temp =new Graph<Subreddit>();


		Subreddit centerSub;
		Node<Subreddit> centerNode;
		try{
			centerSub = GameInfo.instance.reddit.FrontPage;

			//If front already exists we have to get rid of it.
			GameInfo.instance.map.Remove(centerSub);

			centerNode = new Node<Subreddit>(centerSub);
			temp.AddNode (centerNode);

			//If their is a user, get their subscriptions
			Listing<Subreddit> subs=null;
			if(GameInfo.instance.reddit.User!=null)
			{
				subs = GameInfo.instance.reddit.User.SubscribedSubreddits;
			}

			//If there is not a user, then just get the defualt subreddits.
			if((subs==null) || (subs.Count() == 0)){
				 
				subs = GameInfo.instance.reddit.GetDefaultSubreddits();
			}

			//Add the nodes to the graph.
			foreach(var sub in subs)
			{
				Node<Subreddit> node = new Node<Subreddit>(sub);
				temp.AddNode(node);
				temp.AddDirectedEdge(centerNode,node,1);
			}



		}
		catch(System.Net.WebException we) {
			GameInfo.instance.menuController.GetComponent<FatalErrorMenu>().loadMenu("Unable to connect to Reddit Server: "+we.Message);
			return false;
		} 

		GameInfo.instance.map.AddGraph(temp);
		center = GameInfo.instance.map.getNode (centerSub);


		return true;
	}
		
	/// <summary>
	/// Layouts the map.
	/// </summary>
	public void layoutMap()
	{
		ForceDirectedLayout fdl = new ForceDirectedLayout (ForceDirectedLayout.StopOption.Threshold, 1,GameInfo.instance.menuController.GetComponent<MapMenu>().maxNodeSize);
		fdl.run (GameInfo.instance.map, SubredditDomeState.instance.center);

	}

    public override void clear()
    {
        
    }

    public override void reset()
    {
		center = null;
		house = false;
    }

}


	
	

