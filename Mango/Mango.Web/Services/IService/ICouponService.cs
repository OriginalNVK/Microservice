using Mango.Mango.web.Models;
using Mango.Mango.Web.Models;

namespace Mango.Web.Services.IService
{
	public interface ICouponService
	{
		Task<ResponseDTO?> GetAllCouponsAsync();
		Task<ResponseDTO?> GetCouponAsync(string couponCode);
		Task<ResponseDTO?> GetCouponByIdAsync(int id);
		Task<ResponseDTO?> CreateCouponAsync(CouponDTO coupon);
		Task<ResponseDTO?> UpdateCouponAsync(CouponDTO coupon);
		Task<ResponseDTO?> DeleteCouponAsync(int id);

	}
}
