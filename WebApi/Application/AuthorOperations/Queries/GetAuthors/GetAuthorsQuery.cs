using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthors;

public class GetAuthorsQuery 
{
    private readonly IBookStoreDbContext context;
    private readonly IMapper mapper;

    public GetAuthorsQuery(IBookStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public List<GetAuthorViewModel> Handle()
    {
        var authors=context.Authors.OrderBy(a=>a.Id);
        
        var model = mapper.Map<List<GetAuthorViewModel>>(authors);
        
        return model;
    }
}

public class GetAuthorViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime BirthDate { get; set; }
}