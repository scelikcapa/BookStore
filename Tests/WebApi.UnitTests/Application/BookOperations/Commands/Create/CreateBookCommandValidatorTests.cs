using FluentAssertions;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperations.Commands.Create;

public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData("",1,1)]
    [InlineData("Lord",0,1)]
    [InlineData("Lord",1,0)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId)
    {
        // Arrange
        CreateBookCommand command = new CreateBookCommand(null, null);
        command.Model = new CreateBookModel{
            Title = title,
            PageCount = pageCount,
            PublishDate = DateTime.Now.Date.AddYears(-1),
            GenreId = genreId
        };

        // Act
        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeGreaterThan(0);        
    }

    [Fact]
    public void WhenDateTimeEqualNowIsGiven_Validator_ShouldReturnError()
    {
        // Arrange
        CreateBookCommand command = new CreateBookCommand(null, null);
        command.Model = new CreateBookModel{
            Title = "Lord Of The Rings",
            PageCount = 100,
            PublishDate = DateTime.Now.Date,
            GenreId = 1
        };
        // Act
        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeGreaterThan(0);   
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
    {
        // Arrange
        CreateBookCommand command = new CreateBookCommand(null, null);
        command.Model = new CreateBookModel{
            Title = "Lord Of The Rings",
            PageCount = 100,
            PublishDate = DateTime.Now.Date.AddDays(-1),
            GenreId = 1
        };
        // Act
        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
