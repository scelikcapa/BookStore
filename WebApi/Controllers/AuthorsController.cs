using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.DbOperations;

namespace WebApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class AuthorsController : ControllerBase
{
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public AuthorsController(BookStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAuthors()
    {
        var query = new GetAuthorsQuery(context,mapper);
        var authorList = query.Handle();

        return Ok(authorList);
    }

    [HttpGet("{id}")]
    public IActionResult GetAuthorDetail(int id)
    {
        var query = new GetAuthorDetailQuery(context,mapper);
        query.AuthorId = id;

        var validator=new GetAuthorDetailQueryValidator();
        validator.ValidateAndThrow(query);
        
        var author = query.Handle();

        return Ok(author);
    }
    
    [HttpPost]
    public IActionResult CreateAuthor([FromBody] CreateAuthorModel newAuthor)
    {
        var command = new CreateAuthorCommand(context,mapper);
        command.Model = newAuthor;

        var validator=new CreateAuthorCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel updatedAuthor)
    {
        var command = new UpdateAuthorCommand(context,mapper);
        command.AuthorId = id;
        command.Model = updatedAuthor;

        var validator = new UpdateAuthorCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAuthor(int id)
    {
        var command = new DeleteAuthorCommand(context);
        command.AuthorId = id;

        var validator = new DeleteAuthorCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }
    
}