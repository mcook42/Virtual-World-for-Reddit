using System;

/// <summary>
/// Load overview menu on click.
/// </summary>
public class LoadOverviewMenuOnClick: LoadMenuOnClick
{
	public override void loadMenu ()
	{
		ThingListMenu setup = menuInstance.GetComponentInChildren<ThingListMenu> ();
		setup.init (GameInfo.instance.reddit.User.Overview.GetEnumerator());
	}
}


