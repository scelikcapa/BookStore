using FluentAssertions;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBookDetail;

public class GetBookDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenGivenBookIdIsNotGreaterThenZero_Validator_ShouldBeReturnError()
    {
        // Arrange
        GetBookDetailQuery command = new GetBookDetailQuery(null, null);
        command.BookId = 0;

        // Act
        GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeGreaterThan(0);        
    }

    [Fact]
    public void WhenGivenBookIdIsGreaterThenZero__Validator_ShouldNotReturnError()
    {
        // Arrange
        GetBookDetailQuery command = new GetBookDetailQuery(null, null);
        command.BookId = 1;
        // Act
        GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
