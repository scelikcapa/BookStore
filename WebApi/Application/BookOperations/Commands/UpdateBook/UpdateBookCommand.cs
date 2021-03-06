using System.Globalization;
using WebApi.Entities;
using WebApi.DbOperations;
using AutoMapper;

namespace WebApi.Application.BookOperations.Commands.UpdateBook;
public class UpdateBookCommand
{
    public int BookId { get; set; }
    public UpdateBookModel Model { get; set; }
    private readonly IBookStoreDbContext _context;
    private readonly IMapper mapper;

    public UpdateBookCommand(IBookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        this.mapper = mapper;
    }

    public void Handle()
    {
        var book=_context.Books.SingleOrDefault(b=>b.Id==BookId);

        if (book is null)
        {
            throw new InvalidOperationException("Kayıt bulunamadı. Lütfen ilk önce kaydı oluşturun");
        }

        book.GenreId=Model.GenreId != default ? Model.GenreId : book.GenreId;
        book.PageCount=Model.PageCount != default ? Model.PageCount : book.PageCount;
        book.PublishDate=Model.PublishDate != default ? DateTime.ParseExact(Model.PublishDate, "yyyy-MM-dd", CultureInfo.InvariantCulture) : book.PublishDate;
        book.Title=Model.Title != default ? Model.Title : book.Title;

        _context.SaveChanges();
    }
}

public class UpdateBookModel
{
    public string Title { get; set; }
    public int GenreId { get; set; }
    public int PageCount { get; set; }
    public string PublishDate { get; set; }
}