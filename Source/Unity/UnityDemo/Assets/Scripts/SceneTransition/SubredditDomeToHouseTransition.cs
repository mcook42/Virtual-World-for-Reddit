using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**Caleb Whitman
 * calebrwhitman@gmail.com
 * Spring 2017
 */ 

/// <summary>
/// Loads the House from the SubredditDome.
/// </summary>
public class SubredditDomeToHouseTransition : SceneTransition {



	protected override void transferInfo()
	{
		clearCurrentState ();
		SceneManager.LoadScene ("House");
	}
}
