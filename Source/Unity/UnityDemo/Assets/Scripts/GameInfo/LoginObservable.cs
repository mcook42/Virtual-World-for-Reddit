using System;
using System.Collections.Generic;

/**Caleb Whitman
 * calebrwhitman@gmail.com
 * Spring 2017
 */ 

/// <summary>
/// An observable interface for when a user logs in/out.
/// </summary>
public interface LoginObservable
{
	/// <summary>
	/// Register the specified observer.
	/// </summary>
	/// <param name="anObserver">An observer.</param>
	void register (LoginObserver anObserver);
	/// <summary>
	/// Unregister an observer.
	/// </summary>
	/// <param name="anObserver">An observer.</param>
	void unRegister (LoginObserver anObserver);
}


