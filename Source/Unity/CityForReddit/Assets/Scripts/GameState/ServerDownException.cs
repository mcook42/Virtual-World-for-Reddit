using System;
/**Caleb Whitman
 * calebrwhitman@gmail.com
 * Spring 2017
 */ 
/// <summary>
/// An exception thrown when the server cannot be accessed.
/// </summary>
public class ServerDownException : Exception
{
	public ServerDownException ()
	{
	}

	public ServerDownException(string message)
		: base(message)
	{
	}

	public ServerDownException(string message, Exception inner)
		: base(message, inner)
	{
	}
}


