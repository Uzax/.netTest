using AutoMapper;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Mango.Services.CouponAPI.Repository;

namespace Mango.Services.CouponAPI.Services
{
    public class CouponService : ICouponService
    {
        private readonly ICouponRepository _couponRepository;
        private readonly IMapper _mapper;
        private ResponseDto _response;

        public CouponService(ICouponRepository couponRepository , IMapper mapper)
        {
            _couponRepository = couponRepository;
            _mapper = mapper;
            _response = new ResponseDto();
            
        }
        
        
        public async  Task<ResponseDto> GetAllCouponsAsync()
        {
            var coupouns = await _couponRepository.GetCouponsAsync();
            _response.Result =  _mapper.Map<IEnumerable<CouponDto>>(coupouns);
            _response.IsSuccess = true;
            return _response;
        }

        public async Task<ResponseDto> GetCouponByIdAsync(int couponId)
        {
            var coupon = await _couponRepository.GetCouponByIdAsync(couponId);
            if (coupon == null)
            {
                _response.IsSuccess = false;
                _response.Message =  "No such Coupon";
                return _response;
            }
            _response.Result =  _mapper.Map<CouponDto>(coupon);
            _response.IsSuccess = true;
            return _response; 
        }

        public async Task<ResponseDto> GetCouponByCode(string CouponCode)
        {
            var coupon = await _couponRepository.GetCouponByCodeAsync(CouponCode);
            if (coupon == null)
            {
                _response.IsSuccess = false;
                _response.Message =  "No such Coupon";
                return _response;
            }
            _response.Result =  _mapper.Map<CouponDto>(coupon);
            _response.IsSuccess = true;
            return _response; 
        }

        public async  Task<ResponseDto> AddCouponAsync(CouponDto couponDto)
        {
            var LastEl = await _couponRepository.GetLastElementAsync();
            int curreentId = LastEl.CouponId + 1;
            
            var coupon = _mapper.Map<Coupon>(couponDto);
            coupon.CouponId = curreentId;
            coupon.MinAmount = 20;

            _couponRepository.AddCouponAsync(coupon);
            
            _response.IsSuccess = true;
            _response.Message = "Coupon Added Successfully";
            return _response; 
        }
    }
}