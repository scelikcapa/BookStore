using FluentValidation;

namespace WebApi.Application.GenreOperations.Command.DeleteGenre;

public class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand>
{
    public DeleteGenreCommandValidator()
    {
        RuleFor(cmd=>cmd.GenreId).GreaterThan(0);
    }

}