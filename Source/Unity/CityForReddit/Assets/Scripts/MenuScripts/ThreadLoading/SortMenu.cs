using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

/**Caleb Whitman
 * calebrwhitman@gmail.com
 * Spring 2017
 */ 

/// <summary>
/// Sorts the Subreddits.
/// </summary>
public class SortMenu : Menu{

	private SubredditSceneSetup subredditSetup;

	/// <summary>
	/// Intializes the Sort Meu with the SubredditSceneSetup object.
	/// </summary>
	/// <param name="subredditSetup">Subreddit setup.</param>
	public void init(SubredditSceneSetup subredditSetup)
	{

		var sortMethods = Enum.GetValues(typeof(RedditSharp.Things.Sort));
		var buttons = GetComponentsInChildren<Button>();
		this.subredditSetup = subredditSetup;


		for (int i = 0; i < 4; i++)
		{
			RedditSharp.Things.Sort sort = (RedditSharp.Things.Sort)sortMethods.GetValue(i);
			buttons[i].onClick.AddListener(() => onClick(sort));
		}


	}

	/// <summary>
	/// Sorts and then unactivates the panel. Used for the buttons.
	/// </summary>
	/// <param name="sortingMethod"></param>
	void onClick(RedditSharp.Things.Sort sortingMethod)
	{
		WorldState.instance.menuController.GetComponent<MenuController>().loadLoadingMenu();
		subredditSetup.loadThreads(sortingMethod);
		Destroy (gameObject);
		WorldState.instance.setCursorLock (false);
		WorldState.instance.menuController.GetComponent<MenuController>().unLoadLoadingMenu();


	}
}
