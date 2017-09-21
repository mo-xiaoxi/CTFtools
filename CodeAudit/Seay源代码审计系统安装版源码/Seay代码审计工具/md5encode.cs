using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Seay正则URL解码匹配测试工具
{
    class md5encode
    {
        /// <summary>
        /// MD5加密处理
        /// </summary>
        /// <param name="_Str">要加密的字符</param>
        /// <returns>加密后的字符</returns>
        public static String func_Md5EncryptCode(string _Str)
        {
            string Md5Str = "";

            MD5CryptoServiceProvider md516 = new MD5CryptoServiceProvider();
            string str_md516 = BitConverter.ToString(md516.ComputeHash(UTF8Encoding.Default.GetBytes(_Str)), 4, 8).Replace("-", "");

            Md5Str += "小写16位：" + str_md516.ToLower() + "\r\n";
            Md5Str += "大写16位：" + str_md516 + "\r\n\r\n";

            MD5 md532 = MD5.Create();
            byte[] b = Encoding.UTF8.GetBytes(_Str);
            byte[] md5b = md532.ComputeHash(b);
            md532.Clear();
            StringBuilder sbbuil = new StringBuilder();
            foreach (var item in md5b)
            {
                sbbuil.Append(item.ToString("x2"));
            }

            Md5Str += "小写32位：" + sbbuil.ToString() + "\r\n";
            Md5Str += "大写32位：" + sbbuil.ToString().ToUpper() + "\r\n";

            return Md5Str;
        }
    }
}
