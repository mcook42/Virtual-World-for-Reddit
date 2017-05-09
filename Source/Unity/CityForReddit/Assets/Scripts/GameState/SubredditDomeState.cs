using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedditSharp.Things;
using Newtonsoft.Json.Linq;
using System.Linq;
using Graph;
using RedditSharp;

/**Caleb Whitman
 * calebrwhitman@gmail.com
 * Spring 2017
 */ 

/// <summary>
/// Stores the state of the subreddit dome.
/// This includes things like player position, current subreddit, center subreddit in the dome etc.
/// Since the player always exists within the Subreddit Dome, even when they are inside a different scene,
/// this state is static. 
/// </summary>
public class SubredditDomeState :LoginObserver  {

	public Node<Subreddit> center;
	public bool house =false; //If the house is too be loaded in.
	public bool loadNew=true; //If we are loading from the map or start as opposed to exiting a building.
	//where the player spawns
	public Vector3 playerSpawnPoint = new Vector3(0,0,0);
	public Quaternion playerSpawnRotation = new Quaternion();

	/// <summary>
	/// The subreddit that they player is inside. Null if they are outside or inside the house.
	/// </summary>
	public GameObject currentSubreddit = null;



	//Makes this class a singlton.
	private static SubredditDomeState Instance;

	#region LoginObserver
	private SubredditDomeState(){ WorldState.instance.redditRetriever.register (this);}

	/// <summary>
	/// Update the map.
	/// </summary>
	/// <param name="login">If set to <c>true</c> login.</param>
	public void notify(bool login)
	{
		foreach (var node in WorldState.instance.map) {
			node.Value.UpdateReddit (WorldState.instance.reddit);
		}

	}
	#endregion

	public static SubredditDomeState instance
	{
		get 
		{
			if (Instance == null)
			{
				Instance = new SubredditDomeState();
			}
			return Instance;
		}
	}

	/// <summary>
	/// Init the state.
	/// </summary>
	/// <param name="centerSubreddit">Center subreddit.</param>
	/// <returns>True if successfully initialized, false otherwise. </returns>
	public bool init(string centerSubreddit)
	{
		house = false;
		return loadBuildings (centerSubreddit);
	}

	/// <summary>
	/// Loads the front scene with the house in the center.
	/// </summary>
	/// <returns><c>true</c>, if house was inited, <c>false</c> otherwise.</returns>
	public void initHouse()
	{
		if (WorldState.instance.reddit.User != null)
			initFront();
		house = true;

	}

	/// <summary>
	/// Loads the front scene with the front subreddits in the center.
	/// </summary>
	public void initFront()
	{
		house = false;
		loadDefaultSubreddit (WorldState.instance.reddit.FrontPage);
	}

	/// <summary>
	/// Loads the All subreddit surrounded by the default subreddits.
	/// </summary>
	public void initAll()
	{
		house = false;
		loadDefaultSubreddit (WorldState.instance.reddit.RSlashAll);

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
			WorldState.instance.map.Remove(centerSub);

			centerNode = new Node<Subreddit>(centerSub);
			temp.AddNode (centerNode);

			//If their is a user, get their subscriptions
			Listing<Subreddit> subs=null;
			if(WorldState.instance.reddit.User!=null)
			{
				subs = WorldState.instance.reddit.User.SubscribedSubreddits;
			}

			//If there is not a user, then just get the defualt subreddits.
			if((subs==null) || (subs.Count() == 0)){

				subs = WorldState.instance.reddit.GetDefaultSubreddits();
			}
				
			//Add the nodes to the graph.
			foreach(var sub in subs)
			{
				Node<Subreddit> node = new Node<Subreddit>(sub);
				temp.AddNode(node);
				temp.AddUndirectedEdge(centerNode,node,1);

			}



		}
		catch(System.Net.WebException we) {
			WorldState.instance.menuController.GetComponent<MenuController> ().loadFatalErrorMenu("Unable to connect to Reddit Server: "+we.Message);
		} 

		WorldState.instance.map.Clear ();
		WorldState.instance.map.AddGraph (temp);
		center = WorldState.instance.map.getNode (centerSub);

		layoutMap ();

	}

	/// <summary>
	/// Loads the buildings if the given subreddit exists.
	/// </summary>
	/// <returns><c>true</c>, if buildings was loaded, <c>false</c> otherwise.</returns>
	/// <param name="subredditName">Subreddit name.</param>
	public bool loadBuildings(string subredditName)
	{
		Graph<Subreddit> temp;
		temp = WorldState.instance.server.getMap (subredditName);

		if (temp == null) {
			
			Subreddit centerSub;
			try{
				centerSub = WorldState.instance.reddit.GetSubreddit (subredditName);
			}
			catch(System.Net.WebException we) {
				WorldState.instance.menuController.GetComponent<MenuController> ().loadFatalErrorMenu("Unable to connect to Reddit Server: "+we.Message);
				return false;
			}

			if (centerSub == null)
				return false;

			temp = new Graph<Subreddit> ();
			temp.AddNode (centerSub);
		}


		WorldState.instance.map.Clear ();
		WorldState.instance.map.AddGraph (temp);
		center = WorldState.instance.map.getNode (temp.Nodes [0].Value);

		layoutMap ();

		return true;

	}


	/// <summary>
	/// Layouts the map.
	/// </summary>
	public void layoutMap()
	{
		ForceDirectedLayout fdl = new ForceDirectedLayout (1f,MapMenu.maxNodeSize);
		WorldState.instance.map = fdl.initializePositions(WorldState.instance.map, SubredditDomeState.instance.center);

	}


	/// <summary>
	/// Completely destroys/resets every stored value.
	/// Usually only called when exiting to the menu screen.
	/// </summary>
    public void reset()
    {
		center = null;
		house = false;
		loadNew = true;
		playerSpawnRotation = new Quaternion();
		currentSubreddit = null;
    }

}


	
	

