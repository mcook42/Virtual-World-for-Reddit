using System;

/**Caleb Whitman
 * calebrwhitman@gmail.com
 * Spring 2017
 */ 

/// <summary>
/// Load new messages menu on click.
/// </summary>
public class LoadNewMessagesMenuOnClick : LoadMenuOnClick
{
	public override void loadMenu ()
	{
		ThingListMenu setup = menuInstance.GetComponentInChildren<ThingListMenu> ();
		setup.init (WorldState.instance.reddit.User.UnreadMessages.GetEnumerator());
	}
}


