using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.DbOperations;

namespace WebApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class AuthorsController : ControllerBase
{
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public AuthorsController(BookStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    
}