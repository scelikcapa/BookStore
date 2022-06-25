using FluentAssertions;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenreDetail;

public class GetGenreDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenGivenGenreIdIsNotGreaterThenZero_Validator_ShouldBeReturnError()
    {
        // Arrange
        GetGenreDetailQuery command = new GetGenreDetailQuery(null, null);
        command.GenreId = 0;

        // Act
        GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeGreaterThan(0);        
    }

    [Fact]
    public void WhenGivenGenreIdIsGreaterThenZero__Validator_ShouldNotReturnError()
    {
        // Arrange
        GetGenreDetailQuery command = new GetGenreDetailQuery(null, null);
        command.GenreId = 1;
        // Act
        GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
