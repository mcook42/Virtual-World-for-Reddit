using System;

/// <summary>
/// Load messages menu on click.
/// </summary>
public class LoadInboxMenuOnClick:LoadMenuOnClick
{
	public override void loadMenu ()
	{
		ThingListMenu setup = menuInstance.GetComponentInChildren<ThingListMenu> ();
		setup.init (GameInfo.instance.reddit.User.);
	}
}


