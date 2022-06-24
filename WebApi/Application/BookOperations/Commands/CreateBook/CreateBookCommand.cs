using AutoMapper;
using WebApi.Entities;
using WebApi.DbOperations;

namespace WebApi.BookOperations.CreateBook;
public class CreateBookCommand
{
    public CreateBookModel Model { get; set; }
    private readonly IBookStoreDbContext _context;
    private readonly IMapper mapper;

    public CreateBookCommand(IBookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        this.mapper = mapper;
    }

    public void Handle()
    {
        var book=_context.Books.SingleOrDefault(b=>b.Title==Model.Title);

        if (book is not null)
        {
            throw new InvalidOperationException("Kitap zaten mevcut.");
        }

        book=mapper.Map<Book>(Model);

        _context.Books.Add(book);
        _context.SaveChanges();
    }
}

public class CreateBookModel
{
    public string Title { get; set; }
    public int GenreId { get; set; }
    public int PageCount { get; set; }
    public DateTime PublishDate { get; set; }
}