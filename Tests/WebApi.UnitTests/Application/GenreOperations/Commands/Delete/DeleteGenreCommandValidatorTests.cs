using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.Delete;

public class DeleteGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenGivenGenreIdIsNotGreaterThenZero_Validator_ShouldReturnError()
    {
        // Arrange
        DeleteGenreCommand command = new DeleteGenreCommand(null);
        command.GenreId = 0;

        // Act
        DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeGreaterThan(0);        
    }

    [Fact]
    public void WhenGivenGenreIdIsGreaterThenZero_Validator_ShouldNotReturnError()
    {
        // Arrange
        DeleteGenreCommand command = new DeleteGenreCommand(null);
        command.GenreId = 1;
        
        // Act
        DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
