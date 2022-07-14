using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.Delete;

public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
{   
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public DeleteAuthorCommandTests(CommonTestFixture testFixture)
    {
        mapper = testFixture.Mapper;
        context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenAuthorIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var command = new DeleteAuthorCommand(context);
        command.AuthorId = -1;

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek Yazar bulunamadı.");
    }

    [Fact]
    public void WhenGivenAuthorIdExistsInDb_Author_ShouldBeDeleted()
    {
        // Arrange
        var authorInDb = new Author{ 
            Name = "WhenGivenAuthorIdExistsInDb", 
            Surname = "Author_ShouldBeDeleted", 
            BirthDate = new DateTime(1990,02,02)};
        context.Authors.Add(authorInDb);
        context.SaveChanges();

        var command = new DeleteAuthorCommand(context);
        command.AuthorId = authorInDb.Id;

        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        var author = context.Authors.SingleOrDefault(b=> b.Id == authorInDb.Id);
        author.Should().BeNull();
    }
}