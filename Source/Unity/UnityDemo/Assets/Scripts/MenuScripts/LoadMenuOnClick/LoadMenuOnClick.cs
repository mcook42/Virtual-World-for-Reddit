using System;
using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// Instantiates and loads a menu when the user clicks on the object this script is attatched too.
/// Does not apply to menu buttons. Menu button clicks are handled in the corresponding menu.
/// </summary>
public abstract class LoadMenuOnClick :MonoBehaviour
{
	public GameObject menuPrefab;
	protected GameObject menuInstance;

	/// <summary>
	/// Loads the Menu based on the current thread.
	/// </summary>
	public void OnMouseDown()
	{
		if (EventSystem.current.IsPointerOverGameObject())
			return;
		menuInstance= Instantiate (menuPrefab);
		loadMenu ();


	}

	/// <summary>
	/// Loads the menu and instantiates any variables that need to be instantiated.
	/// </summary>
	public abstract void loadMenu ();

}


