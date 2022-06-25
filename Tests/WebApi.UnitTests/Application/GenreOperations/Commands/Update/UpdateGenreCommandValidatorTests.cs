using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.Update;

public class UpdateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData("G")]
    [InlineData("Ge")]
    [InlineData("Gen")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name)
    {
        // Arrange
        UpdateGenreCommand command = new UpdateGenreCommand(null);
        command.Model = new UpdateGenreModel{ Name = name };

        // Act
        UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeGreaterThan(0);        
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
    {
        // Arrange
        UpdateGenreCommand command = new UpdateGenreCommand(null);
        command.Model = new UpdateGenreModel{ Name = "Genr" };
        // Act
        UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
