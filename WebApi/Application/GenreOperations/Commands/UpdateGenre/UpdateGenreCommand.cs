using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre;

public class UpdateGenreCommand 
{
    private readonly IBookStoreDbContext context;

    public int GenreId { get; set; }
    public UpdateGenreModel Model { get; set; }

    public UpdateGenreCommand(IBookStoreDbContext context)
    {
        this.context = context;
    }

    public void Handle ()
    {
        var genre=context.Genres.SingleOrDefault(g=>g.Id==GenreId);

        if(genre is null)
            throw new InvalidOperationException("Kitap Türü bulunamadı.");

        if(context.Genres.Any(g=>g.Name.ToLower()==Model.Name.ToLower() && g.Id!=GenreId))
            throw new InvalidOperationException("Aynı isimli bir Kitap Türü zaten mevcut.");
        
        genre.Name = String.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name : Model.Name;
        genre.IsActive = Model.IsActive;
        context.SaveChanges();
    }
}

public class UpdateGenreModel
{
    public string Name { get; set; }
    public bool IsActive { get; set; } = true;
}