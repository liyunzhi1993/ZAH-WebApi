using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using zah_db.Entity;

namespace zah_db.Repository.Impl
{
    public class ErpAccountRepositoryImpl : RepositoryBase, IErpAccountRepository
    {
        public ErpAccountRepositoryImpl(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        public async Task<ErpAccount> GetByID(int id)
        {
            return await QueryFirst<ErpAccount>("select * from erp_account where AccountId=@AccountId", new { AccountId = id });
        }

        public async Task<ErpAccount> GetByPwd(string accountName, string pwd)
        {
            return await QueryFirst<ErpAccount>("select * from erp_account where AccountName=@AccountName", new { AccountName = accountName });
        }
    }
}
