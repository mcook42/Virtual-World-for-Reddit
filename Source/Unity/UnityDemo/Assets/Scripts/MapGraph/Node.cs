using System.Collections;
using System.Collections.Generic;

namespace GenericGraph
{
	/// <summary>
	/// A simple node class designed to hold any arbitrary element.
	/// Code from https://msdn.microsoft.com/en-us/library/ms379574(v=vs.80).aspx.
	/// </summary>
	public class Node <T>{
		// Private member-variables
		private T data;
		private NodeList<T> neighbors;
		private List<int> costs;

		public Node() {}
		public Node(T data) : this(data, new NodeList<T>()) {}

		/// <summary>
		/// Initializes a new instance of the <see cref="Graph.Node`1"/> class.
		/// </summary>
		/// <param name="data">Data.</param>
		/// <param name="neighbors">Neighbors.</param>
		public Node(T data, NodeList<T> neighbors)
		{
			this.data = data;
			this.neighbors = neighbors;
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
		public NodeList<T> Neighbors
		{
			get
			{
				return neighbors;
			}
			set
			{
				neighbors = value;
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



