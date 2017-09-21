using System;
using System.Collections.Generic;
using System.Text;

namespace Seay正则URL解码匹配测试工具
{
    class ascii_info
    {
        public static string func_asciiEncode(string str, string _chartype)
        {
            ASCIIEncoding AE1 = new ASCIIEncoding();

            byte[] ByteArray1 = AE1.GetBytes(str);

            if (_chartype == "")
            {
                string strchar = "";
                for (int i = 0; i <= ByteArray1.Length - 1; i++)
                {
                    strchar += ByteArray1[i] + ",";
                }
                strchar = strchar.Remove(strchar.Length - 1);
                return strchar;
            }
            else if (_chartype == "sqlchar")
            {
                string _restr = "char(";
                for (int i = 0; i <= ByteArray1.Length - 1; i++)
                {
                    _restr += ByteArray1[i] + ",";
                }
                _restr = _restr.Remove(_restr.Length - 1) + ")";
                return _restr;
            }
            else if (_chartype == "fromchar")
            {
                string _restr = "String.fromCharCode(";
                for (int i = 0; i <= ByteArray1.Length - 1; i++)
                {
                    _restr += ByteArray1[i] + ",";
                }
                _restr = _restr.Remove(_restr.Length - 1) + ")";
                return _restr;
            }
            else if (_chartype == "mssqlchar")
            {
                string _restr = "";
                for (int i = 0; i <= ByteArray1.Length - 1; i++)
                {
                    _restr += "char("+ByteArray1[i] + ")+";
                }
                _restr = _restr.Remove(_restr.Length - 1);
                return _restr;
            }
            return "";
        }

        public static string func_asciiDecode(string str)
        {
            string restr = "";
            for (int i = 0; i < str.Split(',').Length; i++)
            {
                int asciiCode = Convert.ToInt32(str.Split(',')[i]);
                if (asciiCode >= 0 && asciiCode <= 255)
                {
                    ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                    byte[] byteArray = new byte[] { (byte)asciiCode };
                    restr += asciiEncoding.GetString(byteArray);
                }
            }
            return restr;
        }
    }
}
