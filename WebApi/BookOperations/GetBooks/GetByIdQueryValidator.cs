using FluentValidation;

namespace WebApi.BookOperations.GetBooks;

public class GetByIdQueryValidator : AbstractValidator<GetByIdQuery>
{
    public GetByIdQueryValidator()
    {
        RuleFor(q=>q.BookId).GreaterThan(0);
    }
}