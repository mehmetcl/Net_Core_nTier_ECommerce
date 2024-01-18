using ECommerce.BusinessLayer.Abstract;
using ECommerce.EntityLayer.Concrete;
using ECommerce.EntityLayer.DTOS;
using ECommerce.SharedLibrary.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BusinessLayer.Concrete
{
    public class TokenService(UserManager<User> userService, IOptions<CustomTokenOption> options) : ITokenService
    {

        private readonly UserManager<User> _userService = userService;
        private readonly CustomTokenOption _tokenoptions = options.Value;

        //Üyelik sistemi gerektiren Token oluşturmak Payload için
        private IEnumerable<Claim> GetClaims(User user,List<String> audiences) {

            var userList = new List<Claim>
        {
             new Claim(ClaimTypes.NameIdentifier,user.Id),
             new Claim(JwtRegisteredClaimNames.Email,user.Email),
             new Claim(ClaimTypes.Name,user.UserName),
              new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
        };

            userList.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));

            return userList;
        }
        //Üyelik sistemi gerektirmeyen Token oluşturmak Payload için
        private IEnumerable<Claim> GetClaimByClient(Client client)
        {
            var claims = new List<Claim>();
            claims.AddRange(client.Audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud,x)));
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
            new Claim(JwtRegisteredClaimNames.Sub, client.Id.ToString());

            return claims;

        }
        private string CreateRefreshToken()
        {

            var numberBtye = new byte[32];
            using var random= RandomNumberGenerator.Create();
            random.GetBytes(numberBtye);    

            return Convert.ToBase64String(numberBtye);
        }

        public TokenDto CreateToken(User user)
        {
            var accessTokenExpression = DateTime.Now.AddMinutes(_tokenoptions.AccessTokenExpiration);

            var refreshTokenExpression = DateTime.Now.AddMinutes(_tokenoptions.RefreshTokenExpiration);

            var securityKey = SignService.GetSymmetricSecurityKey(_tokenoptions.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer:_tokenoptions.Issuer,
                expires:accessTokenExpression,
                notBefore:DateTime.Now,
                claims:GetClaims(user,_tokenoptions.Audience),
                signingCredentials:signingCredentials
                );
            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwtSecurityToken);

            var tokenDto = new TokenDto
            {
                AccessToken = token,
                RefleshToken = CreateRefreshToken(),
                AccessTokenExpiration = accessTokenExpression,
                RefleshTokenExpiration = refreshTokenExpression,
            };

            return tokenDto;
        }

        public ClientTokenDto CreateTokenByClient(Client client)
        {

            var accessTokenExpression = DateTime.Now.AddMinutes(_tokenoptions.AccessTokenExpiration);

         

            var securityKey = SignService.GetSymmetricSecurityKey(_tokenoptions.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenoptions.Issuer,
                expires: accessTokenExpression,
                notBefore: DateTime.Now,
                claims: GetClaimByClient(client),
                signingCredentials: signingCredentials
                );
            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwtSecurityToken);

            var tokenDto = new ClientTokenDto
            {
                AccessToken = token,
                AccessTokenExpiration = accessTokenExpression,
             
            };

            return tokenDto;
        }
    }
}
