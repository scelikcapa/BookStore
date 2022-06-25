using System.Globalization;
using FluentValidation;

namespace WebApi.Application.BookOperations.Commands.UpdateBook;

public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(cmd=>cmd.BookId).GreaterThan(0);
        RuleFor(cmd=>cmd.Model.GenreId).GreaterThan(0);
        RuleFor(cmd=>cmd.Model.PageCount).GreaterThan(0);
        RuleFor(cmd=>DateTime.ParseExact(cmd.Model.PublishDate, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date).NotEmpty().LessThan(DateTime.Now.Date);
        RuleFor(cmd=>cmd.Model.Title).NotEmpty().MinimumLength(4);
    }
}