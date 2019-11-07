using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zah_core.Dto;
using zah_core.Model;
using zah_core.Service;
using zah_db.Entity;

namespace zah_erp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : BaseController
    {
        private readonly IErpAccountService _erpAccountService;

        public AccountController(IErpAccountService erpAccountService)
        {
            _erpAccountService = erpAccountService;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public ReturnMsg<string> Login(LoginDto loginDto)
        {
            return ApiReturn<string>(_erpAccountService.Authenticate(loginDto));
        }
    }
}
