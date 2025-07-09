using Mango.Mango.web.Models;
using Mango.Mango.Web.Models;
using Mango.Web.Models;
using Mango.Web.Services.IService;
using Mango.Web.Utility;
using static Mango.Web.Utility.Utils;

namespace Mango.Web.Services
{
	public class CouponService : ICouponService
	{
		private readonly IBaseService _baseService;

		public CouponService(IBaseService baseService)
		{
			_baseService = baseService;
		}
		public async Task<ResponseDTO?> CreateCouponAsync(CouponDTO coupon)
		{
			return await _baseService.SendAsync(new RequestDTO()
			{
				Method = ApiType.POST,
				Url = Utils.CouponAPIBase + "/api/coupon",
				Data = coupon
			});
		}

		public async Task<ResponseDTO?> DeleteCouponAsync(int id)
		{
			return await _baseService.SendAsync(new RequestDTO() 
			{ Method = ApiType.DELETE, Url = Utils.CouponAPIBase + "/api/coupon/" + id });
		}

		public async Task<ResponseDTO?> GetAllCouponsAsync()
		{
			return await _baseService.SendAsync(new RequestDTO()
			{
				Method = ApiType.GET,
				Url = Utils.CouponAPIBase + "/api/coupon",
			});
		}

		public async Task<ResponseDTO?> GetCouponAsync(string couponCode)
		{
			return await _baseService.SendAsync(new RequestDTO()
			{
				Method = ApiType.GET,
				Url = Utils.CouponAPIBase + "/api/coupon/getbycode/" + couponCode,
			});
		}

		public async Task<ResponseDTO?> GetCouponByIdAsync(int id)
		{
			return await _baseService.SendAsync(new RequestDTO() 
			{ Method = ApiType.GET, Url = Utils.CouponAPIBase + "/api/coupon/" + id });
		}

		public async Task<ResponseDTO?> UpdateCouponAsync(CouponDTO coupon)
		{
			return await _baseService.SendAsync(new RequestDTO()
			{
				Method = ApiType.PUT,
				Url = Utils.CouponAPIBase + "/api/coupon",
				Data = coupon
			});
		}
	}
}
