using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.Update;

public class UpdateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(" ","S")]
    [InlineData("N","")]
    [InlineData("N"," ")]
    [InlineData("N","S")]
    [InlineData("N","Su")]
    [InlineData("Na","")]
    [InlineData("Na"," ")]
    [InlineData("Na","S")]
    [InlineData("Na","Su")]
    [InlineData("Nam","S")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name, string surname)
    {
        // Arrange
        UpdateAuthorCommand command = new UpdateAuthorCommand(null, null);
        command.Model = new UpdateAuthorModel{ 
            Name = name,
            Surname = surname,
            BirthDate = new DateTime(1990,01,01)
        };

        // Act
        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeGreaterThan(0);        
    }

    [Fact]
    public void WhenAuthorIsUnder18_Validator_ShouldReturnError()
    {
        // Arrange
        UpdateAuthorCommand command = new UpdateAuthorCommand(null, null);
        command.Model = new UpdateAuthorModel{
            Name = "WhenDateTimeEqualNowIsGiven",
            Surname = "Validator_ShouldReturnError",
            BirthDate = DateTime.Now.AddYears(-18).AddDays(1)
        };
        // Act
        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeGreaterThan(0);   
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
    {
        // Arrange
        UpdateAuthorCommand command = new UpdateAuthorCommand(null, null);
        command.AuthorId = 1;
        command.Model = new UpdateAuthorModel{
            Name = "WhenValidInputsAreGiven",
            Surname = "Validator_ShouldNotReturnError",
            BirthDate = new DateTime(1990,01,01)
        };
        // Act
        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
