using AutoMapper;
using WebApi;
using WebApi.DbOperations;

namespace WebApi.BookOperations.CreateBook;
public class CreateBookCommand
{
    public CreateBookModel Model { get; set; }
    private readonly BookStoreDbContext _context;
    private readonly IMapper mapper;

    public CreateBookCommand(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        this.mapper = mapper;
    }

    public void Handle()
    {
        var book=_context.Books.SingleOrDefault(b=>b.Id==Model.Id);

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
    public int Id { get; set; }
    public string Title { get; set; }
    public int GenreId { get; set; }
    public int PageCount { get; set; }
    public DateTime PublishDate { get; set; }
}