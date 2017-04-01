using System;

/// <summary>
/// /// SceneStates are classes responsible for managing the state of a scene.
/// </summary>
public abstract class SceneState
{
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


