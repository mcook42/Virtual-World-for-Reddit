using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Graph
{
	/// <summary>
	/// A simple node class designed to hold any arbitrary element.
	/// Code from https://msdn.microsoft.com/en-us/library/ms379574(v=vs.80).aspx.
	/// </summary>
	public class Node <T>{

		//Variables to used when drawing the graph.
		public Vector2 position { get; set; }
		public Vector2 velocity { get; set; }
		public bool inDome { get; set; }

		// Private member-variables
		private T data;
		private NodeList<T> toNeighbors;
		private NodeList<T> fromNeighbors;
		private List<int> costs;

		public Node() {}
		public Node(T data) : this(data, new NodeList<T>(),new NodeList<T>(),new List<int>()) {}

		/// <summary>
		/// Initializes a new instance of the <see cref="Graph.Node`1"/> class.
		/// </summary>
		/// <param name="data">Data.</param>
		/// <param name="neighbors">Neighbors.</param>
		public Node(T data, NodeList<T> toNeighbors, NodeList<T> fromNeighbors, List<int> costs)
		{
			this.data = data;
			this.toNeighbors = toNeighbors;
			this.costs = costs;
			this.fromNeighbors = fromNeighbors;
			this.position = new Vector2 (0, 0);
			this.velocity = new Vector2 (0, 0);
			inDome = false;
		}

		/// <summary>
		/// The value contained in the node.
		/// </summary>
		/// <value>The value.</value>
		public T Value
		{
			get
			{
				return data;
			}
			set
			{
				data = value;
			}
		}

		/// <summary>
		/// The Adjecent Nodes.
		/// </summary>
		/// <value>The neighbors.</value>
		public NodeList<T> ToNeighbors
		{
			get
			{
				return toNeighbors;
			}
			set
			{
				toNeighbors = value;
			}
		}

		public NodeList<T> FromNeighbors
		{
			get
			{
				return fromNeighbors;
			}
			set
			{
				fromNeighbors = value;
			}
		}

		/// <summary>
		/// The cost of going to the ith neighbor.
		/// </summary>
		/// <value>The costs.</value>
		public List<int> Costs
		{
			get
			{
				if (costs == null)
					costs = new List<int>();

				return costs;
			}
		}
	}

}



