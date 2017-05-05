using System;

/**Caleb Whitman
 * calebrwhitman@gmail.com
 * Spring 2017
 */ 

/// <summary>
/// /// SceneStates are classes responsible for managing the state of a scene.
/// </summary>
public abstract class SceneState
{

	/// <summary>
	/// Completely destroys/resets every stored value.
	/// Usually only called when exiting to the menu screen.
	/// </summary>
	public abstract void reset();
}


