using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using zah_db.Entity;

namespace zah_db.Repository
{
    public interface IErpAccountRepository
    {
        /// <summary>
        /// 根据ID获取ERP账号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ErpAccount> GetByID(int id);

        /// <summary>
        /// 根据账号和密码获取ERP账号
        /// </summary>
        /// <param name="accountName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        Task<ErpAccount> GetByPwd(string accountName,string pwd);
    }
}
