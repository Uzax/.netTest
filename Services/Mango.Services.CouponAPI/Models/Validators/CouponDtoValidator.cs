using FluentValidation;
using Mango.Services.CouponAPI.Models.Dto;

namespace Mango.Services.CouponAPI.Models.Validators
{

    public class CouponDtoValidator : AbstractValidator<CouponDto>
    {
        public CouponDtoValidator()
        {
            RuleFor(x => x.CouponCode)
                .NotEmpty().WithName("CouponCode is required")
                .Length(1,20).WithMessage("CouponCode must be between 1 and 20 characters.");
            
            
            RuleFor(x => x.DiscountAmount)
                .GreaterThan(0).WithMessage("Discount Amount must be greater than zero.");
            
        }
    }
}