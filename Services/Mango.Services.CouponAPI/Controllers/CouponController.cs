using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Mango.Services.CouponAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCoupons()
        {
            var coupons = await _couponService.GetAllCouponsAsync();
            return Ok(coupons);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCoupon(int id)
        {
            var coupon = await _couponService.GetCouponByIdAsync(id);
            return Ok(coupon);
        }

        [HttpGet("bycode/{code}")]
        public async Task<ActionResult<Coupon>> GetCouponByCode(string code)
        {
            var coupon = await _couponService.GetCouponByCode(code);
            return Ok(coupon);
        }

        [HttpPost]
        public async Task<ActionResult> AddCoupon(CouponDto couponDto)
        {
            var coupon = await _couponService.AddCouponAsync(couponDto);
            return Ok(coupon);
            
        }
        
       
    }
}