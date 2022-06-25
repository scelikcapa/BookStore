using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries.GetAuthorDetail;

public class GetAuthorDetailQueryTests : IClassFixture<CommonTestFixture>
{   
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public GetAuthorDetailQueryTests(CommonTestFixture textfixture)
    {
        mapper = textfixture.Mapper;
        context = textfixture.Context;
    }

    [Fact]
    public void WhenGivenAuthorIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
    {
        // arrange
        var author = new Author{ 
                        Name = "WhenGivenAuthorIdDoesNotExistInDb", 
                        Surname = "InvalidOperationException_ShouldBeReturn", 
                        BirthDate = new DateTime(1990,02,02)};
        context.Authors.Add(author);
        context.SaveChanges();
        
        context.Authors.Remove(author);
        context.SaveChanges();

        GetAuthorDetailQuery command = new GetAuthorDetailQuery(context, mapper);
        command.AuthorId= author.Id;

        // act - assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar bulunamadı.");
    }

    [Fact]
    public void WhenGivenAuthorIdDoesExistInDb_Author_ShouldBeRetuned()
    {
        // Arrange
        var author = new Author{ 
                        Name = "WhenGivenAuthorIdDoesExistInDb", 
                        Surname = "Author_ShouldBeRetuned", 
                        BirthDate = new DateTime(1990,02,02)};
        context.Authors.Add(author);
        context.SaveChanges();

        GetAuthorDetailQuery command = new GetAuthorDetailQuery(context, mapper);
        command.AuthorId = author.Id;

        // Act
        var authorReturned = FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        authorReturned.Should().NotBeNull();
        authorReturned.Id.Should().Be(author.Id);
        authorReturned.Name.Should().Be(author.Name);
        authorReturned.Surname.Should().Be(author.Surname);
        authorReturned.BirthDate.Should().Be(author.BirthDate);
    }
}