using System;
using System.Collections.Generic;

/// <summary>
/// An observable interface for when a user logs in/out.
/// </summary>
public interface LoginObservable
{

	void register (LoginObserver anObserver);
	void unRegister (LoginObserver anObserver);
}


