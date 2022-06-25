using FluentAssertions;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries.GetAuthorDetail;

public class GetAuthorDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenGivenAuthorIdIsNotGreaterThenZero_Validator_ShouldBeReturnError()
    {
        // Arrange
        GetAuthorDetailQuery command = new GetAuthorDetailQuery(null, null);
        command.AuthorId = 0;

        // Act
        GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeGreaterThan(0);        
    }

    [Fact]
    public void WhenGivenAuthorIdIsGreaterThenZero__Validator_ShouldNotReturnError()
    {
        // Arrange
        GetAuthorDetailQuery command = new GetAuthorDetailQuery(null, null);
        command.AuthorId = 1;
        // Act
        GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
