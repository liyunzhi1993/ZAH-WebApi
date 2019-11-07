using System;
using System.Collections.Generic;
using System.Text;
using zah_core.Dto;
using zah_db.Entity;

namespace zah_core.Service
{
    public interface IErpAccountService
    {
        string Authenticate(LoginDto loginDto);
    }
}
