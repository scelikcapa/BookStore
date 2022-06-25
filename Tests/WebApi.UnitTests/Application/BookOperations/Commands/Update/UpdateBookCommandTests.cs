using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperations.Commands.Update;

public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
{   
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public UpdateBookCommandTests(CommonTestFixture textfixture)
    {
        mapper = textfixture.Mapper;
        context = textfixture.Context;
    }

    [Fact]
    public void WhenGivenBookIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var command = new UpdateBookCommand(context, mapper);
        command.BookId = -1;

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kayıt bulunamadı. Lütfen ilk önce kaydı oluşturun");
    }

    [Fact]
    public void WhenGivenBookIdExistsInDb_Book_ShouldBeUpdated()
    {
        // Arrange
        var bookInDb = new Book{ Title = "WhenGivenBookIdExistsInDb_Book_ShouldBeUpdated", PageCount = 100, PublishDate = new DateTime(1990,02,02), GenreId = 1};
        var bookCompared = new Book{ 
                            Title = bookInDb.Title, 
                            PageCount = bookInDb.PageCount, 
                            PublishDate = bookInDb.PublishDate, 
                            GenreId = bookInDb.GenreId};
        context.Books.Add(bookInDb);
        context.SaveChanges();

        var command = new UpdateBookCommand(context,mapper);
        command.BookId = bookInDb.Id;
        command.Model = new UpdateBookModel{ Title = "UpdatedTitle", PageCount = 1, PublishDate = (new DateTime(1991,2,2)).ToString("yyyy-MM-dd"), GenreId = 2};

        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        var bookUpdated = context.Books.SingleOrDefault(b=> b.Id == bookInDb.Id);
        bookUpdated.Should().NotBeNull();
        bookUpdated.PageCount.Should().NotBe(bookCompared.PageCount);
        bookUpdated.PublishDate.Should().NotBe(bookCompared.PublishDate);
        bookUpdated.GenreId.Should().NotBe(bookCompared.GenreId);

    }
}