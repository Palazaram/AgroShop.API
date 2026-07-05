using AgroShop.Application.Dto.AuthDto;
using AgroShop.Application.Extensions;
using AgroShop.Core.ValueObjects;
using FluentValidation;

namespace AgroShop.API.Validators.AuthenticationValidators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator()
        {
            //RuleFor(x => x.Login).MustBeValueObject(Login.Create);
            RuleFor(x => x.LastName).MustBeValueObject(LastName.Create);
            RuleFor(x => x.FirstName).MustBeValueObject(FirstName.Create);
            RuleFor(x => x.Patronymic).MustBeValueObject(Patronymic.Create);
            RuleFor(x => x.Email).MustBeValueObject(Email.Create);
            RuleFor(x => x.Phone).MustBeValueObject(Phone.Create);
            RuleFor(x => x.Password).MustBeValueObject(Password.Create);
        }
    }
}
