using System;

/**Caleb Whitman
 * calebrwhitman@gmail.com
 * Spring 2017
 */ 

/// <summary>
/// Loads submitted menu on click.
/// </summary>
public class LoadSubmittedMenuOnClick : LoadMenuOnClick
{
	/// <summary>
	/// Loads the submitted menu.
	/// </summary>
	public override void loadMenu()
	{
		ThingListMenu setup = menuInstance.GetComponentInChildren<ThingListMenu> ();
		setup.init (WorldState.instance.reddit.User.Posts.GetEnumerator());
	}
}


