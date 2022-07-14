using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.Create;

public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
{   
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public CreateAuthorCommandTests(CommonTestFixture testFixture)
    {
        mapper = testFixture.Mapper;
        context = testFixture.Context;
    }

    [Fact]
    public void WhenAlreadyExistAuthorNameIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        // arrange
        var author = new Author{ 
            Name = "WhenAlreadyExistAuthorNameIsGiven", 
            Surname = "InvalidOperationException_ShouldBeReturn", 
            BirthDate = new DateTime(1990,02,02)};
        context.Authors.Add(author);
        context.SaveChanges();
        
        CreateAuthorCommand command = new CreateAuthorCommand(context, mapper);
        command.Model = new CreateAuthorModel () {Name = author.Name, Surname = author.Surname};

        // act - assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar zaten mevcut.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
    {
        // Arrange
        CreateAuthorCommand command = new CreateAuthorCommand(context, mapper);
        CreateAuthorModel model = new CreateAuthorModel(){
            Name = "WhenValidInputsAreGiven",
            Surname = "Author_ShouldBeCreated",
            BirthDate = DateTime.Now.Date.AddYears(-18)};
        command.Model = model;

        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        var author = context.Authors.SingleOrDefault(b=> b.Name == model.Name);
        
        author.Should().NotBeNull();
        author.Name.Should().Be(model.Name);
        author.Surname.Should().Be(model.Surname);
        author.BirthDate.Should().Be(model.BirthDate);

    }
}