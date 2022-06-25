using FluentAssertions;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperations.Commands.Update;

public class UpdateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(0, "Lord",1,1)]
    [InlineData(1, "",1,1)]
    [InlineData(1, "Lor",1,1)]
    [InlineData(1, "Lord",0,1)]
    [InlineData(1, "Lord",1,0)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int bookId, string title, int pageCount, int genreId)
    {
        // Arrange
        UpdateBookCommand command = new UpdateBookCommand(null, null);
        command.BookId = bookId;
        command.Model = new UpdateBookModel{
            Title = title,
            PageCount = pageCount,
            PublishDate = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd"),
            GenreId = genreId
        };

        // Act
        UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeGreaterThan(0);        
    }

    [Fact]
    public void WhenDateTimeEqualNowIsGiven_Validator_ShouldReturnError()
    {
        // Arrange
        UpdateBookCommand command = new UpdateBookCommand(null, null);
        command.BookId = 1;
        command.Model = new UpdateBookModel{
            Title = "Lord Of The Rings",
            PageCount = 100,
            PublishDate = DateTime.Now.ToString("yyyy-MM-dd"),
            GenreId = 1
        };
        // Act
        UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeGreaterThan(0);   
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
    {
        // Arrange
        UpdateBookCommand command = new UpdateBookCommand(null, null);
        command.BookId = 1;
        command.Model = new UpdateBookModel{
            Title = "Lord Of The Rings",
            PageCount = 100,
            PublishDate = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd"),
            GenreId = 1
        };
        // Act
        UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
