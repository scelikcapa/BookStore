using Microsoft.AspNetCore.Mvc;
using WebApi.DbOperations;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using AutoMapper;

namespace WebApi.Controllers;

    [ApiController]
    [Route("[Controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper mapper;    

    public BooksController(BookStoreDbContext context, IMapper mapper)
    {
        _context=context;
        this.mapper = mapper;
    } 

      

        [HttpGet]
        public IActionResult GetBooks() 
        {
            // var bookList=_context.Books.OrderBy(b=>b.Id).ToList<Book>();
            // return bookList;

            GetBooksQuery query =new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById (int id)
        {
            GetByIdQuery query=new GetByIdQuery(_context);
            query.BookId=id;
            var result=new GetByIdQueryModel();

            try
            {
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
            CreateBookCommand command=new CreateBookCommand(_context, mapper);
            
            try
            {
                command.Model=newBook;
                command.Handle();
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
            
            try
            {    
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
            
            try
            {    
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
    