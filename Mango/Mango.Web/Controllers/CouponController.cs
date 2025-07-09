using Mango.Mango.web.Models;
using Mango.Mango.Web.Models;
using Mango.Web.Models;
using Mango.Web.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Mango.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _iCouponService;

        public CouponController(ICouponService iCouponService) { 
            _iCouponService = iCouponService;
        }

        public async Task<IActionResult?> CouponIndex()
        {
            List<CouponDTO>? list = new();
            ResponseDTO? responseDTO = await _iCouponService.GetAllCouponsAsync();
            if (responseDTO != null && responseDTO.isSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CouponDTO>>(Convert.ToString(responseDTO.Result));
            }
            return View(list);
        }

        public async Task<IActionResult> CouponCreate()
        {
            return View();
        }

        public async Task<IActionResult> CreateCoupon(CouponDTO request)
        {
            ResponseDTO? responseDTO = await _iCouponService.CreateCouponAsync(request);
            if(responseDTO != null && responseDTO.isSuccess)
            {
                return Redirect("/coupon/CouponIndex");
            }
            return NotFound();
        }

        public async Task<IActionResult> DeleteCoupon(int id)
        {
            ResponseDTO? responseDTO = await _iCouponService.GetCouponByIdAsync(id);
            if (responseDTO != null && responseDTO.isSuccess)
            {
                var couponDTO = JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(responseDTO.Result));
                return View(couponDTO);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCoupon(CouponDTO couponDTO)
        {
            ResponseDTO? responseDTO = await _iCouponService.DeleteCouponAsync(couponDTO.CouponId);
            if (responseDTO != null && responseDTO.isSuccess)
            {
                return Redirect(nameof(CouponIndex));
            }
            return BadRequest();
        }
    }
}
