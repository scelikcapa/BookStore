using Microsoft.AspNetCore.Mvc;
using WebApi.DbOperations;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using AutoMapper;
using FluentValidation;
using WebApi.BookOperations.UpdateBook;

namespace WebApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class BooksController : ControllerBase
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;    

    public BooksController(BookStoreDbContext context, IMapper mapper)
    {
        _context=context;
        _mapper = mapper;
    } 

    [HttpGet]
    public IActionResult GetBooks() 
    {
        // var bookList=_context.Books.OrderBy(b=>b.Id).ToList<Book>();
        // return bookList;

        GetBooksQuery query =new GetBooksQuery(_context,_mapper);
        var result = query.Handle();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById (int id)
    {
        GetByIdQuery query=new GetByIdQuery(_context,_mapper);
        query.BookId=id;
        GetByIdQueryModel result;
        GetByIdQueryValidator validator =new GetByIdQueryValidator();

        try
        {
            validator.ValidateAndThrow(query);
            result=query.Handle();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
        return Ok(result);
    }
    

    [HttpPost]
    public IActionResult AddBook([FromBody] CreateBookModel newBook)
    {
        CreateBookCommand command=new CreateBookCommand(_context, _mapper);
        
        try
        {
            command.Model=newBook;
            CreateBookCommandValidator validator=new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            // var result = validator.Validate(command);
            // if (!result.IsValid)
            // {
            //     foreach (var item in result.Errors)
            //         Console.WriteLine("Property = "+item.PropertyName + " - Error Message = " + item.ErrorMessage);
            // }
            // else
            //         command.Handle();

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
    {
        UpdateBookCommand command=new UpdateBookCommand(_context);
        command.Model=updatedBook;
        command.Model.Id=id;
        UpdateBookCommandValidator validator = new UpdateBookCommandValidator();

        try
        {    
            validator.ValidateAndThrow(command);
            command.Handle();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        DeleteBookCommand command=new DeleteBookCommand(_context);
        command.BookId=id;
        DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
        try
        {    
            validator.ValidateAndThrow(command);
            command.Handle();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok();
    }
}
