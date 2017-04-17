using System;
using UnityEngine;


/// <summary>
/// Class all menu's inherit from. The Start and OnDestroy methods control the cursor, pause, and some key methods needed when opening/closing menus.
/// </summary>
public abstract class TempMenu:MonoBehaviour
{


	/// <summary>
	/// Pauses time, increments the number of menus in the menu controller, makes the cursor appear, and attaches the menu to the canvas.
	/// </summary>
	protected void Start()
	{

		if (GameInfo.instance.fatalError)
			return;

		GameInfo.instance.menuController.GetComponent<MenuController> ().addMenu ();
		transform.SetParent(GameInfo.instance.menuController.GetComponent<MenuController>().canvas.transform, false);
		transform.SetAsLastSibling(); //make it appear on top of everything else.

	}
		


	/// <summary>
	/// Detatches the menu from the parent. Decrements the number of menus loaded in the menuController, unpauses time, resets the cursor.
	/// </summary>
	protected void OnDestroy()
	{
		transform.SetParent(null);
		if(GameInfo.instance.menuController != null)
			GameInfo.instance.menuController.GetComponent<MenuController> ().removeMenu ();
	}


}


