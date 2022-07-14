using WebApi.DbOperations;
using WebApi.TokenOperations.Models;
using WebApi.TokenOperations;

namespace WebApi.Application.TokenOperations.Commands.RefreshToken;
public class RefreshTokenCommand
{
    public string RefreshToken { get; set; }
    private readonly IBookStoreDbContext context;
    private readonly IConfiguration configuration;

    public RefreshTokenCommand(IBookStoreDbContext context, IConfiguration configuration)
    {
        this.context = context;
        this.configuration = configuration;
    }

    public Token Handle()
    {
        var user=context.Users.FirstOrDefault(u=>u.RefreshToken == RefreshToken && u.RefreshTokenExpireDate > DateTime.Now);

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
            throw new InvalidOperationException("Valid bir Refresh Token bulunamadı");
    }
}