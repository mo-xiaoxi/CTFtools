using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace upgrade
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //线程安全开关，关闭这个就能跨线程访问控件
            CheckForIllegalCrossThreadCalls = false;
        }
        string DownURL = "http://www.cnseay.com/tools/upgrade/codesafecheck.zip";
        string filename = Application.StartupPath + "\\new.zip";
       
        public void DownloadFile()
        {
            listbox_info.Items.Add("开始升级...");
            try
            {
                System.Net.HttpWebRequest Myrq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(DownURL);
                System.Net.HttpWebResponse myrp = (System.Net.HttpWebResponse)Myrq.GetResponse();
                long totalBytes = myrp.ContentLength;

                toolStripProgressBar1.Maximum = (int)totalBytes;

                listbox_info.Items.Add("正在下载升级包...");

                Stream st = myrp.GetResponseStream();
                Stream so = new FileStream(filename, System.IO.FileMode.Create);
                long totalDownloadedByte = 0;
                byte[] by = new byte[1024];
                int osize = st.Read(by, 0, (int)by.Length);
                while (osize > 0)
                {
                    totalDownloadedByte = osize + totalDownloadedByte;
                    System.Windows.Forms.Application.DoEvents();
                    so.Write(by, 0, osize);

                    toolStripProgressBar1.Value = (int)totalDownloadedByte;

                    osize = st.Read(by, 0, (int)by.Length);

                    //percent = (float)totalDownloadedByte / (float)totalBytes * 100;

                    System.Windows.Forms.Application.DoEvents(); //必须加注这句代码，否则将因为循环执行太快而来不及显示信息  

                }

                listbox_info.Items.Add("下载完成... ...");

                so.Flush();//将缓冲区内在写入到基础流中  
                st.Flush();//将缓冲区内在写入到基础流中  
                so.Close();
                st.Close();

                unzip();
                listbox_info.Items.Add("升级成功...");
            }

            catch (System.Exception)
            {
                listbox_info.Items.Add("升级失败...");
            }
        }

        public void unzip()
        {
            listbox_info.Items.Add(" ");
            listbox_info.Items.Add("正在解压文件...");
            try
            {
                Shell32.ShellClass sc = new Shell32.ShellClass();
                Shell32.Folder SrcFolder = sc.NameSpace(filename);
                Shell32.Folder DestFolder = sc.NameSpace(Application.StartupPath + "\\");
                Shell32.FolderItems items = SrcFolder.Items();
                DestFolder.CopyHere(items, 20);
                listbox_info.Items.Add("解压完成...");
            }
            catch (Exception)
            {
                listbox_info.Items.Add("解压失败...");
            }

        }

        public void Startupgrade()
        {
            if (System.Diagnostics.Process.GetProcessesByName("Seay源代码审计系统").Length > 0)
            {
                if (MessageBox.Show("检测到程序正在运行，是否结束进程进行升级", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcessesByName("Seay源代码审计系统"))
                    {
                        p.Kill();
                    }
                }
                else
                {
                    Application.Exit();
                    Thread.CurrentThread.Abort();
                    return;
                }
            }
            DownloadFile();
            if (MessageBox.Show("是否重新启动程序", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                try
                {
                    System.Diagnostics.Process.Start("Seay源代码审计系统.exe");
                }
                catch (Exception)
                {
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Thread th = new Thread(Startupgrade);
                th.Start();
            }
            catch (Exception)
            {

            }

        }
    }
}
