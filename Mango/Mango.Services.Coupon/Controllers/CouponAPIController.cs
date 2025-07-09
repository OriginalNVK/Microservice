using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponAPI.Controllers
{
	[Route("api/coupon")]
	[ApiController]
	public class CouponAPIController : ControllerBase
	{
		private readonly AppDbContext _db;
		private readonly IMapper _mapper;
		private ResponseDTO _response;

		public CouponAPIController(AppDbContext db, IMapper mapper)
		{
			_db = db;
			_mapper = mapper;
			_response = new ResponseDTO();
		}

		[HttpGet]
		public async Task<IActionResult> Gets()
		{
			try
			{
				List<Coupon> listCoupon = await _db.Coupons.ToListAsync();
				_response.Result = _mapper.Map<List<CouponDTO>>(listCoupon);
				_response.isSuccess = true;
				_response.Message = "Get all coupon successfully";
				return Ok(_response);
			}
			catch (Exception ex) {

			}
			return BadRequest();
		}

		[HttpGet]
		[Route("{id:int}")]
		public async Task<IActionResult> GetByID(int id)
		{
			try
			{
				object? coupon = await _db.Coupons.FirstOrDefaultAsync(x => x.CouponID == id);

				if(coupon is null)
				{
					_response = new ResponseDTO(){
						Result = null,
						isSuccess = false,
						Message = "Coupon not found" }
						;
					return BadRequest(_response);
				}
				_response = new ResponseDTO()
				{
					Result = _mapper.Map<CouponDTO>(coupon),
					isSuccess = true,
					Message = "Get coupon successfully"
				};
				return Ok(_response);
			}
			catch(Exception ex)
			{

			}
			return BadRequest();
		}

		[HttpGet]
		[Route("GetByCode/{code}")]
		public async Task<IActionResult> GetByCode(string code)
		{
			try
			{
				object? coupon = await _db.Coupons.FirstOrDefaultAsync(x => x.CouponCode == code);

				if (coupon is null)
				{
					_response = new ResponseDTO()
					{
						Result = null,
						isSuccess = false,
						Message = "Coupon not found"
					}
						;
					return BadRequest(_response);
				}
				_response = new ResponseDTO()
				{
					Result = _mapper.Map<CouponDTO>(coupon),
					isSuccess = true,
					Message = "Get coupon successfully"
				};
				return Ok(_response);
			}
			catch (Exception ex)
			{

			}
			return BadRequest();
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CouponDTO coupon)
		{
			try
			{
				Coupon couponAdded = _mapper.Map<Coupon>(coupon);
				await _db.Coupons.AddAsync(couponAdded);
				await _db.SaveChangesAsync();

				_response.Result = _mapper.Map<CouponDTO>(couponAdded);
				return Ok(_response);
			}
			catch (Exception ex)
			{

			}
			return BadRequest();
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] CouponDTO coupon)
		{
			try
			{
				Coupon couponAdded = _mapper.Map<Coupon>(coupon);
				_db.Coupons.Update(couponAdded);
				await _db.SaveChangesAsync();

				_response.Result = _mapper.Map<CouponDTO>(couponAdded);
				return Ok(_response);
			}
			catch (Exception ex)
			{

			}
			return BadRequest();
		}

		[HttpDelete]
		[Route("{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				Coupon? couponRemove = await _db.Coupons.FirstOrDefaultAsync(x => x.CouponID == id);
				_db.Coupons.Remove(couponRemove);
				await _db.SaveChangesAsync();

				_response.Result = _mapper.Map<CouponDTO>(couponRemove);
				return Ok(_response);
			}
			catch (Exception ex)
			{

			}
			return BadRequest();
		}
	}
}
