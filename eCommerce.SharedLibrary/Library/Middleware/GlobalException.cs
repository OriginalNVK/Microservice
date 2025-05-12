using Library.Logs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Library.Middleware
{
	public class GlobalException(RequestDelegate next)
	{
		public async Task InvokeAsync(HttpContext context)
		{
			// Declare default variables
			string message = "sorry, internal server error occured. Kindly try again";
			int statusCode  = (int)HttpStatusCode.InternalServerError;
			string title = "Error";

			try
			{
				await next(context);

				// Check if response is Too Many Request // 429 status code.
				if(context.Response.StatusCode == StatusCodes.Status429TooManyRequests)
				{
					title = "Warning";
					message = "Too Many Request";
					statusCode = StatusCodes.Status429TooManyRequests;
					await ModifyHeader(context, title, message, statusCode);
				}

				// Check if response is UnAuthorized // 401 status code
				if(context.Response.StatusCode == StatusCodes.Status401Unauthorized)
				{
					title = "Alert";
					message = "You are not authorized to access.";
					statusCode = StatusCodes.Status401Unauthorized;
					await ModifyHeader(context, title, message, statusCode);
				}

				// Check if response is Forbidden // 403 status code
				if(context.Response.StatusCode == StatusCodes.Status403Forbidden)
				{
					title = "Out of access";
					message = "You are not allowed to access";
					statusCode = StatusCodes.Status403Forbidden;
					await ModifyHeader(context, title, message, statusCode);
				}
			}catch (Exception ex)
			{
				// Log Original Exception / Console / Debug
				LogException.LogExceptions(ex);

				// check if exception is Timeout // 408 Request Time Out
				if(ex is TaskCanceledException || ex is TimeoutException) {
					title = "Out of time";
					message = "Request Timeout ... Try again";
					statusCode = StatusCodes.Status408RequestTimeout;
				}
				// If Exception is caught
				// If none of exception then do the default
				await ModifyHeader(context, title, message, statusCode);
			}
		}

		private static async Task ModifyHeader(HttpContext context, string title, string message, int statusCode)
		{
			context.Response.ContentType = "application/json";
			await context.Response.WriteAsync(JsonSerializer.Serialize(new ProblemDetails()
			{
				Detail = message,
				Status = statusCode,
				Title = title,
			}), CancellationToken.None);
			return;
		}
	}
}
