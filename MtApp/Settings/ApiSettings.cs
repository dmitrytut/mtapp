using System;

namespace MtApp.Settings
{
	public class ApiSettings : IApiSettings
	{
		public string BaseUrl => "http://127.0.0.1:5000/api";
	}
}
