/**SceneInfo.cs
 * Caleb Whitman
 * January 28, 2017
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// SceneState are Singleton classes responsible for managing the state of a scene.
/// If used properly, all deriving classes should be Singletons. 
/// </summary>
public abstract class SceneState<T> where T: SceneState<T>, new()
{

   private static T _instance = new T();

    
    //The access to the single, static class.
    public static T instance
    {
        get
        {
            return _instance;
        }
    }
		

    /// <summary>
    /// Sets any stored objects that take up a lot of memory to null.
    /// Called when this scene transitions to another scene.
    /// </summary>
    public abstract void clear();

    /// <summary>
    /// Completely destroys/resets every stored value.
    /// Usually only called when exiting to the menu screen.
    /// </summary>
    public abstract void reset();
}

