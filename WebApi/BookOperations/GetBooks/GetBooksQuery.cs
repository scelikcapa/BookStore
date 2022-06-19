using Microsoft.AspNetCore.Mvc;
using WebApi.DbOperations;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using AutoMapper;

namespace WebApi.BookOperations.GetBooks;

public class GetBooksQuery
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper mapper;

    public GetBooksQuery(BookStoreDbContext context,IMapper mapper)
    {
        _context = context;
        this.mapper = mapper;
    }

    public List<BooksViewModel> Handle()
    {
        var bookList=_context.Books.OrderBy(b=>b.Id).ToList();

        List<BooksViewModel> vm= mapper.Map<List<BooksViewModel>>(bookList);

        return vm;
    }
    
}

public class BooksViewModel
{
    public string Title { get; set; }
    public int PageCount { get; set; }
    public string PublishDate { get; set; }
    public string Genre { get; set; }
    
       
}