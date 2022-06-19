using Microsoft.AspNetCore.Mvc;
using WebApi.DbOperations;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using AutoMapper;

namespace WebApi.BookOperations.GetBooks;

public class GetByIdQuery
{
    public int BookId { get; set; }
    private readonly BookStoreDbContext _context;
    private readonly IMapper mapper;

    public GetByIdQuery(BookStoreDbContext context,IMapper mapper)
    {
        _context = context;
        this.mapper = mapper;
    }

    public GetByIdQueryModel Handle()
    {
        var book=_context.Books.SingleOrDefault(b=>b.Id==BookId);

        if (book is null)
        {
            throw new InvalidOperationException("Kayıt bulunamadı.");
        }
    
        var vm=mapper.Map<GetByIdQueryModel>(book);

        return vm;
    }
}

public class GetByIdQueryModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int PageCount { get; set; }
    public string PublishDate { get; set; }
    public string Genre { get; set; }
}