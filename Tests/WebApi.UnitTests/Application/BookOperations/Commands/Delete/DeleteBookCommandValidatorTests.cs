using FluentAssertions;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperations.Commands.Delete;

public class DeleteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenGivenBookIdIsNotGreaterThenZero_Validator_ShouldReturnError()
    {
        // Arrange
        DeleteBookCommand command = new DeleteBookCommand(null);
        command.BookId = 0;

        // Act
        DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeGreaterThan(0);        
    }

    [Fact]
    public void WhenGivenBookIdIsGreaterThenZero_Validator_ShouldNotReturnError()
    {
        // Arrange
        DeleteBookCommand command = new DeleteBookCommand(null);
        command.BookId = 1;
        
        // Act
        DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
