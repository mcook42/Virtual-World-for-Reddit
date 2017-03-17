using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace Graph
{
	/// <summary>
	/// A generic graph class that can hold nodes, undirected edges, and directed edges of an arbitrary type.
	/// Code modified from https://msdn.microsoft.com/en-us/library/ms379574(v=vs.80).aspx
	/// </summary>
	public class Graph<T> : IEnumerable<Node<T>>
	{
		private NodeList<T> nodeSet;

		/// <summary>
		/// Initializes a new instance of the <see cref="MapGraph.MapGraph`1"/> class.
		/// </summary>
		public Graph() : this(null) {}
		/// <summary>
		/// Initializes a new instance of the <see cref="MapGraph.MapGraph`1"/> class.
		/// </summary>
		/// <param name="nodeSet">Node set.</param>
		public Graph(NodeList<T> nodeSet)
		{
			if (nodeSet == null)
				this.nodeSet = new NodeList<T>();
			else
				this.nodeSet = nodeSet;
		}

		/// <summary>
		/// Adds the node.
		/// Constant time operation.
		/// </summary>
		/// <param name="node">Node.</param>
		public void AddNode(Node<T> node)
		{
			// adds a node to the graph
			nodeSet.Add(node);
		}

		/// <summary>
		/// Adds the node.
		/// constant time operation.
		/// </summary>
		/// <param name="value">Value.</param>
		public void AddNode(T value)
		{
			// adds a node to the graph
			nodeSet.Add(new Node<T>(value));
		}

		/// <summary>
		/// Adds the directed edge.
		/// Constant time operation.
		/// </summary>
		/// <param name="from">Beginning node.</param>
		/// <param name="to">Ending node.</param>
		/// <param name="cost">Cost.</param>
		public void AddDirectedEdge(Node<T> from, Node<T> to, int cost)
		{
			from.ToNeighbors.Add(to);
			from.Costs.Add(cost);

			to.FromNeighbors.Add (from);
		}

		/// <summary>
		/// Adds the undirected edge.
		/// Note: In this implementation an undirected edge is equivalent to two directed edges.
		/// Constant time operation.
		/// </summary>
		/// <param name="from">Beginning node.</param>
		/// <param name="to">Ending node.</param>
		/// <param name="cost">Cost.</param>
		public void AddUndirectedEdge(Node<T> from, Node<T> to, int cost)
		{
			from.ToNeighbors.Add(to);
			from.Costs.Add(cost);
			from.FromNeighbors.Add (to);

			to.FromNeighbors.Add (from);
			to.ToNeighbors.Add(from);
			to.Costs.Add(cost);
		}

		/// <summary>
		/// Finds the specific node.
		/// Order n operation.
		/// </summary>
		/// <param name="value">Node to find</param>
		/// <returns> True is the node is found. False otherwise.</returns>
		public bool Contains(T value)
		{
			return nodeSet.FindByValue(value) != null;
		}

		/// <summary>
		/// Gets the node.
		/// Order n operation.
		/// </summary>
		/// <returns>The node or null if not found.</returns>
		/// <param name="value">Value.</param>
		public Node<T> getNode(T value)
		{
			return nodeSet.FindByValue (value);
		}

		/// <summary>
		/// Remove the specified value.
		/// Order n operation.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <returns> True if found. False otherwise. </returns>
		public bool Remove(T value)
		{
			// first remove the node from the nodeset
			Node<T> nodeToRemove = (Node<T>) nodeSet.FindByValue(value);
			if (nodeToRemove == null)
				// node wasn't found
				return false;


			// otherwise, the node was found

			//Remove references from this node to other nodes
			foreach (Node<T> node in nodeToRemove.ToNeighbors) {
				node.FromNeighbors.Remove (nodeToRemove);
			}

			//Remove references from other nodes to this node.
			foreach (Node<T> node in nodeToRemove.FromNeighbors) {

				int index = node.ToNeighbors.IndexOf (nodeToRemove);
				node.ToNeighbors.Remove (nodeToRemove);
				node.Costs.RemoveAt (index);
				
			}
				
			nodeToRemove.ToNeighbors.Clear ();
			nodeToRemove.FromNeighbors.Clear ();

			nodeSet.Remove(nodeToRemove);

			return true;
		}

		/// <summary>
		/// Removes all nodes.
		/// Order n operation.
		/// </summary>
		public void Clear()
		{
			//remove all edges
			foreach (Node<T> gnode in nodeSet)
			{
				gnode.ToNeighbors.Clear ();
				gnode.FromNeighbors.Clear ();
				gnode.Costs.Clear ();
			}

			//clear the node set.
			nodeSet.Clear ();

		}

		/// <summary>
		/// Gets the nodes.
		/// </summary>
		/// <value>The nodes.</value>
		public NodeList<T> Nodes
		{
			get
			{
				return nodeSet;
			}
		}

		/// <summary>
		/// Gets the number of nodes.
		/// </summary>
		/// <value>The count.</value>
		public int Count
		{
			get { return nodeSet.Count; }
		}

		/// <summary>
		/// Adds the graph to this one.
		/// The resulting graph is the Union of the two graphs. 
		/// This algorithm assumes that an edge from one node to another only exists once.
		/// If the cost between two edges is different, the cost on this graph is kept.
		/// This is an order n+m operation,
		/// </summary>
		/// <param name="graph">Graph.</param>
		public void AddGraph(Graph<T> graph)
		{
			foreach(Node<T> node in graph.nodeSet)
			{
				Node<T> existing;
				if (this.getNode (node.Value) != null) { //node already exists

					//have to merge the two nodes now.

					existing = this.getNode (node.Value);


				} else { //node not found

					existing = new Node<T> (node.Value);
					this.AddNode (existing);

				}


				//find the nodes from this node to other and see if they already exist in the graph.
				int i =0;
				foreach (Node<T> to in node.ToNeighbors) {
					//Neighbor does not already exist
					if (existing.ToNeighbors.FindByValue (to.Value) == null) {

						//see if they exist in the overall graph
						if (this.getNode (to.Value) == null) {

							Node<T> newNode = new Node<T> (to.Value);
							this.AddNode (newNode);
							this.AddDirectedEdge (existing, newNode, node.Costs [i]);

						} else {
							this.AddDirectedEdge (existing, this.getNode (to.Value), node.Costs [i]);

						}

					} 

					i++;
				}
			}

		}

		public override string ToString()
		{
			string returnValue="";
			foreach (Node<T> node in nodeSet) {
				returnValue += node.Value.ToString();
				returnValue += " : {";
				int i = 0;
				foreach (Node<T> to in node.ToNeighbors) {
					returnValue+=to.Value.ToString()+"(" +node.Costs[i]+ "), ";
					i++;
				}
						returnValue+="}\n";
			}
			return returnValue;

		}


		IEnumerator IEnumerable.GetEnumerator()
		{
			// call the generic version of the method
			return this.GetEnumerator();
		}

		public IEnumerator<Node<T>> GetEnumerator()
		{
			for (int i = 0; i < nodeSet.Count; i++)
				yield return nodeSet[i];
	
		}
	}
}
