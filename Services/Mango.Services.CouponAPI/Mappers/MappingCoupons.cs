using AutoMapper;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;

namespace Mango.Services.CouponAPI.Mappers
{

    public class MappingCoupons : Profile
    {

        public MappingCoupons()
        {
            // CreateMap<CouponDto, Coupon>();
            // CreateMap<Coupon, CouponDto>()
            //     .ForMember(dst => dst.CouponCode, opt => opt.MapFrom(src => src.CouponCode))
            //     .ForMember(dst => dst.DiscountAmount , opt => opt.MapFrom(src => src.DiscountAmount))
            //     .ForMember(dst => dst.CouponId , opt => opt.MapFrom(src => src.CouponId));
            CreateMap<Coupon, CouponDto>();
            CreateMap<CouponDto, Coupon>();
        }
    }
}