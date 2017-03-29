using System;
using System.Net;

namespace MtApp.Exceptions
{
	public class NetworkException : Exception
	{
		public HttpStatusCode StatusCode { get; private set; }

		public NetworkException(string message) : base(message) { }

		public NetworkException(string message, HttpStatusCode code) 
			: base(message) 
		{
			StatusCode = code;
		}
	}
}
