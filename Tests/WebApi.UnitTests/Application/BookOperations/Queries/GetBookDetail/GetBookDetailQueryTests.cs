using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBookDetail;

public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
{   
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public GetBookDetailQueryTests(CommonTestFixture testFixture)
    {
        mapper = testFixture.Mapper;
        context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenBookIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
    {
        // arrange
        var book = new Book{ Title = "WhenGivenBookIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn", PageCount = 100, PublishDate = new DateTime(1990,02,02), GenreId = 1};
        context.Books.Add(book);
        context.SaveChanges();
        
        context.Books.Remove(book);
        context.SaveChanges();

        GetBookDetailQuery command = new GetBookDetailQuery(context, mapper);
        command.BookId= book.Id;

        // act - assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kayıt bulunamadı.");
    }

    [Fact]
    public void WhenGivenBookIdDoesExistInDb_Book_ShouldBeReturned()
    {
        // Arrange
        var book = new Book{ Title = "WhenGivenBookIdDoesExistInDb_Book_ShouldBeRetuned", PageCount = 100, PublishDate = new DateTime(1990,02,02), GenreId = 1};
        context.Books.Add(book);
        context.SaveChanges();

        GetBookDetailQuery command = new GetBookDetailQuery(context, mapper);
        command.BookId = book.Id;

        // Act
        var bookReturned = FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        bookReturned.Should().NotBeNull();
        bookReturned.Id.Should().Be(book.Id);
        bookReturned.Title.Should().Be(book.Title);
        bookReturned.PageCount.Should().Be(book.PageCount);
    }
}