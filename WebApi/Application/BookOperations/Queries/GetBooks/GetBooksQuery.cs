using Microsoft.AspNetCore.Mvc;
using WebApi.DbOperations;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using AutoMapper;

namespace WebApi.Application.BookOperations.Queries.GetBooks;

public class GetBooksQuery
{
    private readonly IBookStoreDbContext _context;
    private readonly IMapper mapper;

    public GetBooksQuery(IBookStoreDbContext context,IMapper mapper)
    {
        _context = context;
        this.mapper = mapper;
    }

    public List<GetBooksViewModel> Handle()
    {
        var bookList=_context.Books.Include(b=>b.Genre).OrderBy(b=>b.Id).ToList();

        List<GetBooksViewModel> vm= mapper.Map<List<GetBooksViewModel>>(bookList);

        return vm;
    }
    
}

public class GetBooksViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int PageCount { get; set; }
    public string PublishDate { get; set; }
    public string Genre { get; set; }
    
       
}