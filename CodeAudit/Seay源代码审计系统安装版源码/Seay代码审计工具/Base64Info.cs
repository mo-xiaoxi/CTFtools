using System;
using System.Collections.Generic;
using System.Text;

namespace Seay正则URL解码匹配测试工具
{
    class Base64Info
    {
        public static string func_base64encode(string str)
        {
            str = Convert.ToBase64String(Encoding.GetEncoding("GBK").GetBytes(str));
            return str;
        }

        public static string func_base64decode(string str)
        {
            str = Encoding.GetEncoding("GBK").GetString(Convert.FromBase64String(str));
            return str;
        }
    }
}
