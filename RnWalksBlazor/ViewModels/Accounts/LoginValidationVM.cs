using FluentValidation;

namespace RnWalksBlazor.ViewModels.Accounts
{
    public class LoginValidationVM : AbstractValidator<LoginVM>
    {
        public LoginValidationVM()
        {
            RuleFor(_ => _.Username)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Not a valid username");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Invalid credentials")
                .MinimumLength(6).WithMessage("Invalid credentials")
                .MaximumLength(16).WithMessage("Invalid credentials")
                    .Matches(@"[A-Z]+").WithMessage("Invalid credentials.")
                    .Matches(@"[a-z]+").WithMessage("Invalid credentials.")
                    .Matches(@"[0-9]+").WithMessage("Invalid credentials.")
                    .Matches(@"[\!\?\*\@\.]+").WithMessage("Invalid credentials");
        }
        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<LoginVM>.CreateWithOptions((LoginVM)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
