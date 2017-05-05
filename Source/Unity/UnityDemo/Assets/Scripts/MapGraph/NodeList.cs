using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

/**Caleb Whitman
 * calebrwhitman@gmail.com
 * Spring 2017
 */ 

namespace Graph{
	/// <summary>
	/// A list of nodes.
	/// Code from https://msdn.microsoft.com/en-us/library/ms379574(v=vs.80).aspx.
	/// </summary>
public class NodeList<T> : Collection<Node<T>>
{
	public NodeList() : base() { }

	
	/// <summary>
	/// Finds the Node with  the value using the values equal method.
	/// </summary>
	/// <returns>The Node if found, null otherwise.</returns>
	/// <param name="value">Value to find.</param>
	public Node<T> FindByValue(T value)
	{
		// search the list for the value
		foreach (Node<T> node in Items)
			if (node.Value.Equals(value))
				return node;

		// if we reached here, we didn't find a matching node
		return null;
	}
}
}
