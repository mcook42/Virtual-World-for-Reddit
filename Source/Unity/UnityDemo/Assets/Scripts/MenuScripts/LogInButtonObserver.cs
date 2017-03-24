using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Changes the login button when the user logs in/out.
/// </summary>
public class LogInButtonObserver : MonoBehaviour,LoginObserver {

	public GameObject LoginButton;

	void Start()
	{
		GameInfo.instance.redditRetriever.register (this);

		if (GameInfo.instance.reddit.User != null) {
			notify (true);
		} else {
			notify (false);
		}
	}

	public void notify(bool login)
	{

		if (login) {
			LoginButton.GetComponent<Button> ().onClick.RemoveAllListeners ();
			LoginButton.GetComponent<Button> ().onClick.AddListener (() => GameInfo.instance.menuController.GetComponent<MainMenu> ().logout ());
			LoginButton.GetComponentInChildren<Text> ().text = "Logout";
		} else {
			LoginButton.GetComponent<Button> ().onClick.RemoveAllListeners ();
			LoginButton.GetComponent<Button> ().onClick.AddListener (() => GameInfo.instance.menuController.GetComponent<MainMenu> ().login());
			LoginButton.GetComponentInChildren<Text> ().text = "Login";
		}


	}

	void OnDestroy()
	{
		GameInfo.instance.redditRetriever.unRegister (this);
	}

}
