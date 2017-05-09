/**Caleb Whitman
 * calebrwhitman@gmail.com
 * Spring 2017
 */ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using System.Linq;

/// <summary>
///  A class that holds the canvas. In addition, this class contains functions and key controllers for menus not associated with any 
/// single object such as the pause and error menus.
/// </summary>
public class MenuController : MonoBehaviour {

    public GameObject canvas;
	public GameObject errorMenuPrefab;
	public GameObject fatalErrorMenuPrefab;
	public GameObject pauseMenuPrefab;
	public GameObject mapMenuPrefab;
	public GameObject loadingMenuPrefab;
	private GameObject loadingMenu;

	//number of menus currently loaded. This is used to tell if the player is still in the menu screen.
	public int menusLoaded = 0;
	//Lock to keep multiple menus from accessing menusLoaded at once.
	private readonly object menusLoadedLock = new object();

	/// <summary>
	/// Records that a menu has been added.
	/// The number of menus added affects the key bindings.
	/// Also pauses time and makes the cursor appear.
	/// </summary>
	public void addMenu()
	{
		lock (menusLoadedLock) {
			menusLoaded++;
			WorldState.instance.setCursorLock (false); 
			Time.timeScale = 0;
		}

	}

	/// <summary>
	/// Records that a menu has been removed.
	/// The number of menus affects the key bindings.
	/// Also retarts time and makes the cursor disappear if all menus are gone.
	/// </summary>
	public void removeMenu()
	{
		lock (menusLoadedLock) {
			if(menusLoaded>0)
				menusLoaded--;
			if (WorldState.instance.menuController.GetComponent<MenuController> ().menusLoaded <= 0) {
				WorldState.instance.setCursorLock (true);
				Time.timeScale = 1;
			}
		}
	}
	void Update () {

		if (menusLoaded == 0) {
			if (Input.GetKeyDown ("escape")) {
				Instantiate (pauseMenuPrefab);
			}
				

			if (Input.GetKeyDown ("m")) {
				loadMap ();
			}
				
		}
	}

	/// <summary>
	/// Loads the error menu.
	/// </summary>
	public void loadErrorMenu(string error)
	{
		var errorMenu = Instantiate (errorMenuPrefab);
		errorMenu.GetComponent<ErrorMenu> ().init (error);

	}

	/// <summary>
	/// Loads the fatal error menu.
	/// </summary>
	/// <param name="error">Error.</param>
	public void loadFatalErrorMenu(string error)
	{
		var fatalErrorMenu = Instantiate (fatalErrorMenuPrefab);
		fatalErrorMenu.GetComponent<FatalErrorMenu> ().init (error);
	}

	/// <summary>
	/// Loads the map.
	/// </summary>
	public void loadMap()
	{
		Instantiate (mapMenuPrefab);
	}

	/// <summary>
	/// Loads the loading menu.
	/// </summary>
	public void loadLoadingMenu()
	{
		loadingMenu = Instantiate (loadingMenuPrefab);
	}

	/// <summary>
	/// Unloads loading menu.
	/// </summary>
	public void unLoadLoadingMenu()
	{
		Destroy (loadingMenu);
	}

	/// <summary>
	/// Destroys all children of  the canvas.
	/// </summary>
	public void clearMenus()
	{

		menusLoaded = 0;
		foreach (Transform child in canvas.transform) {
			Destroy (child.gameObject);
		}
	}
    /// <summary>
    /// Exists the application. This will not work within the editor.
    /// </summary>
    public void quit(){
		
		Application.Quit ();

	}


}
