using AutoMapper;
using FluentAssertions;
using WebApi.BookOperations.CreateBook;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateCommand;

public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
{   
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public CreateBookCommandTests(CommonTestFixture textfixture)
    {
        mapper = textfixture.Mapper;
        context = textfixture.Context;
    }

    [Fact]
    public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        // arrange
        var book = new Book{ Title = "Test_WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn", PageCount = 100, PublishDate = new DateTime(1990,02,02), GenreId = 1};
        context.Books.Add(book);
        context.SaveChanges();
        
        CreateBookCommand command = new CreateBookCommand(context, mapper);
        command.Model = new CreateBookModel () {Title = book.Title};

        // act - assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcuttur.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
    {
        // Arrange
        CreateBookCommand command = new CreateBookCommand(context, mapper);
        CreateBookModel model = new CreateBookModel(){
            Title = "Hobbit",
            PageCount = 0,
            PublishDate = DateTime.Now.Date.AddYears(-10),
            GenreId = 1};
        command.Model = model;

        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        var book = context.Books.SingleOrDefault(b=> b.Title == model.Title);
        
        book.Should().NotBeNull();
        book.PageCount.Should().Be(model.PageCount);
        book.PublishDate.Should().Be(model.PublishDate);
        book.GenreId.Should().Be(model.GenreId);

    }
}