﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
//using Microsoft.IdentityModel.Tokens;

namespace Library.Dependency_Injection
{
	public static class JWTAuthenticationScheme
	{
		public static IServiceCollection AddJWTAuthenticationScheme(this IServiceCollection services, IConfiguration config)
		{
			// add JWT service
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer("Bearer", options =>
				{
					var key = Encoding.UTF8.GetBytes(config.GetSection("Authentication:Key").Value!);
					string issuer = config.GetSection("Authentication:Issuer").Value!;
					string audience = config.GetSection("Authentication:Audience").Value!;
					options.RequireHttpsMetadata = false;
					options.SaveToken = false;
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = issuer,
						ValidAudience = audience,
						IssuerSigningKey = new SymmetricSecurityKey(key)
					};
				});
			return services;
		}
	}
}
