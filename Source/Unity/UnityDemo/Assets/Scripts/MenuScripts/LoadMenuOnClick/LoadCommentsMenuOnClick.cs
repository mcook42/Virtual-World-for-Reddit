using System;

/// <summary>
/// Load comments menu on click.
/// </summary>
public class LoadCommentsMenuOnClick : LoadMenuOnClick
{
	public override void loadMenu()
	{
		ThingListMenu setup = menuInstance.GetComponentInChildren<ThingListMenu> ();
		setup.init (GameInfo.instance.reddit.User.Comments.GetEnumerator());
	}
}


