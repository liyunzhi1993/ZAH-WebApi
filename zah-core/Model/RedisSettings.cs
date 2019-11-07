using System;
using System.Collections.Generic;
using System.Text;

namespace zah_core.Model
{
    public class RedisSettings
    {
        public string Server { get; set; }
        public string Port { get; set; }
        public string Auth { get; set; }
        public int Db { get; set; }
    }
}
