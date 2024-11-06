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
            // _response.Result =  _mapper.Map<IEnumerable<CouponDto>>(coupouns);
            _response.Result = coupouns;
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
            // _response.Result =  _mapper.Map<CouponDto>(coupon);
            _response.Result = coupon;
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
            // _response.Result =  _mapper.Map<CouponDto>(coupon);
            _response.Result = coupon;
            _response.IsSuccess = true;
            return _response; 
        }

        public async  Task<ResponseDto> AddCouponAsync(CouponDto couponDto)
        {
            // var LastEl = await _couponRepository.GetLastElementAsync();
            // int curreentId = LastEl.CouponId + 1;

            var checkExist = await _couponRepository.GetCouponByCodeAsync(couponDto.CouponCode);
            if (checkExist != null)
            {
                _response.IsSuccess = false;
                _response.Message =  "Duplicate Coupon Code";
                return _response;
            }
            
            
            var coupon = _mapper.Map<Coupon>(couponDto);
            // coupon.CouponId = curreentId;
            coupon.MinAmount = 20;

            _couponRepository.AddCouponAsync(coupon);
            
            _response.IsSuccess = true;
            _response.Message = "Coupon Added Successfully";
            return _response; 
        }

        public async Task<ResponseDto> DeleteCouponbyIdAsync(int id )
        {
          var coupon = await _couponRepository.GetCouponByIdAsync(id);
          if (coupon == null)
          {
              _response.IsSuccess = false;
              _response.Message = "No such Coupon";
              return _response;
          }
          
          await _couponRepository.DeleteCouponByIdAsync(id);
          _response.IsSuccess = true;
          _response.Message = "Coupon Deleted Successfully";
          return _response;
        }

        public async Task<ResponseDto> DeleteCouponbyCodeAsync(string CouponCode)
        {
            var coupon = await _couponRepository.GetCouponByCodeAsync(CouponCode);
            if (coupon == null)
            {
                _response.IsSuccess = false;
                _response.Message = "No such Coupon";
                return _response;
            }
          
            await _couponRepository.DeleteCouponByCodeAsync(CouponCode);
            _response.IsSuccess = true;
            _response.Message = "Coupon Deleted Successfully";
            return _response;
        }

        public async Task<ResponseDto> UpdateCouponAsync(CouponDto couponDto , int id)
        {
            var coupon = await _couponRepository.GetCouponByIdAsync(id);

            if (coupon == null)
            {
                _response.IsSuccess = false;
                _response.Message = "No such Coupon";
                return _response;
            }
           
            var couponToUpdate = _mapper.Map<Coupon>(couponDto);
           couponToUpdate.MinAmount = coupon.MinAmount;
           couponToUpdate.CouponId = coupon.CouponId;
           
            await _couponRepository.UpdateCouponAsync(couponToUpdate);
            _response.IsSuccess = true;
            _response.Message = "Coupon Updated Successfully";
            return _response;
        }
    }
}