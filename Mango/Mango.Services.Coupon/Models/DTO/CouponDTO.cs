﻿namespace Mango.Services.CouponAPI.Models.DTO
{
	public class CouponDTO
	{
        public int CouponID { get; set; }
        public string couponCode { get; set; }
		public double discountMount { get; set; }
		public int minAmount { get; set; }
	}
}
