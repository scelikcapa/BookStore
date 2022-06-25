using FluentValidation;

namespace WebApi.Application.BookOperations.Commands.CreateBook;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(cmd=>cmd.Model.GenreId).GreaterThan(0);
        RuleFor(cmd=>cmd.Model.PageCount).GreaterThan(0);
        RuleFor(cmd=>cmd.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
        RuleFor(cmd=>cmd.Model.Title).NotEmpty().MinimumLength(4);
    }
}