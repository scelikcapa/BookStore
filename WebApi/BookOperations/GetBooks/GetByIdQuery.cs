using Microsoft.AspNetCore.Mvc;
using WebApi.DbOperations;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;

namespace WebApi.BookOperations.GetBooks;


public class GetByIdQuery
{
    public int BookId { get; set; }
    private readonly BookStoreDbContext _context;

    public GetByIdQuery(BookStoreDbContext context)
    {
        _context = context;
    }

    public GetByIdQueryModel Handle()
    {
        var book=_context.Books.SingleOrDefault(b=>b.Id==BookId);

        if (book is null)
        {
            throw new InvalidOperationException("Kayıt bulunamadı.");
        }
        var vm= new GetByIdQueryModel
        {
            Id=BookId,
            Title = book.Title,
            Genre = ((GenreEnum)book.GenreId).ToString(),
            PublishDate= book.PublishDate.Date.ToString("dd/MM/yyyy"),
            PageCount = book.PageCount
        };
    
        return vm;
    }
}

    public class GetByIdQueryModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }