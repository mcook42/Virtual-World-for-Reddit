using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace GenericGraph
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
			from.Neighbors.Add(to);
			from.Costs.Add(cost);
		}

		/// <summary>
		/// Adds the undirected edge.
		/// Constant time operation.
		/// </summary>
		/// <param name="from">Beginning node.</param>
		/// <param name="to">Ending node.</param>
		/// <param name="cost">Cost.</param>
		public void AddUndirectedEdge(Node<T> from, Node<T> to, int cost)
		{
			from.Neighbors.Add(to);
			from.Costs.Add(cost);

			to.Neighbors.Add(from);
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
		/// <returns> True if found. False otherwise.
		public bool Remove(T value)
		{
			// first remove the node from the nodeset
			Node<T> nodeToRemove = (Node<T>) nodeSet.FindByValue(value);
			if (nodeToRemove == null)
				// node wasn't found
				return false;

			// otherwise, the node was found
			nodeSet.Remove(nodeToRemove);


			// enumerate through each node in the nodeSet, removing edges to this node
			foreach (Node<T> gnode in nodeSet)
			{
				int index = gnode.Neighbors.IndexOf(nodeToRemove);
				if (index != -1)
				{
					// remove the reference to the node and associated cost
					gnode.Neighbors.RemoveAt(index);
					gnode.Costs.RemoveAt(index);
				}
			}

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
				gnode.Neighbors.Clear ();
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



		IEnumerator IEnumerable.GetEnumerator()
		{
			// call the generic version of the method
			return this.GetEnumerator();
		}

		public IEnumerator<Node<T>> GetEnumerator()
		{
			for (int i = 0; i < Count; i++)
				yield return nodeSet[i];
	
		}
	}
}
