using System;
using System.Collections.Generic;
using RedditSharp.Things;
using Newtonsoft.Json.Linq;
using Graph;
using UnityEngine;

/// <summary>
/// Handles communication with the server.
/// All methods may throw a ServerDownExcpetion. The excpetion should be expected and handles appropriately.
/// </summary>
public class Server
{




	public Server ()
	{

	}
		

	/// <summary>
	/// A temporary method used to similate getting subreddits from a server.
	/// Always loads AskScience
	/// </summary>
	/// <returns>The subreddits.</returns>
	/// <param name="subreddits">The url field of each subreddit.</param>
	[System.Obsolete("User getMap(string center) instead")]
	public Graph<Subreddit> getSubreddits(List<String> subreddits,String center)
	{
		Graph<Subreddit> returnGraph = new Graph<Subreddit> ();
		Subreddit tempCenter = new Subreddit ();
		tempCenter = GameInfo.instance.reddit.GetSubreddit (center);
		Node<Subreddit> centerNode = new Node<Subreddit>(tempCenter);
		returnGraph.AddNode (centerNode);

		foreach (String sub in subreddits) {

			var return_sub = new Subreddit ();
			return_sub.DisplayName = sub;


			System.Random random = new System.Random ();

			Node<Subreddit> node = new Node<Subreddit> (return_sub);
			returnGraph.AddNode(node);
			returnGraph.AddDirectedEdge (centerNode, node, Mathf.FloorToInt((float)random.NextDouble()*3));
			returnGraph.AddDirectedEdge (node, centerNode, Mathf.FloorToInt((float)random.NextDouble()*3));
		}

		return returnGraph;
	}

	/// <summary>
	/// Gets the subreddits.
	/// </summary>
	/// <returns>The subreddits if found, null otherwise</returns>
	/// <param name="subredditFullName">Subreddit full name.</param>
	[System.Obsolete("User getMap(string center) instead")]
	public Graph<Subreddit> getSubreddits(String subredditFullName)
	{

		return getMap ("");
		/**
		if (subredditFullName == "/r/askscience") {
			List<String> buildingNames = new List<String> ();

			buildingNames.Add ("/r/science"); 
			buildingNames.Add ("/r/news");
			buildingNames.Add ("/r/politics"); 
			buildingNames.Add ("/r/worldnews"); 
			buildingNames.Add ("/r/bestof"); 
			buildingNames.Add ("/r/explainitlikeimfive");
			buildingNames.Add ("/r/LifeProTips"); 
			buildingNames.Add ("/r/space"); 

			for (int i = 0; i < 100; i++) {
				buildingNames.Add ("/r/space"); 
			}

			return getSubreddits (buildingNames, subredditFullName);
		} else {

			return null;
		}
		*/

	}

	/// <summary>
	/// Gets the maps with the optional parameter of the center node.
	/// </summary>
	/// <param name="centerNode"> The Node that should be in the approximate center of the map </param>
	/// <returns>The map.</returns>
	public Graph<Subreddit> getMap(String centerName)
	{
		//Get json document from the server and then set up.

		JObject jsonForSubreddits = JObject.Parse (@"{ ""center"" : ""AskReddit"", 
		""nodes"": [""askscience"",""funny"",""todayilearned"",""science"",""worldnews"",""pics"",""IAmA"",""announcements"",""gaming"",""videos"",""movies"",""blog"",""Music"",""aww"",""news""],
	     ""edges"" : [ [ ""AskReddit"",""askscience"",5 ], [ ""AskReddit"", ""science"", 6 ] ,[ ""AskReddit"", ""videos"", 6 ] ,[ ""AskReddit"", ""aww"", 6 ],[ ""AskReddit"", ""news"", 6 ],[ ""AskReddit"", ""gaming"", 6 ], 
		[ ""todayilearned"", ""askscience"", 6 ],[ ""pics"", ""science"", 6 ],[ ""gaming"", ""science"", 6 ],[ ""AskReddit"", ""movies"", 6 ]]}");

		Graph<Subreddit> graph = new Graph<Subreddit> ();
		//Add center
		Subreddit center = new Subreddit();
		center.DisplayName = jsonForSubreddits ["center"].ToString();
		Node<Subreddit> centerNode = new Node<Subreddit> (center);
		graph.AddNode (centerNode);

		Dictionary<string,Node<Subreddit>> nodeDict = new Dictionary<string,Node<Subreddit>> ();

		nodeDict.Add(center.DisplayName,centerNode);
		//Adding Nodes
		foreach (var node in jsonForSubreddits["nodes"]) {
			Subreddit sub = new Subreddit ();
			sub.DisplayName = node.ToString ();
			Node<Subreddit> newNode = new Node<Subreddit> (sub);
			graph.AddNode (newNode);
			nodeDict.Add (node.ToString(), newNode);
		}

		//Adding the Edges
		foreach (var edge in jsonForSubreddits["edges"]) {
			List<string> list = edge.ToObject<List<string>> ();
			graph.AddUndirectedEdge (nodeDict[list [0]], nodeDict [list [1]], Int32.Parse(list [2]));
		}



		return graph;
	}

	/// <summary>
	/// Gets the map.
	/// </summary>
	/// <returns>The map.</returns>
	public Graph<Subreddit> getMap()
	{
		return getMap (null);
	}




}


