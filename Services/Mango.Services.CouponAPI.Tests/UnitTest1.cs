
using AutoMapper;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Mango.Services.CouponAPI.Repository;
using Mango.Services.CouponAPI.Services;
using Moq;
namespace Mango.Services.CouponAPI.Tests
{
    public class UnitTest1
    {
       
        private readonly CouponService _couponService;
        private readonly Mock<ICouponRepository> _couponRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly IMapper _mapper;
        public UnitTest1()
        {
            // Arrange: Initialize the service to be tested.
            _couponRepositoryMock = new Mock<ICouponRepository>();
            _mapperMock = new Mock<IMapper>();
            _mapper = _mapperMock.Object;
            
            _couponService = new CouponService(_couponRepositoryMock.Object, _mapperMock.Object);
            
            
        }

        [Fact]
        public async void Test1()
        {
            var expectedCoupon = new Coupon { CouponCode = "10", DiscountAmount = 20, CouponId = 0, MinAmount = 20 };
            var expectedCouponDTO = new CouponDto { CouponCode = "10", DiscountAmount = 20 };
            var expectedResponseDto = new ResponseDto{IsSuccess = true , Result = expectedCouponDTO , Message = ""};

        _couponRepositoryMock.Setup(repo => repo.GetCouponByIdAsync(0))
                .ReturnsAsync(expectedCoupon);

        _mapperMock.Setup(repo => repo.Map<CouponDto>(expectedCoupon)).Returns(expectedCouponDTO);
            //Act
            var result = await _couponService.GetCouponByIdAsync(0);
            
            //Assert
            Assert.NotNull(result);
            _mapperMock.Verify(x => x.Map<CouponDto>(expectedCoupon), Times.Never);
            Assert.Equal(expectedResponseDto.ToString(), result.ToString());

        }
    }
}