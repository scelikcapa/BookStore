using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.TokenOperations.Commands.CreateToken;
using WebApi.Application.TokenOperations.Commands.RefreshToken;
using WebApi.Application.UserOperations.Commands.CreateUser;
using WebApi.DbOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class UsersController : ControllerBase 
{
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;
    private readonly IConfiguration configuration;

    public UsersController(BookStoreDbContext context, IMapper mapper, IConfiguration configuration)
       {
        this.context = context;
        this.mapper = mapper;
        this.configuration = configuration;
    } 

    [HttpPost]
    public IActionResult CreateUser([FromBody] CreateUserModel newUser)
    {
        var command = new CreateUserCommand(context, mapper);
        command.Model = newUser;
        command.Handle();

        return Ok();
    }

    [HttpPost("connect/token")]
    public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login)
    {
        var command = new CreateTokenCommand(context, mapper, configuration);
        command.Model = login;
        var token = command.Handle();

        return Ok(token);
    }

    [HttpGet("refreshToken")]
    public ActionResult<Token> RefreshToken([FromQuery] string refreshToken)
    {
        var command = new RefreshTokenCommand(context, configuration);
        command.RefreshToken = refreshToken;
        var newToken = command.Handle();

        return Ok(newToken);
    }

}
