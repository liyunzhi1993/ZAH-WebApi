using System;
using System.Collections.Generic;
using System.Text;

namespace zah_db.Entity
{
    public class ErpAccount
    {
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public string AccountPwd { get; set; }
        public string Token { get; set; }
    }
}
