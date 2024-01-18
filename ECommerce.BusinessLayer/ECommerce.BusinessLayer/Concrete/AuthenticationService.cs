
using ECommerce.BusinessLayer.Abstract;
using ECommerce.DataAccessLayer.Abstract;
using ECommerce.DataAccessLayer.UnitOfWork;
using ECommerce.EntityLayer.Concrete;
using ECommerce.EntityLayer.DTOS;
using ECommerce.SharedLibrary.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ECommerce.BusinessLayer.Concrete
{
    public class AuthenticationService(IOptions<List<Client>> optionsClient, ITokenService tokenService, UserManager<User> userManager, IUnitOfWork unitOfWork, IGenericDal<UserRefreshToken> userRefreshTokenService) : IAuthenticationService
    {
        private readonly List<Client> _clients = optionsClient.Value;
        private readonly ITokenService _tokenService = tokenService;
        private readonly UserManager<User> _userManager = userManager;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IGenericDal<UserRefreshToken> _userRefreshTokenService = userRefreshTokenService;

        public async Task<CustomResponseDto<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            if (loginDto == null)
            {
                throw new ArgumentNullException(nameof(loginDto));
            }
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                return CustomResponseDto<TokenDto>.Fail(400, "Email or Password is wrong", true);
            }

            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return CustomResponseDto<TokenDto>.Fail(400, "Email or Password is wrong", true);
            }

            var token = _tokenService.CreateToken(user);
            var userRefreshToken = await _userRefreshTokenService.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();

            if (userRefreshToken == null)
            {
                await _userRefreshTokenService.AddAsync(new UserRefreshToken
                {
                    UserId = user.Id,
                    Code = token.RefleshToken,
                    Expiration = token.RefleshTokenExpiration

                });
            }
            else
            {
                userRefreshToken.Code = token.RefleshToken;
                userRefreshToken.Expiration = token.RefleshTokenExpiration;
            }

            await _unitOfWork.CommitAsync();
            return CustomResponseDto<TokenDto>.Success(200, token);
        }

        public CustomResponseDto<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            var client = _clients.SingleOrDefault(x => x.Id == clientLoginDto.ClientId && x.Secret == clientLoginDto.ClientSecret);
            if (client == null)
            {

                return CustomResponseDto<ClientTokenDto>.Fail(404, "ClientId or ClientSecret not found", true);
            }
            var token = _tokenService.CreateTokenByClient(client);

            return CustomResponseDto<ClientTokenDto>.Success(200, token);
        }

        public async Task<CustomResponseDto<TokenDto>> CreateTokenByRefreshToken(string refreshToken)
        {
            var existRefreshToken = await _userRefreshTokenService.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();

            if (existRefreshToken == null)
            {

                return CustomResponseDto<TokenDto>.Fail(404, "Refresh token not found", true);
            }
            var user = await _userManager.FindByIdAsync(existRefreshToken.UserId);
            if (user == null)
            {
                return CustomResponseDto<TokenDto>.Fail(404, "UserId  not found", true);


            }
            var tokenDto = _tokenService.CreateToken(user);
            existRefreshToken.Code = tokenDto.RefleshToken;
            existRefreshToken.Expiration = tokenDto.RefleshTokenExpiration;

            await _unitOfWork.CommitAsync();

            return CustomResponseDto<TokenDto>.Success(200, tokenDto);
        }

        public async Task<CustomResponseDto<NoContentDto>> RevokeRefreshTokenAsync(string refreshToken)
        {
            var existRefreshToken = await _userRefreshTokenService.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();

            if (existRefreshToken == null)
            {
                return CustomResponseDto<NoContentDto>.Fail(404, "Refresh token not found", true);

            }
            _userRefreshTokenService.Remove(existRefreshToken);

            await _unitOfWork.CommitAsync();

            return CustomResponseDto<NoContentDto>.Success(200, true);
        }
    }
}
