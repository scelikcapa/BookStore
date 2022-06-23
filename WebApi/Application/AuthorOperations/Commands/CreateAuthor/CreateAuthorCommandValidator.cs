using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor;

public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(c=>c.Model.Name).NotEmpty().MinimumLength(3);
        RuleFor(c=>c.Model.Surname).NotEmpty().MinimumLength(2);
        RuleFor(c=>c.Model.BirthDate).GreaterThan(DateTime.Now.AddYears(-18));
    }
}