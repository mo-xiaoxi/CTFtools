using System;
using System.Collections.Generic;
using System.Text;

namespace Seay正则URL解码匹配测试工具
{
    class HexInfo
    {
        /// <summary>
        /// 转换到16进制
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string func_HexEncode(string s)
        {
            if ((s.Length % 2) != 0)
            {
                s += " ";//空格
            }

            System.Text.Encoding chs = System.Text.Encoding.GetEncoding("GBK");

            byte[] bytes = chs.GetBytes(s);

            string str = "";

            for (int i = 0; i < bytes.Length; i++)
            {
                str += string.Format("{0:X}", bytes[i]);

            }

            return "0x" + str.ToLower();
        }


        ///<summary>   
        /// 从16进制转换到10进制   
        /// </summary>   
        /// <param name="hex"></param>   
        /// <returns></returns>   

        public static string func_HexDecode(string hex)
        {
            hex = hex.Replace("0x", "");
            if (hex.Length % 2 != 0)
            {
                hex += "20";//空格   
            }

            // 需要将 hex 转换成 byte 数组。
            byte[] bytes = new byte[hex.Length / 2];

            for (int i = 0; i < bytes.Length; i++)
            {
                try
                {
                    // 每两个字符是一个 byte。 
                    bytes[i] = byte.Parse(hex.Substring(i * 2, 2),
                    System.Globalization.NumberStyles.HexNumber);
                }
                catch
                {
                }

            }
            System.Text.Encoding chs = System.Text.Encoding.GetEncoding("GBK");

            return chs.GetString(bytes);

        }
    }
}
