/**Caleb Whitman
 * calebrwhitman@gmail.com
 * Spring 2017
 */ 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// If used properly, all deriving classes should be Singletons. 
/// </summary>
public abstract class SceneStateSingleton<T>:SceneState where T: SceneStateSingleton<T>, new()
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
		
}

