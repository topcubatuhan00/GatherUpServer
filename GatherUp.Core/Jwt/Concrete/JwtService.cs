using GatherUp.Core.Jwt.Abstract;
using GatherUp.Domain.Entities;
using GatherUp.Domain.Models.HelperModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GatherUp.Core.Jwt.Concrete;

public class JwtService : IJwtService
{
    #region Fields
    private readonly IConfiguration _configuration;
    #endregion

    #region Ctor
    public JwtService
    (
        IConfiguration configuration
    )
    {
        _configuration = configuration;
    }
    #endregion

    #region Methods
    public TokenResponseModel CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };
        var signingKey = _configuration["JwtConfig:SigningKey"];
        var audience = _configuration["JwtConfig:Audience"];
        var issuer = _configuration["JwtConfig:Issuer"];
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
        var securityCreds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddDays(15),
                notBefore: DateTime.Now,
                signingCredentials: securityCreds
            );
        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        return new TokenResponseModel { Token = token, Expiration = jwtSecurityToken.ValidTo };
    }
    #endregion
}
