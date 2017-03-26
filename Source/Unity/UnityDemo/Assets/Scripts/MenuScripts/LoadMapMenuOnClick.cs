using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LoadMapMenuOnClick : MonoBehaviour {

	void OnMouseDown()
	{
		if (!EventSystem.current.IsPointerOverGameObject())
			GameInfo.instance.menuController.GetComponent<MapMenu> ().loadMenu (true);
	}
}
