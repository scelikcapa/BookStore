using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;

public class GetAuthorDetailQuery 
{
    private readonly IBookStoreDbContext context;
    private readonly IMapper mapper;
    public int AuthorId { get; set; }

    public GetAuthorDetailQuery(IBookStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public GetAuthorDetailViewModel Handle()
    {
        var author = context.Authors.SingleOrDefault(a=>a.Id==AuthorId);

        if(author is null)
            throw new InvalidOperationException("Yazar bulunamadı.");

        var model = mapper.Map<GetAuthorDetailViewModel>(author);

        return model;
    }
}

public class GetAuthorDetailViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime BirthDate { get; set; }
}