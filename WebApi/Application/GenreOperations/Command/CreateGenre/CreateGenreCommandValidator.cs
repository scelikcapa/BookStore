using FluentValidation;

namespace WebApi.Application.GenreOperations.Command.CreateGenre;

public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
{
    public CreateGenreCommandValidator()
    {
        RuleFor(cmd=>cmd.Model.Name).NotEmpty().MinimumLength(4);
    }
}