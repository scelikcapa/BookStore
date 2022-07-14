using AutoMapper;
using WebApi.Entities;
using WebApi.DbOperations;
using WebApi.TokenOperations.Models;
using WebApi.TokenOperations;

namespace WebApi.Application.TokenOperations.Commands.CreateToken;
public class CreateTokenCommand
{
    public CreateTokenModel Model { get; set; }
    private readonly IBookStoreDbContext context;
    private readonly IMapper mapper;
    private readonly IConfiguration configuration;

    public CreateTokenCommand(IBookStoreDbContext context, IMapper mapper, IConfiguration configuration)
    {
        this.context = context;
        this.mapper = mapper;
        this.configuration = configuration;
    }

    public Token Handle()
    {
        var user=context.Users.FirstOrDefault(u=>u.Email == Model.Email && u.Password == Model.Password);

        if (user is not null)
        {
            var handler = new TokenHandler(configuration);
            var token = handler.CreateAccessToken(user);

            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
            context.SaveChanges();

            return token;
        }
        else
            throw new InvalidOperationException("Kullanıcı Adı - Şifre Hatalı!");
    }
}

public class CreateTokenModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}