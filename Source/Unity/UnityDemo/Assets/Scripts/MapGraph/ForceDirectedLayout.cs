using System;
using Graph;
using UnityEngine;


/// <summary>
/// Draws the graph using a force directed layout algorithm. 
/// </summary>
public class ForceDirectedLayout
{
	private readonly float attractiveForce=0.01f;
	private readonly float repulsiveForce=10000;

	public readonly static int  innerBuildingNum=12;
	public readonly static int outerBuildingNum =13; 
	public enum StopOption{Time, Iteratation, NoOverlap, Threshold};

	public StopOption stopOption { get; set;}
	public float nodeSize { get; set;}


	public int threshold { get; set;}

	private float domeRadius=0;
	public int maxIterations=100;
	/// <summary>
	/// Initializes a new instance of the <see cref="ForceDirectedLayout"/> class.
	/// With StopOption.Time, threshold is the number of maximum milliseconds to run.
	/// With StopOption.Iteratation, threshold is the number of maximum iterations to run. 
	/// With StopOption.NoOverlap, the algorithm will stop when no nodes are overlapping. Max number of iterations set at 1000.
	/// With StopOption.Threshold, the algorithm will stop when the overall netforce goes below the threshold. Max number of iterations set at 1000.
	/// </summary>
	/// <param name="stopOption">Stop option.</param>
	/// <param name="maxIterations">Max iterations.</param>
	public ForceDirectedLayout(StopOption stopOption, int threshold, float nodeSize)
	{
		this.stopOption = stopOption;
		this.threshold = threshold;
		this.nodeSize = nodeSize;
	}

	/// <summary>
	/// Run the algorithm on specified graph and nodeSize.
	/// </summary>
	/// <param name="graph">Graph.</param>
	/// <param name="nodeSize">Node size.</param>
	/// <param name="centerNode"> The subreddit in the center of the graph. </param> 
	/// <typeparam name="T">The item the graph holds.</typeparam>
	public Graph<T> run<T>(Graph<T> graph, Node<T> centerNode)
	{
		graph = initializePositions (graph, centerNode);
		if (stopOption == StopOption.Threshold) {
			return runThreshold (graph,centerNode);
		}

		return graph;
	}


	/// <summary>
	/// Initialzes the nodes in a sort of circle around the centerNode. 
	/// </summary>
	/// <returns>The initialize.</returns>
	/// <param name="graph">Graph.</param>
	/// <param name="centerNode">Center node.</param>
	/// <param name="nodeSize">Node size.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	private Graph<T> initializePositions<T>(Graph<T> graph, Node<T> centerNode)
	{
		resetInDome (graph);
		domeRadius = setInDome (centerNode);

		System.Random random = new System.Random ();

		foreach (Node<T> node in graph) {
			int minValue = - Mathf.CeilToInt(nodeSize) * graph.Count * 2;
			int maxValue = Mathf.CeilToInt(nodeSize) * graph.Count * 2;

			int randomX = random.Next (minValue, maxValue);
			int randomY = random.Next (minValue, maxValue);

			if(node.inDome == false)
				node.position = new Vector2 (randomX, randomY);
		}
		return graph;
	}

	/// <summary>
	/// Sets all inDome values to false.
	/// </summary>
	/// <param name="graph">Graph.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public void resetInDome<T>(Graph<T> graph)
	{
		foreach (Node<T> node in graph) {
			node.inDome = false;
		}

	}

	/// <summary>
	/// Sets the positions of the centerNode and its neighbors so that they are in the dome structure.
	/// Also sets inDome in each Node to true.
	/// </summary>
	/// <param name="centerNode">Center node.</param>
	/// <returns>Radius of the Dome </returns>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	private float setInDome<T>(Node<T> centerNode)
	{
		

		float innerAngle = 2 * Mathf.PI / innerBuildingNum;
		float outerAngle = 2 * Mathf.PI / outerBuildingNum;

		float innerRadius = nodeSize * innerBuildingNum * 3;
		float outerRadius = nodeSize * outerBuildingNum * 5;

		float domeRadius = innerRadius+outerRadius+nodeSize/2;

		centerNode.inDome = true;
		centerNode.position = new Vector2 (0, 0);

		NodeList<T> nodes = centerNode.ToNeighbors;
		for(int i =0; i < nodes.Count && i < innerBuildingNum+outerBuildingNum;i++) {
			nodes [i].inDome = true;

			float x;
			float y;
			if (i < innerBuildingNum) {
				x = innerRadius * Mathf.Cos (innerAngle * i);
				y = innerRadius * Mathf.Sin (innerAngle * i);
			} else {
				x = outerRadius * Mathf.Cos (outerAngle * (i-innerBuildingNum));
				y = outerRadius * Mathf.Sin (outerAngle * (i-innerBuildingNum));
			}

			nodes [i].position = new Vector2 (x, y);

		}

		return domeRadius;


	}


	/// <summary>
	/// Centers the nodes so that the average position is centered.
	/// </summary>
	/// <returns>The nodes.</returns>
	/// <param name="graph">Graph.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public Graph<T> centerNodes<T>(Graph<T> graph)
	{
		Vector2 average = new Vector2 (0, 0);

		foreach (Node<T> node in graph) {
			average.x += node.position.x;
			average.y += node.position.y;
		}

		average.x = average.x / graph.Nodes.Count;
		average.y = average.y / graph.Nodes.Count;

		foreach (Node<T> node in graph) {
			node.position = new Vector2 (node.position.x + average.x, node.position.y + average.y);
		}

		return graph;

	}


	public Graph<T> runThreshold<T>(Graph<T> graph, Node<T> centerNode)
	{


		float totalDisplacement=threshold+1;
		int iterations = 0;
		while( (totalDisplacement > threshold ) && ( iterations< maxIterations )){
			totalDisplacement = 0;
			foreach (Node<T> node in graph) {

				node.velocity = new Vector2 (0, 0);
			}

			for (int i = 0; i < graph.Nodes.Count; i++) {

				Node<T> n1 = graph.Nodes [i];
				for (int j = i + 1; j < graph.Nodes.Count; j++) {
					Node<T> n2 = graph.Nodes [j];

					Vector2 repulsion = calcRepulsionForce (n1, n2);
					Vector2 attraction = calcAttractionForce (n1, n2);
					if(!n1.inDome)
						n1.velocity += repulsion + attraction;
					if(!n2.inDome)
						n2.velocity += repulsion + attraction;

				}

			}

			foreach (Node<T> node in graph) {
				if(!node.inDome)
					totalDisplacement += Mathf.Sqrt (node.velocity.x * node.velocity.x + node.velocity.y * node.velocity.y);
				node.position = node.position + node.velocity;
			}

			iterations++;
	}

		return graph;
	}

	private Vector2 calcRepulsionForce<T>(Node<T> n1, Node<T> n2)
	{
		float distanceBetween = Mathf.Sqrt (Mathf.Pow((n1.position.x - n2.position.x),2) + Mathf.Pow((n1.position.y - n2.position.y),2));
		float force = (distanceBetween==0) ? 10*repulsiveForce : -(repulsiveForce / (Mathf.Pow(distanceBetween,2)));

		Vector2 angle = new Vector2 (n1.position.x - n2.position.x, n1.position.y - n2.position.y);
		angle.Normalize ();

		return force*angle;
	}

	/// <summary>
	/// Calculates the attraction force. If the nodes are on top of each other, then the attractive force is zero.
	/// </summary>
	/// <returns>The attraction force.</returns>
	/// <param name="n1">N1.</param>
	/// <param name="n2">N2.</param>
	/// <param name="nodeSize">Node size.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	private Vector2 calcAttractionForce<T>(Node<T> n1, Node<T> n2)
	{
		float springLength;
		if (n1.inDome) {
			springLength = (domeRadius - n1.position.magnitude) + nodeSize;//Node will not be pulled into the dome.
		} else if (n2.inDome) {
			springLength = (domeRadius - n2.position.magnitude) + nodeSize;//Node will not be pulled into the dome.

		}else{
			springLength = 2 * nodeSize;

		}


		if (n1.ToNeighbors.Contains (n2) || n2.ToNeighbors.Contains (n1)) {
			float distanceBetween = Mathf.Sqrt (Mathf.Pow ((n1.position.x - n2.position.x), 2) + Mathf.Pow ((n1.position.y - n2.position.y), 2));
			float force = attractiveForce * Mathf.Max (distanceBetween - ( springLength ), 0.0f);

			Vector2 angle = new Vector2 (n1.position.x - n2.position.x, n1.position.y - n2.position.y);
			angle.Normalize ();

			return force*angle;
		}
		else
		{
			return new Vector2 (0, 0);
		}

	}
}

