using FluentValidation;

namespace WebApi.Application.GenreOperations.Commands.DeleteGenre;

public class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand>
{
    public DeleteGenreCommandValidator()
    {
        RuleFor(cmd=>cmd.GenreId).GreaterThan(0);
    }

}