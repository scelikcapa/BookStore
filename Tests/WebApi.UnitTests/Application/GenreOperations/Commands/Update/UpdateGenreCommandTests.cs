using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.Update;

public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
{   
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public UpdateGenreCommandTests(CommonTestFixture textfixture)
    {
        mapper = textfixture.Mapper;
        context = textfixture.Context;
    }

    [Fact]
    public void WhenGivenGenreIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var command = new UpdateGenreCommand(context);
        command.GenreId = -1;

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü bulunamadı.");
    }

    public void WhenGivenNameIsSameWithAnotherGenre_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var genreInDb = new Genre{ Name = "Genre1Name"};
        var genreSameInDb = new Genre{ Name = "Genre2Name"};
            
        context.Genres.Add(genreInDb);
        context.Genres.Add(genreSameInDb);
        context.SaveChanges();

        var command = new UpdateGenreCommand(context);
        command.GenreId = genreInDb.Id;
        command.Model = new UpdateGenreModel{ Name = genreSameInDb.Name };

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aynı isimli bir Kitap Türü zaten mevcut.");
    }


    [Fact]
    public void WhenGivenGenreIdExistsInDb_Genre_ShouldBeUpdated()
    {
        // Arrange
        var genreInDb = new Genre{ Name = "WhenGivenGenreIdExistsInDb_Genre_ShouldBeUpdated"};
        var genreCompared = new Genre{ Name = genreInDb.Name};
        context.Genres.Add(genreInDb);
        context.SaveChanges();

        var command = new UpdateGenreCommand(context);
        command.GenreId = genreInDb.Id;
        command.Model = new UpdateGenreModel{ Name = "UpdatedName", IsActive = false};

        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        var genreUpdated = context.Genres.SingleOrDefault(b=> b.Id == genreInDb.Id);
        genreUpdated.Should().NotBeNull();
        genreUpdated.Name.Should().NotBe(genreCompared.Name);
        genreUpdated.IsActive.Should().NotBe(genreCompared.IsActive);
    }
}