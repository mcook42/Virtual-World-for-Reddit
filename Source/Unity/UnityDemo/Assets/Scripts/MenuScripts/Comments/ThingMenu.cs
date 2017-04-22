using System;
using UnityEngine;
using RedditSharp.Things;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// An Abstract class with methods for organizing the comments.
/// </summary>
public abstract class ThingMenu : Menu
{
	public GameObject commentPrefab;
	public GameObject postPrefab;
	public GameObject title;
	public GameObject loadMoreButton;
	public GameObject loadMorePanel;

	public GameObject content;

	protected readonly int topLevelThingssLoaded = 5;
	protected 	IEnumerator thingEnumerator;


	/// <summary>
	/// Initializes the things.
	/// </summary>
	/// <returns>The number of things created.</returns>
	/// <param name="parent">Parent.</param>
	/// <param name="things">Things to be intialized.</param>
	/// <param name="startDepth">Used to determine the indent.</param>
	public abstract int initializeThings (GameObject parent, Thing[] things, int startDepth);


	/// <summary>
	/// Initializes the thing.
	/// </summary>
	/// <param name="thing">The thing to initialize.</param>
	/// <param name="depth">Depth in comment tree.</param>
	/// <returns> The initialized thing.</returns>
	public abstract GameObject initializeThing(GameObject parent,Thing thing, int depth);

	/// <summary>
	/// Loads more things
	/// </summary>
	public void loadMore(){
		bool thingsLeft = true;
		List<Thing> thing = new List<Thing> ();
		for (int i = 0; i < topLevelThingssLoaded; i++) {
			if (thingEnumerator.MoveNext ())
				thing.Add ((Thing)thingEnumerator.Current);
			else {
				thingsLeft = false;
				break;
			}


		}
		//no more comments to load.
		if (!thingsLeft)
			loadMorePanel.SetActive (false);

		initializeThings (content,thing.ToArray(),0);


	}

	/// <summary>
	/// Destroys this menu.
	/// </summary>
	public void close()
	{
		Destroy (gameObject);
	}
}


