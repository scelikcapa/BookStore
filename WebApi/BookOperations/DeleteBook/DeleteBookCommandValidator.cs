using FluentValidation;

namespace WebApi.BookOperations.DeleteBook;

public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidator()
    {
        RuleFor(c=>c.BookId).GreaterThan(0);
    }
}