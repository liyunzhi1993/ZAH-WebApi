using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using zah_core.Dto;
using zah_core.Model;
using zah_db.Entity;
using zah_db.Repository;

namespace zah_core.Service.Impl
{
    public class ErpAccountServiceImpl : IErpAccountService
    {
        private readonly IErpAccountRepository _erpAccountRepository;
        private readonly AppSettings _appSettings;
        public ErpAccountServiceImpl(IErpAccountRepository erpAccountRepository,IOptions<AppSettings> appSettings)
        {
            _erpAccountRepository = erpAccountRepository;
            _appSettings = appSettings.Value;
        }
        public string Authenticate(LoginDto loginDto)
        {
            var erpAcccount=_erpAccountRepository.GetByPwd(loginDto.account, loginDto.pwd).Result;

            if (erpAcccount == null)
                return String.Empty;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, erpAcccount.AccountId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
