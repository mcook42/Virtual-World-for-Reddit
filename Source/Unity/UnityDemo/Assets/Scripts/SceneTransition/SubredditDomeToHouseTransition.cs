using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubredditDomeToHouseTransition : SceneTransition {



	protected override void transferInfo()
	{
		clearCurrentState ();
		SceneManager.LoadScene ("House");
	}
}
