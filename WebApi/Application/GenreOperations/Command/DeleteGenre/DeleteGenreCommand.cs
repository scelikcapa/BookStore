using WebApi.DbOperations;

namespace WebApi.Application.GenreOperations.Command.DeleteGenre;

public class DeleteGenreCommand
{
    private readonly BookStoreDbContext context;

    public int GenreId { get; set; }

    public DeleteGenreCommand(BookStoreDbContext context)
    {
        this.context = context;
    }

    public void Handle()
    {
        var genre = context.Genres.SingleOrDefault(g=>g.Id==GenreId);

        if(genre is null)
            throw new InvalidOperationException("Kitap Türü bulunamadı.");
        
        if(genre.IsActive==false)
            throw new InvalidOperationException("Kitap Türü zaten silinmiş.");

        genre.IsActive=false;
        context.SaveChanges();
    }


}