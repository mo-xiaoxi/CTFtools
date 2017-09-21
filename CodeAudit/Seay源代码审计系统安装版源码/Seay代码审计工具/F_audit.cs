using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Diagnostics;
using System.Net;

namespace Seay代码审计工具
{
    public partial class F_audit : Form
    {
        private ListViewColumnSorter lvwColumnSorter;
        public F_audit()
        {
            InitializeComponent();
            //线程安全开关，关闭这个就能跨线程访问控件
            CheckForIllegalCrossThreadCalls = false;
            lvwColumnSorter = new ListViewColumnSorter();
            this.lv_buglist.ListViewItemSorter = lvwColumnSorter;
        }

        /// <summary>
        /// 文件编码
        /// </summary>
        string var_encoding = "";

        /// <summary>
        /// 规则路径
        /// </summary>
        string var_rulepath = Application.StartupPath + "\\config\\rule.bin";

        /// <summary>
        /// 线程
        /// </summary>
        Thread myThread = null;

        MatchCollection Matches = null;

        string[] var_rulearr = null;

        string var_filetext = "";

        public string var_model { get; set; }

        string var_errorinfo = "";

        List<string> var_bugresult = new List<string>();

        List<string> var_errorinfolist = new List<string>();

        public void func_checkfile(string file)
        {
            pgb_scan.Value += 1;
            lbl_state.Text = file;

            try
            {
                var_filetext = File.ReadAllText(file, Encoding.GetEncoding(F_Main.var_fileencoding));

                foreach (string rule in var_rulearr)
                {
                    if (rule.Split('谶')[0] == "1" && var_filetext != "")
                    {
                        Matches = Regex.Matches(
                                    var_filetext,
                                    "\n.*" + rule.Split('谶')[1] + ".*\n",
                                    RegexOptions.IgnoreCase|
                                    RegexOptions.RightToLeft);        //从左向右匹配字符串

                        if (Matches.Count > 0)
                        {
                            for (int j = 0; j < Matches.Count; j++)
                            {
                                if (Matches[j].Success)
                                {
                                    if (!var_bugresult.Contains(rule + file + Matches[j].Value.Trim()))
                                    {
                                        ListViewItem lvitem = new ListViewItem((lv_buglist.Items.Count + 1).ToString());

                                        lvitem.SubItems.Add(rule.Split('谶')[2]);
                                        lvitem.SubItems.Add(file.Replace(F_Main.var_webpath, "").Replace("\\", "/"));
                                        lvitem.SubItems.Add(Matches[j].Value.Trim());
                                        lvitem.Tag = file;
                                        lv_buglist.Items.Add(lvitem);
                                        var_bugresult.Add(rule + file + Matches[j].Value.Trim());

                                    }
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception)
            {
            }
        }

        Stream myStream = null;

        string url = "";

        List<string> var_urllist = new List<string>();

        StreamReader sr = null;

        string var_cookie = "";

        public void func_loadphperror(string path)
        {
            pgb_scan.Value += 1;
            func_getvar(path);

            foreach (string var in var_urllist)
            {
                var_errorinfo = "";
                try
                {
                    lbl_state.Text = url + var.Replace("&","&&");
                    WebClient wc = new WebClient();

                    wc.Headers.Add("Cookie", var_cookie);
                    myStream = wc.OpenRead(url + var);
                    sr = new StreamReader(myStream);
                    var_errorinfo = sr.ReadToEnd();

                }
                catch (Exception)
                {
                    continue;
                }
                finally
                {
                    myStream.Close();
                    sr.Close();
                }
                if (var_errorinfo.Contains("<b>Notice</b>: Use") || var_errorinfo.Contains("<b>Warning</b>:") || var_errorinfo.Contains("<b>Fatal error</b>:"))
                {
                    if (var_errorinfolist.Contains(path + var_errorinfo))
                    {
                        continue;
                    }

                    ListViewItem lvitem = new ListViewItem((lv_buglist.Items.Count + 1).ToString());

                    lvitem.SubItems.Add("存在敏感信息泄露漏洞");
                    lvitem.SubItems.Add(url + var);
                    lvitem.SubItems.Add(var_errorinfo.Length < 150 ? var_errorinfo : var_errorinfo.Remove(150));
                    lvitem.Tag = path;
                    lv_buglist.Items.Add(lvitem);
                    var_errorinfolist.Add(path + var_errorinfo);
                }
            }
        }

        public void func_getvar(string path)
        {
            string var_urlparameters = "";
            var_urllist.Clear();
            try
            {
                var_filetext = File.ReadAllText(path, Encoding.GetEncoding(F_Main.var_fileencoding));
                path = path.Replace(F_Main.var_webpath, "").Replace("\\", "/");
                var_urllist.Add(path);
                if (var_filetext != "")
                {
                    Matches = Regex.Matches(
                                var_filetext,
                                "\\$_(GET|REQUEST)\\[['\"]([a-zA-Z0-9_]{1,30})['\"]\\]",
                                RegexOptions.RightToLeft);        //从左向右匹配字符串

                    if (Matches.Count > 0)
                    {
                        for (int j = 0; j < Matches.Count; j++)
                        {
                            if (Matches[j].Success)
                            {
                                if (var_urlparameters.Contains(Matches[j].Groups[2].Value + "[]=Seay"))
                                {
                                    continue;
                                }
                                var_urlparameters+=Matches[j].Groups[2].Value + "[]=Seay&";
                            }
                        }
                        var_urllist.Add(path + "?" + var_urlparameters);
                    }
                }

            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 信息泄露扫描
        /// </summary>
        /// <param name="path">文件路径</param>
        public void func_errorScan(object path)
        {
            DirectoryInfo RootFolder = new DirectoryInfo(path.ToString());

            //遍历主文件夹文件
            foreach (FileInfo RootFile in RootFolder.GetFiles("*.php"))
            {
                func_loadphperror(RootFile.FullName);
            }

            //遍历子文件夹
            foreach (DirectoryInfo NextFolder in RootFolder.GetDirectories())
            {
                func_errorScan(NextFolder.FullName);
            }
        }

        public void func_startscan(object path)
        {
            toolshelper.fileNum = 0;
            pgb_scan.Value = 0;
            pgb_scan.Maximum = toolshelper.GetFileNum(path.ToString());

            System.Diagnostics.Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start(); //  开始监视代码运行时间


            if (var_model == "自动审计")
            {
                var_rulearr = File.ReadAllLines(var_rulepath, Encoding.GetEncoding("GBK"));
                func_Scanbug(path);

            }
            else if (var_model == "信息泄露审计")
            {
                if (cmbb_url.Text == "")
                {
                    MessageBox.Show("请输入访问程序URL地址", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                try
                {
                    WebClient wc = new WebClient();
                    myStream = wc.OpenRead(cmbb_url.Text);
                    url = cmbb_url.Text;
                    var_cookie = txt_cookie.Text;
                    if (url.Substring(url.Length) != "/")
                    {
                        url += "/";
                    }
                    func_errorScan(path);
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("无法解析此远程名称") || ex.Message.Contains("无法连接到远程服务器"))
                    {
                        MessageBox.Show("URL错误，请输入本地程序访问URL", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
            }
            stopwatch.Stop(); //  停止监视
            TimeSpan timespan = stopwatch.Elapsed;

            string time = "";

            if (timespan.TotalSeconds>60)
            {
                time = String.Format("{0:F}", timespan.TotalMinutes) + "分钟";  // 总分钟
            }
            else
            {
                time = String.Format("{0:F}", timespan.TotalSeconds) + "秒";  //  总秒数
            }

            lbl_state.Text = "扫描完成，发现" + lv_buglist.Items.Count + "个可疑漏洞，花费时间"+time;
            if (myThread != null && myThread.ThreadState == System.Threading.ThreadState.Running)
            {
                try
                {
                    myThread.Abort();
                }
                catch (Exception)
                {
                }
            }
        }

        public void func_Scanbug(object path)
        {
            if (!File.Exists(var_rulepath))
            {
                MessageBox.Show("请先添加扫描规则", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DirectoryInfo RootFolder = new DirectoryInfo(path.ToString());

            //遍历主文件夹文件
            foreach (FileInfo RootFile in RootFolder.GetFiles("*.php"))
            {
                func_checkfile(RootFile.FullName);
            }

            //遍历子文件夹
            foreach (DirectoryInfo NextFolder in RootFolder.GetDirectories())
            {
                func_Scanbug(NextFolder.FullName);
            }
        }

        private void F_audit_Load(object sender, EventArgs e)
        {
            var_encoding = F_Main.var_fileencoding;
            foreach (ToolStripMenuItem item in toolshelper.func_loadeditplus().edits)
            {
                cms_father.Items.Add(item);
            }

        }

        private void F_audit_Resize(object sender, EventArgs e)
        {
            //随窗体大小改变Listview的列
            lv_buglist.Columns[3].Width = lv_buglist.Width - (lv_buglist.Columns[0].Width + lv_buglist.Columns[1].Width + lv_buglist.Columns[2].Width) - 23;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (myThread != null && myThread.ThreadState == System.Threading.ThreadState.Running)
            {
                MessageBox.Show("正在审计中，请先停止", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            lv_buglist.Items.Clear();
            var_bugresult.Clear();
            var_errorinfolist.Clear();

            if (Directory.Exists(F_Main.var_webpath))
            {
                ParameterizedThreadStart ParStart = new ParameterizedThreadStart(func_startscan);//引用函数名称
                myThread = new Thread(ParStart);//定义多线程
                myThread.Start(F_Main.var_webpath);//启动带参数的多线程
            }
            else
            {
                MessageBox.Show("目录" + F_Main.var_webpath + "不存在,请选择扫描目录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (myThread != null && myThread.ThreadState == System.Threading.ThreadState.Running)
            {
                try
                {
                    myThread.Abort();
                }
                catch (Exception)
                {
                }
            }
            lbl_state.Text = "扫描完成，发现" + lv_buglist.Items.Count + "个可疑漏洞";
        }

        private void F_audit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (myThread != null && myThread.ThreadState == System.Threading.ThreadState.Running)
            {
                try
                {
                    myThread.Abort();
                }
                catch (Exception)
                {
                }
            }

        }

        public void func_openfile()
        {
            if (lv_buglist.SelectedItems.Count > 0)
            {
                string filepath = lv_buglist.SelectedItems[0].Tag.ToString();
                if (File.Exists(lv_buglist.SelectedItems[0].Tag.ToString()))
                {
                    try
                    {
                        TabPage tpage = new TabPage(filepath.Substring(filepath.LastIndexOf('\\') + 1));
                        //tpage.MouseDoubleClick += new MouseEventHandler(tabPage_DoubleClick);
                        F_code form = new F_code();//动态创建一个窗体
                        form.FormBorderStyle = FormBorderStyle.None;//取消边框
                        form.TopLevel = false;
                        form.Dock = DockStyle.Fill;//控件边缘控制
                        form.var_filepath = filepath;
                        tpage.Tag = filepath;
                        form.txt_search.Text = lv_buglist.SelectedItems[0].SubItems[3].Text;
                        tpage.Controls.Add(form);
                        F_Main.Tab.TabPages.Add(tpage);


                        form.Show();
                        F_Main.Tab.SelectedTab = tpage;

                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private void lv_buglist_DoubleClick(object sender, EventArgs e)
        {
            func_openfile();
        }

        private void comm_editplus_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "打开文件")
            {
                func_openfile();
            }
            else if (e.ClickedItem.Text == "复制路径")
            {
                if (lv_buglist.SelectedItems.Count > 0)
                {
                    Clipboard.SetDataObject(lv_buglist.SelectedItems[0].SubItems[2].Text);
                }
            }
            else if (e.ClickedItem.Text == "生成报告")
            {
                func_report();
            }
            else
            {
                if (!File.Exists(e.ClickedItem.Tag.ToString()))
                {
                    MessageBox.Show("编辑器错误，请在编辑器配置重新设置", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                try
                {
                    //指定进程开所选中的结果文件
                    Process.Start(e.ClickedItem.Tag.ToString(), lv_buglist.SelectedItems[0].Tag.ToString());
                }
                catch (Exception)
                {
                }
            }
        }

        public void func_report()
        {
            if (sfd_report.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(Application.StartupPath + "\\config\\report.html"))
                {
                    try
                    {
                        string var_report = "";

                        foreach (ListViewItem item in lv_buglist.Items)
                        {
                            var_report += "<tr><td width=\"5%\">" + item.SubItems[0].Text + "</td><td width=\"20%\">" + item.SubItems[1].Text + "</td><td width=\"30%\">" + item.SubItems[2].Text + "</td><td width=\"45%\">" + toolshelper.func_HtmlEntity(item.SubItems[3].Text) + "</td></tr>\r\n";
                        }
                        File.WriteAllText(sfd_report.FileName, File.ReadAllText(Application.StartupPath + "\\config\\report.html", Encoding.GetEncoding("GBK")).Replace("$content$", var_report).Replace("$count$", lv_buglist.Items.Count.ToString()), Encoding.GetEncoding("GBK"));
                        MessageBox.Show("生成成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("生成失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    MessageBox.Show("模板文件不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            func_report();
        }

        private void lv_buglist_MouseHover(object sender, EventArgs e)
        {
            if (lv_buglist.SelectedItems.Count > 0)
            {
                toolTip1.SetToolTip(lv_buglist, lv_buglist.SelectedItems[0].SubItems[3].Text);
            }
        }

        private void txt_cookie_Enter(object sender, EventArgs e)
        {
            txt_cookie.Multiline = true;
            txt_cookie.Height = 200;
        }

        private void txt_cookie_Leave(object sender, EventArgs e)
        {
            txt_cookie.Multiline = false;
        }

        private void lv_buglist_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // 重新设置此列的排序方法.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // 设置排序列，默认为正向排序
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // 用新的排序方法对ListView排序
            this.lv_buglist.Sort();
        }
    }
}
