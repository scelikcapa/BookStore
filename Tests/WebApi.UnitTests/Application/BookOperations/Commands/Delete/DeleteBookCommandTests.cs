using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperations.Commands.Delete;

public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
{   
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public DeleteBookCommandTests(CommonTestFixture testFixture)
    {
        mapper = testFixture.Mapper;
        context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenBookIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var command = new DeleteBookCommand(context);
        command.BookId = -1;

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek kitap bulunamadı!");
    }

    [Fact]
    public void WhenGivenBookIdExistsInDb_Book_ShouldBeDeleted()
    {
        // Arrange
        var bookInDb = new Book{ Title = "WhenGivenBookIdExistsInDb_Book_ShouldBeDeleted", PageCount = 100, PublishDate = new DateTime(1990,02,02), GenreId = 1};
        context.Books.Add(bookInDb);
        context.SaveChanges();

        var command = new DeleteBookCommand(context);
        command.BookId = bookInDb.Id;

        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        var book = context.Books.SingleOrDefault(b=> b.Id == bookInDb.Id);
        book.Should().BeNull();
    }
}