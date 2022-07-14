using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenreDetail;

public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
{   
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public GetGenreDetailQueryTests(CommonTestFixture testFixture)
    {
        mapper = testFixture.Mapper;
        context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenGenreIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
    {
        // arrange
        var genre = new Genre{ Name = "WhenGivenGenreIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn"};
        context.Genres.Add(genre);
        context.SaveChanges();
        
        context.Genres.Remove(genre);
        context.SaveChanges();

        GetGenreDetailQuery command = new GetGenreDetailQuery(context, mapper);
        command.GenreId= genre.Id;

        // act - assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü bulunamadı");
    }

    [Fact]
    public void WhenGivenGenreIdDoesExistInDb_Genre_ShouldBeReturned()
    {
        // Arrange
        var genre = new Genre{ Name = "WhenGivenGenreIdDoesExistInDb_Genre_ShouldBeRetuned"};
        context.Genres.Add(genre);
        context.SaveChanges();

        GetGenreDetailQuery command = new GetGenreDetailQuery(context, mapper);
        command.GenreId = genre.Id;

        // Act
        var genreReturned = FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        genreReturned.Should().NotBeNull();
        genreReturned.Id.Should().Be(genre.Id);
        genreReturned.Name.Should().Be(genre.Name);
    }
}