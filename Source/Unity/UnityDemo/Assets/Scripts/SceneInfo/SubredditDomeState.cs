using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedditSharp.Things;
using Newtonsoft.Json.Linq;
using System.Linq;
using Graph;

public class SubredditDomeState : SceneState<SubredditDomeState> {

	public Node<Subreddit> center;

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

	public bool loadFront()
	{

		int maxIterations = 50;
		Graph<Subreddit> temp =new Graph<Subreddit>();

		Subreddit centerSub;
		Node<Subreddit> centerNode;
		try{
			centerSub = GameInfo.instance.reddit.FrontPage;

			centerNode = new Node<Subreddit>(centerSub);
			temp.AddNode (centerNode);
			List<string> seenSubreddits = new List<string> ();
			int i=0;
			foreach (Post post in centerSub.Hot) {
				if (seenSubreddits.Count >= ForceDirectedLayout.innerBuildingNum + ForceDirectedLayout.outerBuildingNum)
					break;
				
				if (!seenSubreddits.Contains (post.SubredditName)) {
					Node<Subreddit> neighbor = new Node<Subreddit>(post.Subreddit);
					temp.AddNode(neighbor);
					temp.AddDirectedEdge(centerNode,neighbor,1);
				}


				if(i>maxIterations)
					break;
				i++;
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
    }

}


	
	

