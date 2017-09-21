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

namespace Seay代码审计工具
{
    public partial class F_searchinfile : Form
    {
        public F_searchinfile()
        {
            InitializeComponent();
        }

        string var_filetext = "";

        string[] var_filelines = null;

        MatchCollection Matches = null;

        public string keyword { get; set; }

        bool isreg = false;

        bool isqufendxx = false;

        /// <summary>
        /// 线程
        /// </summary>
        Thread myThread = null;

        /// <summary>
        /// 正则搜索文件
        /// </summary>
        /// <param name="file">文件路径</param>
        public void func_checkfile(string file)
        {
            lbl_state.Text = file;

            try
            {
                var_filetext = File.ReadAllText(file, Encoding.GetEncoding(F_Main.var_fileencoding));

                if (var_filetext != "")
                {
                    if (chk_daxiaoxie.Checked)
                    {
                        Matches = Regex.Matches(
                                var_filetext,
                                "\n.*" + keyword + ".*\n",
                                RegexOptions.IgnoreCase |   //不区分大小写
                                RegexOptions.RightToLeft);        //从左向右匹配字符串
                    }
                    else
                    {
                        Matches = Regex.Matches(
                                var_filetext,
                                "\n.*" + keyword + ".*\n",
                                RegexOptions.RightToLeft);        //从左向右匹配字符串
                    }

                    if (Matches.Count > 0)
                    {
                        for (int j = 0; j < Matches.Count; j++)
                        {
                            if (Matches[j].Success)
                            {
                                ListViewItem lvitem = new ListViewItem((lv_searchresult.Items.Count + 1).ToString());

                                lvitem.SubItems.Add(file.Replace(F_Main.var_webpath, "").Replace("\\", "/"));
                                lvitem.SubItems.Add(Matches[j].Value.Trim());
                                lvitem.Tag = file;
                                lv_searchresult.Items.Add(lvitem);
                            }
                        }
                    }
                }

            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 打开文件搜索字符串
        /// </summary>
        /// <param name="file">文件路径</param>
        public void func_searchstr(string file)
        {
            lbl_state.Text = file;

            try
            {
                if (File.ReadAllText(file, Encoding.GetEncoding(F_Main.var_fileencoding)).Contains(keyword))
                {
                    var_filelines = File.ReadAllLines(file, Encoding.GetEncoding(F_Main.var_fileencoding));

                    if (var_filelines.Length > 0)
                    {
                        foreach (string str in var_filelines)
                        {
                            if (!isqufendxx)
                            {
                                if (str.Contains(keyword))
                                {
                                    ListViewItem lvitem = new ListViewItem((lv_searchresult.Items.Count + 1).ToString());

                                    lvitem.SubItems.Add(file.Replace(F_Main.var_webpath, "").Replace("\\", "/"));
                                    lvitem.SubItems.Add(str.Trim());
                                    lvitem.Tag = file;
                                    lv_searchresult.Items.Add(lvitem);
                                }
                            }
                            else
                            {
                                if (str.ToLower().Contains(keyword))
                                {
                                    ListViewItem lvitem = new ListViewItem((lv_searchresult.Items.Count + 1).ToString());

                                    lvitem.SubItems.Add(file.Replace(F_Main.var_webpath, "").Replace("\\", "/"));
                                    lvitem.SubItems.Add(str.Trim());
                                    lvitem.Tag = file;
                                    lv_searchresult.Items.Add(lvitem);
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

        /// <summary>
        /// 递归遍历目录
        /// </summary>
        /// <param name="path">目录路径</param>
        public void func_Scanbug(object path)
        {
            DirectoryInfo RootFolder = new DirectoryInfo(path.ToString());

            //遍历主文件夹文件
            foreach (FileInfo RootFile in RootFolder.GetFiles("*.php"))
            {
                if (isreg)
                {
                    func_checkfile(RootFile.FullName);
                }
                else
                {
                    func_searchstr(RootFile.FullName);
                }

            }

            //遍历子文件夹
            foreach (DirectoryInfo NextFolder in RootFolder.GetDirectories())
            {
                func_Scanbug(NextFolder.FullName);
            }
        }

        /// <summary>
        /// 开始线程
        /// </summary>
        /// <param name="path"></param>
        public void func_startscan(object path)
        {
            func_Scanbug(path);

            lbl_state.Text = "搜索完成，发现" + lv_searchresult.Items.Count + "处";
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

        public void func_startsearch()
        {
            if (chk_reg.Checked)
            {
                isreg = true;
            }
            else
            {
                isreg = false;
            }

            if (chk_daxiaoxie.Checked)
            {
                isqufendxx = true;
                keyword = keyword.ToLower();
            }
            else
            {
                isqufendxx = false;
            }

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

        private void button1_Click(object sender, EventArgs e)
        {
            if (myThread != null && myThread.ThreadState == System.Threading.ThreadState.Running)
            {
                MessageBox.Show("正在审计中，请先停止", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cmbb_keyword.Text == "")
            {
                MessageBox.Show("请输入要查找的内容", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            lv_searchresult.Items.Clear();
            keyword = cmbb_keyword.Text;
            func_startsearch();

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
            lbl_state.Text = "扫描停止，发现" + lv_searchresult.Items.Count + "处";
        }

        private void F_searchinfile_Resize(object sender, EventArgs e)
        {
            //随窗体大小改变Listview的列
            lv_searchresult.Columns[2].Width = lv_searchresult.Width - (lv_searchresult.Columns[0].Width + lv_searchresult.Columns[1].Width) - 23;
        }

        private void F_searchinfile_FormClosing(object sender, FormClosingEventArgs e)
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

        /// <summary>
        /// 打开选定文件
        /// </summary>
        public void func_openfile()
        {
            if (lv_searchresult.SelectedItems.Count > 0)
            {
                string filepath = lv_searchresult.SelectedItems[0].Tag.ToString();
                if (File.Exists(lv_searchresult.SelectedItems[0].Tag.ToString()))
                {
                    try
                    {
                        TabPage tpage = new TabPage(filepath.Substring(filepath.LastIndexOf('\\') + 1));
                        F_code form = new F_code();//动态创建一个窗体
                        form.FormBorderStyle = FormBorderStyle.None;//取消边框
                        form.TopLevel = false;
                        form.Dock = DockStyle.Fill;//控件边缘控制
                        form.txt_search.Text = lv_searchresult.SelectedItems[0].SubItems[2].Text;
                        form.var_filepath = filepath;
                        tpage.Tag = filepath;
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

        private void cms_filesearch_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "打开文件")
            {
                func_openfile();
            }
            else if (e.ClickedItem.Text == "复制路径")
            {
                if (lv_searchresult.SelectedItems.Count > 0)
                {
                    Clipboard.SetDataObject(lv_searchresult.SelectedItems[0].SubItems[1].Text);
                }
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
                    Process.Start(e.ClickedItem.Tag.ToString(), lv_searchresult.SelectedItems[0].Tag.ToString());
                }
                catch (Exception)
                {
                }
            }
        }

        private void F_searchinfile_Load(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in toolshelper.func_loadeditplus().edits)
            {
                cms_filesearch.Items.Add(item);
            }
            if (keyword != null && keyword != "")
            {
                cmbb_keyword.Text = keyword;
                func_startsearch();
            }

        }

        private void lv_searchresult_DoubleClick(object sender, EventArgs e)
        {
            func_openfile();
        }

        private void cmbb_keyword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (cmbb_keyword.Text == "")
                {
                    MessageBox.Show("请输入要查找的内容", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                lv_searchresult.Items.Clear();
                keyword = cmbb_keyword.Text;
                func_startsearch();
            }
        }
    }
}
