using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.Delete;

public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
{   
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public DeleteGenreCommandTests(CommonTestFixture testFixture)
    {
        mapper = testFixture.Mapper;
        context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenGenreIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var command = new DeleteGenreCommand(context);
        command.GenreId = -1;

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü bulunamadı.");
    }

    [Fact]
    public void WhenGivenGenreIdExistsInDb_Genre_ShouldBeDeleted()
    {
        // Arrange
        var genreInDb = new Genre{ Name = "WhenGivenGenreIdExistsInDb_Genre_ShouldBeDeleted"};
        context.Genres.Add(genreInDb);
        context.SaveChanges();

        var command = new DeleteGenreCommand(context);
        command.GenreId = genreInDb.Id;

        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        var genre = context.Genres.SingleOrDefault(b=> b.Id == genreInDb.Id);
        genre.Should().BeNull();
    }
}