using System;
using UnityEngine;

/**Caleb Whitman
 * calebrwhitman@gmail.com
 * Spring 2017
 */ 

/// <summary>
/// An Observer interface that updates whenever a user logins or logouts.
/// </summary>
public interface LoginObserver
{
	/// <summary>
	/// Notify the specified login.
	/// </summary>
	/// <param name="login">Is set to <c>true</c> if the user logged in, <c>false</c> if the user is logged out.</param>
	void notify(bool login);

}


