using System;
using System.Collections.Generic;
using System.Text;

namespace zah_core.Model
{
    public class ReturnMsg<T>
    {
        /// <summary>
        /// 请求是否成功
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// 返回状态码 
        /// </summary>
        public int Code { get; set; } = 200;

        /// <summary>
        /// 状态码的描述信息
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// 服务器返回时间
        /// </summary>
        public string RespTime { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        /// <summary>
        /// 具体业务数据
        /// </summary>
        public T Data { get; set; }
    }
}
