using Microsoft.EntityFrameworkCore;
using Mango.Services.CouponAPI.Models;

namespace Mango.Services.CouponAPI.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<Coupon> Coupons { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Coupon>().HasData(new Coupon
			{
				CouponID = 1,
				CouponCode = "ABC123",
				DiscountMount = 10,
				MinAmount = 20,
			});

			modelBuilder.Entity<Coupon>().HasData(new Coupon
			{
				CouponID = 2,
				CouponCode = "BAC123",
				DiscountMount = 50,
				MinAmount = 60,
			});
		}
	}
	
}
