using FluentValidation;

namespace WebApi.Application.GenreOperations.Command.UpdateGenre;

public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand> 
{
    public UpdateGenreCommandValidator()
    {
        RuleFor(cmd=>cmd.Model.Name).MinimumLength(4).When(cmd=>cmd.Model.Name.Trim()!=String.Empty);
    }
}