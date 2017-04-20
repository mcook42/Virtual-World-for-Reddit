using System;

/// <summary>
/// Load new messages menu on click.
/// </summary>
public class LoadNewMessagesMenuOnClick : LoadMenuOnClick
{
	public override void loadMenu ()
	{
		ThingListMenu setup = menuInstance.GetComponentInChildren<ThingListMenu> ();
		setup.init (GameInfo.instance.reddit.User.UnreadMessages.GetEnumerator());
	}
}


