using FluentValidation;
using Test.WebAPI.Models.ApiRequests;

namespace Test.WebAPI.Infrastructure.Validators
{
    public class PostRequestValidator : AbstractValidator<PostRequest>
    {
        public PostRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage("Id must be not empty");
            RuleFor(x => x.Time).Must(IsValidDateTime).WithMessage("Date must be in valid format");
            RuleFor(x => x.Url).Must(ValidateUri).WithMessage("Url must be in valid format");
        }
        
        private bool IsValidDateTime(string? date)
        {
            return date is null || DateTime.TryParse(date, out _);
        }
        
        public bool ValidateUri(string? uri)
        {
            return uri is null || Uri.TryCreate(uri, UriKind.Absolute, out _);
        }
    }
}