using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.GenreOperations.Command.CreateGenre;
using WebApi.Application.GenreOperations.Command.DeleteGenre;
using WebApi.Application.GenreOperations.Command.UpdateGenre;
using WebApi.Application.GenreOperations.Query.GetGenreDetail;
using WebApi.Application.GenreOperations.Query.GetGenres;
using WebApi.DbOperations;

namespace WebApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class GenresController : ControllerBase
{
    private readonly IBookStoreDbContext context;
    private readonly IMapper mapper;

    public GenresController(IBookStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetGenres()
    {
        var query=new GetGenresQuery(context,mapper);
        
        var obj = query.Handle();
        
        return Ok(obj);
    }
    
    [HttpGet("{id}")]
    public IActionResult GetGenreDetail(int id)
    {
        var query=new GetGenreDetailQuery(context,mapper);
        query.GenreId=id;

        var validator=new GetGenreDetailQueryValidator();
        validator.ValidateAndThrow(query);

        var obj=query.Handle();

        return Ok(obj);
    }

    [HttpPost]
    public IActionResult AddGenre([FromBody] CreateGenreModel newGenre)
    {
        var command=new CreateGenreCommand(context,mapper);
        command.Model=newGenre;

        var validator=new CreateGenreCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreModel updateGenre)
    {
        var command=new UpdateGenreCommand(context);
        command.GenreId=id;
        command.Model=updateGenre;

        var validator=new UpdateGenreCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteGenre(int id)
    {
        var command=new DeleteGenreCommand(context);
        command.GenreId=id;
        
        var validator=new DeleteGenreCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }
}