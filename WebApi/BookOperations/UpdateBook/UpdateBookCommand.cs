using System.Globalization;
using WebApi;
using WebApi.DbOperations;

public class UpdateBookCommand
{
    public UpdateBookModel Model { get; set; }
    private readonly BookStoreDbContext _context;

    public UpdateBookCommand(BookStoreDbContext context)
    {
        _context = context;
    }

    public void Handle()
    {
        var book=_context.Books.SingleOrDefault(b=>b.Id==Model.Id);

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
    public int Id { get; set; }
    public string Title { get; set; }
    public int GenreId { get; set; }
    public int PageCount { get; set; }
    public string PublishDate { get; set; }
}