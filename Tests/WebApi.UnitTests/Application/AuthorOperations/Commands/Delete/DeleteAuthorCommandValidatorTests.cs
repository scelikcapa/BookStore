using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.Delete;

public class DeleteAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenGivenAuthorIdIsNotGreaterThenZero_Validator_ShouldReturnError()
    {
        // Arrange
        DeleteAuthorCommand command = new DeleteAuthorCommand(null);
        command.AuthorId = 0;

        // Act
        DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeGreaterThan(0);        
    }

    [Fact]
    public void WhenGivenAuthorIdIsGreaterThenZero_Validator_ShouldNotReturnError()
    {
        // Arrange
        DeleteAuthorCommand command = new DeleteAuthorCommand(null);
        command.AuthorId = 1;
        
        // Act
        DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
