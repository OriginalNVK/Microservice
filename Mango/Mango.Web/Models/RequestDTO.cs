using Microsoft.AspNetCore.Mvc;
using static Mango.Web.Utility.Utils;

namespace Mango.Web.Models
{
	public class RequestDto
	{
		public ApiType Method { get; set; } = ApiType.GET;

		public string Url { get; set; }

		public string Data { get; set; }

		public string AccessToken { get; set; }
	}
}
