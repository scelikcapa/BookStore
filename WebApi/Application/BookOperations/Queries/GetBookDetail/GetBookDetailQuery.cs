using Microsoft.AspNetCore.Mvc;
using WebApi.DbOperations;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using AutoMapper;

namespace WebApi.Application.BookOperations.Queries.GetBookDetail;

public class GetBookDetailQuery
{
    public int BookId { get; set; }
    private readonly IBookStoreDbContext _context;
    private readonly IMapper mapper;

    public GetBookDetailQuery(IBookStoreDbContext context,IMapper mapper)
    {
        _context = context;
        this.mapper = mapper;
    }

    public GetBookDetailViewModel Handle()
    {
        var book=_context.Books.Include(b=>b.Genre).SingleOrDefault(b=>b.Id==BookId);

        if (book is null)
        {
            throw new InvalidOperationException("Kayıt bulunamadı.");
        }
    
        var vm=mapper.Map<GetBookDetailViewModel>(book);

        return vm;
    }
}

public class GetBookDetailViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int PageCount { get; set; }
    public string PublishDate { get; set; }
    public string Genre { get; set; }
}