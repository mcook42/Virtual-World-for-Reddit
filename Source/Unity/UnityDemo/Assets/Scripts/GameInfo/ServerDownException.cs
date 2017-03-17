using System;

namespace AssemblyCSharp
{
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
}

