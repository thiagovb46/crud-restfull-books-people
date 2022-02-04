using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using RestWith.NET.Configurations;
using RestWith.NET.Data.VO;
using RestWith.NET.Repository;
using RestWith.NET.Services;

namespace RestWith.NET.Business.Implementations
{
    public class LoginBusinessImplementation : ILoginBusiness
    {
        private const  string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private TokenConfiguration _configuration;
        private IUserRepository _repository;
        private readonly ITokenService _tokenService;

        public LoginBusinessImplementation(TokenConfiguration configuration, IUserRepository repository, ITokenService tokenService)
        {
            _configuration = configuration;
            _repository = repository;
            _tokenService = tokenService;
        }

        public TokenVo ValidateCredentials(UserVo userCredentials)
        {
            var user  = _repository.ValidateCredentials(userCredentials);
            if(user == null)
                return null;
            var claims = new List<Claim> 
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim ( JwtRegisteredClaimNames.UniqueName, user.UserName)
            };
            var acessToken = _tokenService.GenerateAcessToken(claims);
            var refreshToken  = _tokenService.GenerateRefreshToken();
            
            user.RefreshToken = refreshToken;
            user.Refresh_Token_Expiry_Time = DateTime.Now.AddDays(_configuration.DaysToExpire);
            
            _repository.RefreshUserInfo(user);    
            
            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

            return   new TokenVo 
            (
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                acessToken,
                refreshToken
            );
        }

        public TokenVo ValidateCredentials(TokenVo token)
        {
            var acessToken = token.AcessToken;
            var refreshToken  = token.RefresToken;
            var principal =  _tokenService.GetPrincipalFromExpiredToken(acessToken);

            var username = principal.Identity.Name;

            var user = _repository.ValidateCredentials(username);

            if(user == null || 
                user.RefreshToken!= refreshToken 
                || user.Refresh_Token_Expiry_Time 
                <= DateTime.Now)
                return null;
            
            acessToken = _tokenService.GenerateAcessToken(principal.Claims);
            refreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = refreshToken;

            _repository.RefreshUserInfo(user);
            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

            return new TokenVo (
                true, 
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                acessToken,
                refreshToken
            );
        }

        public bool RevokeToken(string userName)
        {
            return _repository.RevokeToken(userName);
        }
    }
}