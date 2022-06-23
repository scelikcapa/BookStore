using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor;

public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand> 
{
    public UpdateAuthorCommandValidator()
    {
        RuleFor(cmd=>cmd.Model.Name).MinimumLength(3).When(cmd=>!String.IsNullOrEmpty(cmd.Model.Name.Trim()));
        RuleFor(cmd=>cmd.Model.Surname).MinimumLength(2).When(cmd=>!String.IsNullOrEmpty(cmd.Model.Surname.Trim()));
        RuleFor(cmd=>cmd.Model.BirthDate).LessThan(DateTime.Now.AddYears(-18));
    }
}