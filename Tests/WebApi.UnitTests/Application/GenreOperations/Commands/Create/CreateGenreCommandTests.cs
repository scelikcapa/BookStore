using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.Create;

public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
{   
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public CreateGenreCommandTests(CommonTestFixture textfixture)
    {
        mapper = textfixture.Mapper;
        context = textfixture.Context;
    }

    [Fact]
    public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        // arrange
        var genre = new Genre{ Name = "Test_WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn"};
        context.Genres.Add(genre);
        context.SaveChanges();
        
        CreateGenreCommand command = new CreateGenreCommand(context, mapper);
        command.Model = new CreateGenreModel () {Name = genre.Name};

        // act - assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü zaten mevcut.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
    {
        // Arrange
        CreateGenreCommand command = new CreateGenreCommand(context, mapper);
        var model = new CreateGenreModel(){ Name = "WhenValidInputsAreGiven_Genre_ShouldBeCreated",};
        command.Model = model;

        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        var genre = context.Genres.SingleOrDefault(b=> b.Name == model.Name);
        
        genre.Should().NotBeNull();
        genre.Name.Should().Be(model.Name);
        genre.IsActive.Should().Be(true);
    }
}