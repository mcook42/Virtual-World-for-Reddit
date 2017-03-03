/**Menu.cs
* Caleb Whitman
* February 17, 2017
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// All panels and menus derive from this class. Contains methods and fields in order to make instantiating and destroying the menus easy.
/// </summary>
/// <typeparam name="T">The derived class.</typeparam>
public abstract class Menu<T>: MonoBehaviour
{
    public GameObject prefab;
    protected static GameObject instance;

    /// <summary>
    /// The basic menu load. Simply loads the menu and makes the cursor appear.
    /// </summary>
    /// <param name="pause">Pause the game when the menu appears.</param>
    public void loadMenu(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0;
        }

        loadPanel();
        instance.transform.SetAsLastSibling(); //make it appear on top of everything else.
        GameInfo.instance.setCursorLock(false);
    }

    /// <summary>
    /// Loads the menu without modifying anything else in the game state.
    /// </summary>
    public void loadPanel()
    {
        instance = Instantiate(prefab);
        instance.transform.SetParent(GameInfo.instance.menuController.GetComponent<MenuController>().canvas.transform, false);
    }


    /// <summary>
    /// Destroys the menu, makes the cursor disappear, and then unpauses the game.
    /// </summary>
    public void unLoadMenu()
    {
        if (instance != null)
        {
            instance.transform.SetParent(null);
            Destroy(instance);
            GameInfo.instance.setCursorLock(true);
        }
        if(Time.timeScale!=1)
            Time.timeScale = 1;
        
    }
}

