using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Mango.Services.CouponAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpGet]
        // [Authorize]
        public async Task<ActionResult> GetAllCoupons()
        {
            var coupons = await _couponService.GetAllCouponsAsync();
            return Ok(coupons);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCoupon(int id)
        {
            var coupon = await _couponService.GetCouponByIdAsync(id);
            if (coupon.IsSuccess)
            {
                return Ok(coupon);
            }
            return NotFound(coupon);
        }

        [HttpGet("bycode/{code}")]
        public async Task<ActionResult<Coupon>> GetCouponByCode(string code)
        {
            var coupon = await _couponService.GetCouponByCode(code);
            if (coupon.IsSuccess)
            {
                return Ok(coupon);
            }

            return NotFound(coupon);
        }

        [HttpPost]
        public async Task<ActionResult> AddCoupon(CouponDto couponDto)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors
            }
            
            var coupon = await _couponService.AddCouponAsync(couponDto);

            if (coupon.IsSuccess)
            {
                return Ok(coupon);
            }
            return BadRequest(coupon);
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCoupon(int id)
        {
            var result = await _couponService.DeleteCouponbyIdAsync(id);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        
        [HttpDelete("bycode/{code}")]
        public async Task<ActionResult> DeleteCoupon(string code)
        {
            var result = await _couponService.DeleteCouponbyCodeAsync(code);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCoupon(int id, CouponDto couponDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var result = await _couponService.UpdateCouponAsync(couponDto , id);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            
            return BadRequest(result);
        }
       
        
    }
}