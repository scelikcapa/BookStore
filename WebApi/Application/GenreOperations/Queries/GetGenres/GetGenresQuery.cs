using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.GenreOperations.Queries.GetGenres;
public class GetGenresQuery
{
    private readonly IBookStoreDbContext context;
    private readonly IMapper mapper;

    public GetGenresQuery(IBookStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public List<GetGenresViewModel> Handle()
    {
        var genres = context.Genres.Where(g => g.IsActive == true).OrderBy(g => g.Id);
        List<GetGenresViewModel> returnObj = mapper.Map<List<GetGenresViewModel>>(genres);
        return returnObj;
    }
}

public class GetGenresViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}