using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.Update;

public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
{   
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public UpdateAuthorCommandTests(CommonTestFixture testFixture)
    {
        mapper = testFixture.Mapper;
        context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenAuthorIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var command = new UpdateAuthorCommand(context, mapper);
        command.AuthorId = -1;

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellenecek Yazar bulunamadı.");
    }

    [Fact]
    public void WhenGivenFullNameIsSameWithAnotherAuthor_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var authorInDb = new Author{ 
                            Name = "Author1Name", 
                            Surname = "Author1Surname", 
                            BirthDate = new DateTime(1990,02,02)};
        var authorSameInDb = new Author{ 
                            Name = "Author2Name", 
                            Surname = "Author2Surname", 
                            BirthDate = new DateTime(1992,12,12)};
            
        context.Authors.Add(authorInDb);
        context.Authors.Add(authorSameInDb);
        context.SaveChanges();

        var command = new UpdateAuthorCommand(context, mapper);
        command.AuthorId = authorInDb.Id;
        command.Model = new UpdateAuthorModel{ 
                            Name = authorSameInDb.Name, 
                            Surname = authorSameInDb.Surname, 
                            BirthDate = new DateTime(1990,02,02)};

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aynı isimli bir Yazar zaten mevcuttur.");
    }

    [Fact]
    public void WhenGivenAuthorIdExistsInDb_Author_ShouldBeUpdated()
    {
        // Arrange
        var authorInDb = new Author{ 
                            Name = "WhenGivenAuthorIdExistsInDb", 
                            Surname = "Author_ShouldBeUpdated", 
                            BirthDate = new DateTime(1990,02,02)};
        var authorCompared = new Author{ 
                            Name = authorInDb.Name, 
                            Surname = authorInDb.Surname, 
                            BirthDate = authorInDb.BirthDate};
        context.Authors.Add(authorInDb);
        context.SaveChanges();

        var command = new UpdateAuthorCommand(context,mapper);
        command.AuthorId = authorInDb.Id;
        command.Model = new UpdateAuthorModel{ Name = "UpdatedName", Surname = "UpdatedSurname", BirthDate = new DateTime(1991,2,2)};

        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        var authorUpdated = context.Authors.SingleOrDefault(b=> b.Id == authorInDb.Id);
        authorUpdated.Should().NotBeNull();
        authorUpdated.Name.Should().NotBe(authorCompared.Name);
        authorUpdated.Surname.Should().NotBe(authorCompared.Surname);
        authorUpdated.BirthDate.Should().NotBe(authorCompared.BirthDate);

    }
}