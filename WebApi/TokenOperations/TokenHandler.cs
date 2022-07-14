using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.Entities;
using WebApi.TokenOperations.Models;

namespace WebApi.TokenOperations;

public class TokenHandler 
{
    private readonly IConfiguration configuration;

    public TokenHandler(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public Token CreateAccessToken(User user)
    {
        var tokenModel = new Token();
        var key =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        tokenModel.Expiration = DateTime.Now.AddMinutes(15);

        JwtSecurityToken securityToken = new JwtSecurityToken(
            issuer: configuration["Token:Issuer"],
            audience: configuration["Token:Audience"],
            expires: tokenModel.Expiration,
            notBefore: DateTime.Now,
            signingCredentials: credentials
        );

        var tokenHandler = new JwtSecurityTokenHandler();

        // Token is creating...
        tokenModel.AccessToken = tokenHandler.WriteToken(securityToken);
        tokenModel.RefreshToken = CreateRefreshToken();

        return tokenModel;
    }

    private string CreateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }
}