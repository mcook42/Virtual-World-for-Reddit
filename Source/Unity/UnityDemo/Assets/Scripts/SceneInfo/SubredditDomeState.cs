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
	//where the player spawns
	public Vector3 playerSpawnPoint = new Vector3(SubredditDomeSetup.buildingFootprint,1,1);
	public Quaternion playerSpawnRotation = new Quaternion();

	/// <summary>
	/// Init the state.
	/// </summary>
	/// <param name="centerSubreddit">Center subreddit.</param>
	/// <returns>True if successfully initialized, false otherwise. </returns>
	public bool init(string centerSubreddit)
	{
			return loadBuildings (centerSubreddit);
	}

	/// <summary>
	/// Loads the front scene with the house in the center.
	/// </summary>
	/// <returns><c>true</c>, if house was inited, <c>false</c> otherwise.</returns>
	public void initHouse()
	{
		if (GameInfo.instance.reddit.User != null)
			initFront();
		house = true;

	}

	/// <summary>
	/// Loads the front scene with the front subreddits in the center.
	/// </summary>
	public void initFront()
	{

		loadDefaultSubreddit (GameInfo.instance.reddit.FrontPage);

	}

	/// <summary>
	/// Loads the All subreddit surrounded by the default subreddits.
	/// </summary>
	public void initAll()
	{
		loadDefaultSubreddit (GameInfo.instance.reddit.RSlashAll);

	}

	/// <summary>
	/// Loads the center subreddit surrounded by the default subreddits.
	/// </summary>
	/// <param name="centerSub">Center sub.</param>
	public void loadDefaultSubreddit(Subreddit centerSub)
	{
		Graph<Subreddit> temp =new Graph<Subreddit>();


		Node<Subreddit> centerNode;
		try{

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
		} 

		GameInfo.instance.map.AddGraph(temp);
		center = GameInfo.instance.map.getNode (centerSub);

	}

	/// <summary>
	/// Loads the buildings if the given subreddit exists.
	/// </summary>
	/// <returns><c>true</c>, if buildings was loaded, <c>false</c> otherwise.</returns>
	/// <param name="subredditName">Subreddit name.</param>
	public bool loadBuildings(string subredditName)
	{
		Graph<Subreddit> temp;
		temp = GameInfo.instance.server.getMap (subredditName);

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


		GameInfo.instance.map.Clear ();
		GameInfo.instance.map.AddGraph (temp);
		center = GameInfo.instance.map.getNode (temp.Nodes [0].Value);

		layoutMap ();

		return true;

	}


	/// <summary>
	/// Layouts the map.
	/// </summary>
	public void layoutMap()
	{
		ForceDirectedLayout fdl = new ForceDirectedLayout (ForceDirectedLayout.StopOption.Threshold, 0.01f,GameInfo.instance.menuController.GetComponent<MapMenu>().maxNodeSize);
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


	
	

