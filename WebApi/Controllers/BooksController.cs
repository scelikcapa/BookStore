using Microsoft.AspNetCore.Mvc;
using WebApi.DbOperations;
using Microsoft.EntityFrameworkCore;
using WebApi.BookOperations.GetBooks;

namespace WebApi.Controllers;

    [ApiController]
    [Route("[Controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookStoreDbContext _context;

        public BooksController(BookStoreDbContext context)
        {
            _context=context;
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

        // [HttpGet]
        // public Book GetBooks([FromQuery] string id) 
        // {
        //     var book=BookList.SingleOrDefault(b=>b.Id==Convert.ToInt32(id));
        //     return book;
        // }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command=new CreateBookCommand(_context);
            
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
            var dbBook=_context.Books.SingleOrDefault(b=>b.Id==id);

            if (dbBook is null)
                return BadRequest();
            
            _context.Remove(dbBook);
            _context.SaveChanges();
            return Ok();
        }
    }
    