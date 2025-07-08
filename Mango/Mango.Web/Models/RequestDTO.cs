using Mango.Mango.web.Models;
using Microsoft.AspNetCore.Mvc;
using static Mango.Web.Utility.Utils;

namespace Mango.Web.Models
{
	public class RequestDTO
	{
		public ApiType Method { get; set; } = ApiType.GET;

		public string Url { get; set; }

		public CouponDTO Data { get; set; }

		public string AccessToken { get; set; }
	}
}
