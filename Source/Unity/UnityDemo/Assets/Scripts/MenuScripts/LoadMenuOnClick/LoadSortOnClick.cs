/**
* Author: Caleb Whitman
* Date: February 17, 2017
* Email: calebrwhitman@gmail.com
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/// <summary>
/// Controls the menu to sort the threads.
/// </summary>
public class LoadSortOnClick: LoadMenuOnClick {

	public override void loadMenu()
	{
		menuInstance.GetComponent<SortMenu>().init ();
	}




}
