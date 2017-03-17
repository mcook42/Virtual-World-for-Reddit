using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Graph;
//using List = System.Collections.Generic.List;
using UnityEditor;


/// <summary>
/// Tests the Graph class. 
/// </summary>
[TestFixture]
public class GraphTest  {

	//The Graph that will be generated and tested.
	static int[] nodes = new int[9]{1, 2, 3, 4, 5, 6, 7, 8, 9 };

	//From, To, cost
	static int[,] directedEdges = new int[7,3]{ 
		{1,2,1},
		{2,3,1},
		{3,4,1},
		{4,2,-2},
		{1,3,-2},
		{6,3,1},
		{4,6,1}
	};
	static int[,] undirectedEdges = new int[3,3]{ 
		{5,6,2},
		{6,7,6},
		{5,7,8}
	};


	//merged Graph to be tested
	static int[] nodes2 = new int[9]{1, 2, 3, 4, 5, 6, 7, 8, 11 };

	//From, To, cost
	static int[,] directedEdges2 = new int[4,3]{ 
		{6,3,1},
		{4,6,1},
		{11,4,-10},
		{5,11,2}
	};
	static int[,] undirectedEdges2 = new int[2,3]{ 
		{5,6,2},
		{6,7,6}
	};


	/// <summary>
	/// Addes nodes/edges, checks to see if they exist, and then removes them.
	/// </summary>
	[MenuItem ("GraphTest/basicTest")]
	[Test]
	public static void basicTest()
	{
		Graph<int> graph = generateGraph ();

		int[] graphNodes = getNodes (graph);

		//Assert.AreEqual (graphNodes.Length, nodes.Length);

		if (graphNodes.Length != nodes.Length)
			Debug.Log ("graph has different length of nodes");
		for (int i = 0; i < graphNodes.Length; i++) {
			if (graphNodes [i] != nodes [i])
				Debug.Log ("Graph does not produce same nodes");
		}

		foreach (Node<int> node in graph.getNode(6).ToNeighbors) {
			//Debug.Log (node.Value);
		}

		if (graph.getNode (5).ToNeighbors.Count != 2 || graph.getNode (5).ToNeighbors.Count != 2)
			Debug.Log ("Node 5 has an incorrect number of neighbors");
		
		graph.Remove (6);

		if (graph.getNode (6) != null) {
			Debug.Log ("Node 6 should be gone");
		}
			
		foreach (Node<int> node in graph) {
			if (node.ToNeighbors.FindByValue (6) != null)
				Debug.Log ("Node 6 still exists in " + node.Value);
			if (node.FromNeighbors.FindByValue (6) != null)
				Debug.Log ("Node 6 still exists in " + node.Value);

		}
		if (graph.getNode (5).ToNeighbors.Count != 1 || graph.getNode (5).ToNeighbors.Count != 1)
			Debug.Log ("Node 5 has an incorrect number of neighbors");

		//Debug.Log (graph);
		//graph.AddGraph (graph);
		//Debug.Log (graph);

		//Graph<int> toMerge = generateGraph2 ();
		Graph<int> toMerge = new Graph<int>();

		graph.AddGraph (toMerge); 
		Debug.Log (graph);



		Debug.Log ("Test done");

	}
		
	public static Graph<int> generateGraph2()
	{
		Graph<int> graph= new Graph<int> ();

		foreach (int node in nodes2) {
			graph.AddNode (node);
		}

		for(int i =0; i< (directedEdges2.Length/3);i++) {
			Node <int> first = graph.getNode (directedEdges2 [i,0]);
			Node <int> second = graph.getNode (directedEdges2 [i,1]);

			graph.AddDirectedEdge (first, second, directedEdges2 [i,2]);

		}

		for(int i =0; i< (undirectedEdges2.Length/3);i++) {
			Node <int> first = graph.getNode (undirectedEdges2 [i,0]);
			Node <int> second = graph.getNode (undirectedEdges2 [i,1]);
			graph.AddUndirectedEdge (first, second, undirectedEdges2 [i,2]);

		}



		return graph;

	}

	public static Graph<int> generateGraph()
	{
		Graph<int> graph= new Graph<int> ();

		foreach (int node in nodes) {
			graph.AddNode (node);
		}

		for(int i =0; i< (directedEdges.Length/3);i++) {
			Node <int> first = graph.getNode (directedEdges [i,0]);
			Node <int> second = graph.getNode (directedEdges [i,1]);

			graph.AddDirectedEdge (first, second, directedEdges [i,2]);

		}

		for(int i =0; i< (undirectedEdges.Length/3);i++) {
			Node <int> first = graph.getNode (undirectedEdges [i,0]);
			Node <int> second = graph.getNode (undirectedEdges [i,1]);
			graph.AddUndirectedEdge (first, second, undirectedEdges [i,2]);

		}



		return graph;

	}

	public static int[] getNodes(Graph<int> graph)
	{
		int[] nodes = new int[graph.Count];
		int i = 0;
		foreach (Node<int> node in graph) {
			nodes [i] = node.Value;
			i++;
		}
		return nodes;

	}




}
