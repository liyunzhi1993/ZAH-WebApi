using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zah_core.Enum;
using zah_core.Model;

namespace zah_erp.Controllers
{
    public class BaseController:ControllerBase
    {
        public BaseController()
        {
           
        }
        protected ReturnMsg<T> ApiReturn<T>(T data, bool success=true, string msg="")
        {
            var returnMsg = new ReturnMsg<T>();
            returnMsg.Code = (int)ResponseCode.Success;
            returnMsg.Data = data;
            returnMsg.Success = success;
            returnMsg.Message = msg;
            return returnMsg;
        }
    }
}
