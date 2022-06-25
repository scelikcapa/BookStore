using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.Create;

public class CreateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    [InlineData("Nam")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name)
    {
        // Arrange
        CreateGenreCommand command = new CreateGenreCommand(null, null);
        command.Model = new CreateGenreModel{ Name = name };

        // Act
        CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeGreaterThan(0);        
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
    {
        // Arrange
        CreateGenreCommand command = new CreateGenreCommand(null, null);
        command.Model = new CreateGenreModel{ Name = "Name"};
        // Act
        CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
