namespace Mango.Mango.web.Models
{
	public class CouponDTO
	{
		public int CouponId { get; set; }
		public string couponCode { get; set; }
		public double discountMount { get; set; }
		public int minAmount { get; set; }
	}
}
