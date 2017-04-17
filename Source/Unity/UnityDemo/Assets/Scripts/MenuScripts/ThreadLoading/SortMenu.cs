using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class SortMenu : TempMenu{

	public void init()
	{

		var sortMethods = Enum.GetValues(typeof(RedditSharp.Things.Sort));
		var buttons = GetComponentsInChildren<Button>();


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
		GameInfo.instance.menuController.GetComponent<MenuController>().loadLoadingMenu();
		SubredditSceneState.instance.subbredditSetup.GetComponent<SubredditSceneSetup>().loadThreads(sortingMethod);
		Destroy (gameObject);
		Debug.Log("Sorting!");
		GameInfo.instance.setCursorLock (false);
		GameInfo.instance.menuController.GetComponent<MenuController>().unLoadLoadingMenu();


	}
}
