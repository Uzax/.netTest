using AutoMapper;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Mango.Services.CouponAPI.Repository;

namespace Mango.Services.CouponAPI.Services
{
    public class CouponService : ICouponService
    {
        // private readonly ICouponRepository _couponRepository;
        private readonly IMapper _mapper;
        private ResponseDto _response;
        
        private readonly IUnitOfWork _unitOfWork;

        public CouponService( IMapper mapper , IUnitOfWork unitOfWork )//, ICouponRepository couponRepository )
        {
            // _couponRepository = couponRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _response = new ResponseDto();
            
        }
        
        
        public async  Task<ResponseDto> GetAllCouponsAsync()
        {
            var coupouns = await _unitOfWork.Coupons.GetAllAsync(); //.GetCouponsAsync();
            // _response.Result =  _mapper.Map<IEnumerable<CouponDto>>(coupouns);
            _response.Result = coupouns;
            _response.IsSuccess = true;
            return _response;
        }

        public async Task<ResponseDto> GetCouponByIdAsync(int couponId)
        {
            var coupon = await _unitOfWork.Coupons.GetByIdAsync(couponId);//.GetCouponByIdAsync(couponId);
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
            var coupon = await _unitOfWork.Coupons.Find(coupon => coupon.CouponCode == CouponCode);//.GetCouponByCodeAsync(CouponCode);
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

            var checkExist = await _unitOfWork.Coupons.Find(coupon => coupon.CouponCode == couponDto.CouponCode);// .GetCouponByCodeAsync(couponDto.CouponCode);
            if (checkExist != null)
            {
                _response.IsSuccess = false;
                _response.Message =  "Duplicate Coupon Code";
                return _response;
            }
            
            
            var coupon = _mapper.Map<Coupon>(couponDto);
            // coupon.CouponId = curreentId;
            coupon.MinAmount = 20;

            await _unitOfWork.Coupons.AddAsync(coupon); //.AddCouponAsync(coupon);
            _unitOfWork.Complete();
            
            _response.IsSuccess = true;
            _response.Message = "Coupon Added Successfully";
            return _response; 
        }

        public async Task<ResponseDto> DeleteCouponbyIdAsync(int id )
        {
          var coupon = await _unitOfWork.Coupons.GetByIdAsync(id);//.GetCouponByIdAsync(id);
          if (coupon == null)
          {
              _response.IsSuccess = false;
              _response.Message = "No such Coupon";
              return _response;
          }
          
          // await _couponRepository.DeleteCouponByIdAsync(id);
          _unitOfWork.Coupons.Delete(coupon);
          _unitOfWork.Complete();
          _response.IsSuccess = true;
          _response.Message = "Coupon Deleted Successfully";
          return _response;
        }

        public async Task<ResponseDto> DeleteCouponbyCodeAsync(string CouponCode)
        {
            var coupon = await _unitOfWork.Coupons.Find(coupon => coupon.CouponCode == CouponCode); //.GetCouponByCodeAsync(CouponCode);
            if (coupon == null)
            {
                _response.IsSuccess = false;
                _response.Message = "No such Coupon";
                return _response;
            }
          
            _unitOfWork.Coupons.Delete(coupon);
            _unitOfWork.Complete();
            // await _couponRepository.DeleteCouponByCodeAsync(CouponCode);
            _response.IsSuccess = true;
            _response.Message = "Coupon Deleted Successfully";
            return _response;
        }

        public async Task<ResponseDto> UpdateCouponAsync(CouponDto couponDto , int id)
        {
            // var coupon = await _couponRepository.GetCouponByIdAsync(id);
            var coupon = await _unitOfWork.Coupons.GetByIdAsync(id);
            if (coupon == null)
            {
                _response.IsSuccess = false;
                _response.Message = "No such Coupon";
                return _response;
            }
           
            var couponToUpdate = _mapper.Map<Coupon>(couponDto);
           couponToUpdate.MinAmount = coupon.MinAmount;
           couponToUpdate.CouponId = coupon.CouponId;
           
            // await _couponRepository.UpdateCouponAsync(couponToUpdate);
            _unitOfWork.Coupons.Update(couponToUpdate);
            _unitOfWork.Complete();
            _response.IsSuccess = true;
            _response.Message = "Coupon Updated Successfully";
            return _response;
        }
    }
}