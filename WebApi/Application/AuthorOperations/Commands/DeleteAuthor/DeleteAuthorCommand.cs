using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor;

public class DeleteAuthorCommand 
{
    private readonly BookStoreDbContext context;
    public int AuthorId { get; set; }

    public DeleteAuthorCommand(BookStoreDbContext context)
    {
        this.context = context;
    }

    public void Handle()
    {
        var author=context.Authors.SingleOrDefault(a=>a.Id==AuthorId);

        if(author is null)
            throw new InvalidOperationException("Silinecek Yazar bulunamadı.");

        context.Authors.Remove(author);
        context.SaveChanges();
    }
}