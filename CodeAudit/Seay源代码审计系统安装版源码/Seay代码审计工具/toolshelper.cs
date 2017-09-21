using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Net;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Seay代码审计工具
{
    /// <summary>
    /// 利用双缓冲解决listview闪屏问题
    /// </summary>
    public class ListViewNF : System.Windows.Forms.ListView    
    {
        public ListViewNF()       
        {            
            // 开启双缓冲            
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);            
            // Enable the OnNotifyMessage event so we get a chance to filter out             
            // Windows messages before they get to the form's WndProc            
            this.SetStyle(ControlStyles.EnableNotifyMessage, true);        
        }        
        
        protected override void OnNotifyMessage(Message m)        
        {            
            //Filter out the WM_ERASEBKGND message            
            if (m.Msg != 0x14)            
            {                
                base.OnNotifyMessage(m);
            }        
        }    
    }


    class toolshelper
    {
        /// <summary>
        /// 载入自定义编辑器
        /// </summary>
        /// <returns></returns>
        public static toolsinfo func_loadeditplus()
        {
            toolsinfo ts = new toolsinfo();
            if (File.Exists(Application.StartupPath + "\\config\\editor.bin"))
            {
                ts.edits = new List<ToolStripMenuItem>();
                string[] pathName = File.ReadAllLines(Application.StartupPath + "\\config\\editor.bin", Encoding.GetEncoding("GBK"));

                foreach (string str in pathName)
                {
                    ToolStripMenuItem tsm1 = new ToolStripMenuItem(str.Split('=')[0] + "打开");  //获取名字
                    tsm1.Tag = str.Split('=')[1];
                    ts.edits.Add(tsm1);
                }
            }
            return ts;
        }


        /// <summary>
        /// 使用次数统计
        /// </summary>
        public static void func_countuse()
        {
            try
            {
                WebClient wc = new WebClient();
                wc.OpenRead("http://www.cnseay.com/tools/count.php?count=seay");
                
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 检测新版本
        /// </summary>
        /// <returns></returns>
        public static string Get_NewVerSion(bool start)
        {
            string str = "";
            Stream myStream = null;
            try
            {
                WebClient wc = new WebClient();
                myStream = wc.OpenRead("http://www.cnseay.com/tools/ver.txt");
                StreamReader sr = new StreamReader(myStream, System.Text.Encoding.GetEncoding("GBK"));
                str = sr.ReadToEnd();

            }
            catch (Exception)
            {
                if (!start)
                {
                    MessageBox.Show("获取失败，请到www.cnseay.com下载最新版", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
            }
            finally
            {
                try
                {
                    myStream.Close();
                }
                catch (Exception)
                {
                }
            }
            return str;
            
        }

        /// <summary>
        /// 转换HTML实体
        /// </summary>
        /// <param name="oldstr"></param>
        /// <returns></returns>
        public static string func_HtmlEntity(string oldstr)
        {
            return oldstr.Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'","&apos;");
        }

        public static string func_queryen(string keyword)
        { 
            string str = "";
            Stream myStream = null;
            StreamReader sr=null;
            try
            {
                keyword = keyword.Replace("\r\n",",");
                WebClient wc = new WebClient();
                myStream = wc.OpenRead("http://openapi.baidu.com/public/2.0/bmt/translate?client_id=IGRde9G9hsF3y6zr4aVPvMxq&from=auto&to=auto&q=" + Seay正则URL解码匹配测试工具.UrlInfo.func_UrlEncode(keyword));
                sr = new StreamReader(myStream, System.Text.Encoding.GetEncoding("GBK"));
                str = sr.ReadToEnd();
                JsonReader reader = new JsonTextReader(new StringReader(str));

                JObject jo = (JObject)JsonConvert.DeserializeObject(str);

                str ="";

                foreach (var item in jo["trans_result"])
                {
                    str = item["dst"].ToString();
                }
            }
            catch (Exception)
            { }
            finally
            {
                myStream.Close();
                sr.Close();
            }
            return str;
        }


        public static int fileNum = 0;
        
        /// <summary>
        /// 获取某目录下的所有文件(包括子目录下文件)的数量
        /// </summary>
        /// <param name="srcPath"></param>
        /// <returns></returns>
        public static int GetFileNum(string srcPath)
        {
            try
            {
                DirectoryInfo RootFolder = new DirectoryInfo(srcPath);

                fileNum += RootFolder.GetFiles("*.php").Length;

                //遍历子文件夹
                foreach (DirectoryInfo NextFolder in RootFolder.GetDirectories())
                {
                    try
                    {
                        GetFileNum(NextFolder.FullName);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

            }
            catch (Exception)
            {
            }
            return fileNum;
        }
    }
}
