using System;

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
		setup.init (GameInfo.instance.reddit.User.Posts.GetEnumerator());
	}
}


