using System;
using System.Collections.Generic;
using System.Text;

namespace Seay正则URL解码匹配测试工具
{
    class UrlInfo
    {
        public static string func_UrlEncode(string str)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = System.Text.Encoding.UTF8.GetBytes(str);
            for (int i = 0; i < byStr.Length; i++)
            {
                sb.Append(@"%" + Convert.ToString(byStr[i], 16));
            }

            return sb.ToString();
        }

        public static string func_UrlDencode(string str)
        {
            //将URL转为小写
            string lowerUrl = str.ToLower();

            //判断URL中是否包含%，如果不包含%就不需要解码

            if (lowerUrl.IndexOf('%') != -1)
            {

                //判断URL中是否包含%E，如果不包含直接用GB2312解码

                if (lowerUrl.IndexOf("%e") != -1)
                {

                    //以UTF-8对URL进行解码

                    string stringUrl = System.Web.HttpUtility.UrlDecode(str, Encoding.GetEncoding("UTF-8"));

                    //判断解码后的字符串是否为UTF-8编码

                    if (CheckIsUTF8(stringUrl))
                    {
                        return stringUrl;
                    }
                    else
                    {
                        return System.Web.HttpUtility.UrlDecode(str, Encoding.GetEncoding("GB2312"));
                    }
                }

                return System.Web.HttpUtility.UrlDecode(str, Encoding.GetEncoding("GB2312"));

            }

            return str;

        }

        private static bool CheckIsUTF8(string url)
        {
            //将URL转成UTF-8字节数组

            byte[] bs = Encoding.GetEncoding("UTF-8").GetBytes(url);

            for (int i = 0; i < bs.Length; i++)
            {
                //查找字节239 191 189，如果找到退出循环，标记为GB2312

                if (bs[i++] == 239 && bs[i] == 191)
                {
                    return false;
                }

            }

            return true;

        }
    }
}
