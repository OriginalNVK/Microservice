using AutoMapper;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.DTO;

namespace Mango.Services.CouponAPI
{
	public class MapperConfig
	{
		public static MapperConfiguration RegisterMaps()
		{
			var config = new MapperConfigurationExpression();
			config.CreateMap<CouponDTO, Coupon>();
			config.CreateMap<Coupon, CouponDTO>();
			ILoggerFactory loggerFactory = new LoggerFactory();

			var mappingConfig = new MapperConfiguration(config, loggerFactory);

			return mappingConfig;
		}
	}
}
