using System;
using System.Collections.Generic;
using RedditSharp.Things;
using Newtonsoft.Json.Linq;
using Graph;
using UnityEngine;
using System.Net.Sockets;
using UnityEditor;
using System.Net;
using System.Text;

/// <summary>
/// Handles communication with the server.
/// All methods may throw a ServerDownExcpetion. The excpetion should be expected and handled appropriately.
/// </summary>
public class Server
{

	private readonly string serverIP = "69.146.92.116";
	private readonly Int32 port = 4269;

	[MenuItem("Servertest/test")]
	public static void testServer()
	{
		Server server = new Server ();
		server.getJSONMap ("funny");

	}


	public Server ()
	{

	}

	/// <summary>
	/// Gets the JSON map.
	/// </summary>
	/// <returns>The JSON map.</returns>
	/// <param name="center">Center subreddit to receive.</param>
	/// <exception cref="ServerDownException">Exception thrown when problems with server communicaiton occur.</exception>
	public JObject getJSONMap(string center)
	{
		//string to be returned
		string jsonString = "";
		JObject returnJson = null;

		byte[] bytes = new byte[100];

		//store IP address and port.
		IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(serverIP), port);


		// Create a TCP/IP  socket.  
		Socket sender = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);
		// Connect the socket to the remote endpoint. Catch any errors.  
		try
		{
			sender.Connect(remoteEP);

			// Encode the data string into a byte array.  
			byte[] msg = Encoding.ASCII.GetBytes(center);

			// Send the data through the socket.  
			sender.Send(msg);

			// Receive the response from the remote device.  
			int bytesRec = bytes.Length;
			do {
				bytesRec = sender.Receive(bytes);
				jsonString += Encoding.UTF8.GetString(bytes, 0, bytesRec);
			}
			while(bytesRec == bytes.Length);


			// Release the socket.  
			sender.Shutdown(SocketShutdown.Both);
			sender.Close();

			returnJson = JObject.Parse(jsonString);

		}
		catch (ArgumentNullException)
		{
			throw new ServerDownException ("Error sending data to server");
		}
		catch (SocketException)
		{
			throw new ServerDownException ("Cannot connect to server");
		}
		catch (Exception e)
		{
			throw new ServerDownException (e.Message);
		}


		return returnJson;
	}



	/// <summary>
	/// Gets the map from the given center subreddit.
	/// If the center subreddit cannot be found, returns a map with a single node representing the center subreddit.
	/// </summary>
	/// <param name="centerNode"> The Node that should be in the center of the map </param>
	/// <returns>The map.</returns>
	public Graph<Subreddit> getMap(String centerName)
	{


		//Remove any /r/s or /s on the subreddit name.
		centerName = parseSlashR (centerName);

		//Connect to the server and get the map. Will throw an exception if server is down.
		JObject jsonForSubreddits = getJSONMap (centerName);

		//Graph to return
		Graph<Subreddit> graph = new Graph<Subreddit> ();

		//Temporary dictionary to store the nodes.
		Dictionary<string,Node<Subreddit>> nodeDict = new Dictionary<string,Node<Subreddit>> ();

		//Add center
		Subreddit center = new Subreddit();
		center.DisplayName = jsonForSubreddits ["center"].ToString();
		Node<Subreddit> centerNode = new Node<Subreddit> (center);
		graph.AddNode (centerNode);
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
		Graph<Subreddit> graph = new Graph<Subreddit> ();
		Subreddit sub = new Subreddit ();
		sub.DisplayName = "/r/AskReddit";
		graph.AddNode (sub);
		return graph;
	}

	/// <summary>
	/// Removes the /r/ and any slashes from a subreddits name. 
	/// </summary>
	/// <returns>The slash r.</returns>
	/// <param name="subreddit">Subreddit.</param>
	public string parseSlashR(string subreddit)
	{
		return subreddit.Replace ("/r/", "").Replace ("/", "");
	}



}


