using WebApi;
using WebApi.DbOperations;

public class CreateBookCommand
{
    public CreateBookModel Model { get; set; }
    private readonly BookStoreDbContext _context;

    public CreateBookCommand(BookStoreDbContext context)
    {
        _context = context;
    }

    public void Handle()
    {
        var book=_context.Books.SingleOrDefault(b=>b.Id==Model.Id);

        if (book is null)
        {
            throw new InvalidOperationException("Kitap zaten mevcut.");
        }

        book=new Book
        {
            Title=Model.Title,
            PublishDate=Model.PublishDate,
            PageCount=Model.PageCount,
            GenreId=Model.GenreId
        };

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