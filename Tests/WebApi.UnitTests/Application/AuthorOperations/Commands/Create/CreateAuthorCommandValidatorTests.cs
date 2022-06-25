using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.Create;

public class CreateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData("","")]
    [InlineData(""," ")]
    [InlineData("","  ")]
    [InlineData("","S")]
    [InlineData("","Su")]
    [InlineData("  ","")]
    [InlineData("  "," ")]
    [InlineData("  ","  ")]
    [InlineData("  ","S")]
    [InlineData("  ","Su")]
    [InlineData("   ","")]
    [InlineData("   "," ")]
    [InlineData("   ","  ")]
    [InlineData("   ","S")]
    [InlineData("   ","Su")]
    [InlineData("N","")]
    [InlineData("N"," ")]
    [InlineData("N","  ")]
    [InlineData("N","S")]
    [InlineData("N","Su")]
    [InlineData("Na","")]
    [InlineData("Na"," ")]
    [InlineData("Na","  ")]
    [InlineData("Na","S")]
    [InlineData("Na","Su")]
    [InlineData("Nam","")]
    [InlineData("Nam"," ")]
    [InlineData("Nam","  ")]
    [InlineData("Nam","S")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name, string surname)
    {
        // Arrange
        CreateAuthorCommand command = new CreateAuthorCommand(null, null);
        command.Model = new CreateAuthorModel{
            Name = name,
            Surname = surname,
            BirthDate = DateTime.Now.Date.AddYears(-19)
        };

        // Act
        CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeGreaterThan(0);        
    }

    [Fact]
    public void WhenAuthorIsUnder18_Validator_ShouldReturnError()
    {
        // Arrange
        CreateAuthorCommand command = new CreateAuthorCommand(null, null);
        command.Model = new CreateAuthorModel{
            Name = "WhenAuthorIsUnder18",
            Surname = "Validator_ShouldReturnError",
            BirthDate = DateTime.Now.Date.AddYears(-18).AddDays(1)
        };
        // Act
        CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeGreaterThan(0);   
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
    {
        // Arrange
        CreateAuthorCommand command = new CreateAuthorCommand(null, null);
        command.Model = new CreateAuthorModel{
            Name = "WhenValidInputsAreGiven",
            Surname = "Validator_ShouldNotReturnError",
            BirthDate = DateTime.Now.Date.AddYears(-18)
        };
        // Act
        CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
