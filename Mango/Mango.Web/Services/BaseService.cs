using Mango.Mango.Web.Models;
using Mango.Web.Models;
using Mango.Web.Services.IService;
using System.Text.Json.Serialization;
using static Mango.Web.Utility.Utils;
using Newtonsoft.Json;
using System.Text;
using System.Net;

namespace Mango.Web.Services
{
	public class BaseService : IBaseService
	{
		private readonly IHttpClientFactory _iHttpClientFactory;

		public BaseService(IHttpClientFactory iHttpClientFactory) { 
			_iHttpClientFactory = iHttpClientFactory;
		}

		public async Task<ResponseDTO?> SendAsync(RequestDTO requestDto)
		{
			try
			{
				HttpClient client = _iHttpClientFactory.CreateClient();
				HttpRequestMessage message = new HttpRequestMessage();

				switch (requestDto.Method)
				{
					case ApiType.GET:
						message.Method = HttpMethod.Get;
						break;
					case ApiType.POST:
						message.Method = HttpMethod.Post;
						break;
					case ApiType.PUT:
						message.Method = HttpMethod.Put;
						break;
					default:
						message.Method = HttpMethod.Delete;
						break;
				}

				message.Headers.Add("Accept", "application/json");
				// Token

				message.RequestUri = new Uri(requestDto.Url);
				if (requestDto.Data != null)
				{
					message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
				}

				HttpResponseMessage response = await client.SendAsync(message);
				switch (response.StatusCode)
				{
					case HttpStatusCode.NotFound:
						return new() { isSuccess = false, Message = "Not Found" };
					case HttpStatusCode.Unauthorized:
						return new() { isSuccess = false, Message = "Unauthorized" };
					case HttpStatusCode.Forbidden:
						return new() { isSuccess = false, Message = "Forbidden" };
					case HttpStatusCode.InternalServerError:
						return new() { isSuccess = false, Message = "Server Error" };
					default:
						var responseContent = await response.Content.ReadAsStringAsync();
						var responseDTO = JsonConvert.DeserializeObject<ResponseDTO>(responseContent);
						return responseDTO;
				}
			}catch(Exception ex)
			{
				var dto = new ResponseDTO
				{
					Message = ex.Message.ToString(),
					isSuccess = false,
				};
				return dto;
			}
		}
	}
}
