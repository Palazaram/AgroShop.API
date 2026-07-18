using FluentValidation;
using AgroShop.Application.Extensions;
using AgroShop.Core.ValueObjects;
using AgroShop.Application.Dto.AuthDto;

namespace AgroShop.Application.Validators.AuthenticationValidators
{
    public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
    {
        public LoginUserDtoValidator()
        {
            RuleFor(x => x.Phone).MustBeValueObject(Phone.Create);
            RuleFor(x => x.Password).MustBeValueObject(Password.Create);
        }
    }
}
