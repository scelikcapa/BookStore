using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Command.CreateGenre;

public class CreateGenreCommand
{
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;
    public CreateGenreModel Model { get; set; }

    public CreateGenreCommand(BookStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public void Handle()
    {
        var genre = context.Genres.SingleOrDefault(g=>g.Name==Model.Name);
        
        if(genre is not null)
            throw new InvalidOperationException("Kitap Türü zaten mevcut.");

        genre = mapper.Map<Genre>(Model);

        context.Genres.Add(genre);
        context.SaveChanges();
    }
}

public class CreateGenreModel
{
    public string Name { get; set; }
}