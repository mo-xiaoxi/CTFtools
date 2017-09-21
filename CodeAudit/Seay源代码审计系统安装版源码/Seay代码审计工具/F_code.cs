using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Threading;
using System.IO;
using System.Diagnostics;
using ICSharpCode.TextEditor.Document;

namespace Seay代码审计工具
{
    public partial class F_code : Form
    {
        public F_code()
        {
            InitializeComponent();
            //线程安全开关，关闭这个就能跨线程访问控件
            CheckForIllegalCrossThreadCalls = false;
        }

        Thread myThread = null;

        public string var_filepath { get; set; }

        string var_filetext = "";

        private void 查询函数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txt_code.ActiveTextAreaControl.SelectionManager.SelectedText != null && txt_code.ActiveTextAreaControl.SelectionManager.SelectedText != "")
            {
                if ((new Regex("^\\w+$").Match(txt_code.ActiveTextAreaControl.SelectionManager.SelectedText)).Success)
                {
                    try
                    {
                        TabPage tpage = new TabPage("函数查询");
                        F_funcquery form = new F_funcquery();//动态创建一个窗体
                        form.funcname = txt_code.ActiveTextAreaControl.SelectionManager.SelectedText.Trim().Replace("_", "-");
                        form.cmbb_func.Text = txt_code.ActiveTextAreaControl.SelectionManager.SelectedText.Trim();
                        form.FormBorderStyle = FormBorderStyle.None;//取消边框
                        form.TopLevel = false;
                        form.Dock = DockStyle.Fill;//控件边缘控制
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

        MatchCollection Matches = null;

        /// <summary>
        /// 获取文件中的函数
        /// </summary>
        public void func_getfunc()
        {
            lbox_var.Items.Add("--函数列表--");
            try
            {
                if (var_filetext != "")
                {
                    Matches = Regex.Matches(
                                var_filetext,
                                @"function\s{1,5}(\w{1,20})\s{0,5}\(",
                                RegexOptions.RightToLeft);        //从左向右匹配字符串

                    if (Matches.Count > 0)
                    {
                        for (int j = 0; j < Matches.Count; j++)
                        {
                            if (Matches[j].Success)
                            {
                                lbox_var.Items.Add(Matches[j].Groups[1].Value);
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
        /// 获取文件中的变量
        /// </summary>
        public void func_getvar()
        {
            lbox_var.Items.Add("            ");
            lbox_var.Items.Add("--变量列表--");
            //List<string> varlist = new List<string>();
            try
            {
                if (var_filetext != "")
                {
                    Matches = Regex.Matches(
                                var_filetext,
                                "\\$\\w{1,20}((\\[[\"']|\\[)\\${0,1}[\\w\\[\\]\"']{0,30}){0,1}",
                                RegexOptions.RightToLeft);        //从左向右匹配字符串

                    if (Matches.Count > 0)
                    {
                        for (int j = 0; j < Matches.Count; j++)
                        {
                            if (Matches[j].Success && !lbox_var.Items.Contains(Matches[j].Value))
                            {
                                lbox_var.Items.Add(Matches[j].Value);
                                //varlist.Add(Matches[j].Value);
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
        /// 载入文件到编辑器
        /// </summary>
        public void func_loadcode()
        {

            if (var_filepath != "")
            {
                StreamReader sr = null;

                try
                {
                    sr = new StreamReader(var_filepath, Encoding.GetEncoding(F_Main.var_fileencoding));
                    var_filetext = sr.ReadToEnd();
                    txt_code.Text =
                    txt_code.Text = var_filetext;

                }
                catch (Exception)
                {
                }
                finally
                {
                    sr.Close();
                }
            }
        }

        /// <summary>
        /// 字符串查找
        /// </summary>
        public void func_startfind()
        {
            lbox_var.Items.Clear();
            func_loadcode();

            if (txt_search.Text != "")
            {
                func_searchkey(txt_search.Text);
            }

            func_getfunc();
            func_getvar();
            foreach (ToolStripMenuItem item in toolshelper.func_loadeditplus().edits)
            {
                cms_codems.Items.Add(item);
            }

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

        private void F_code_Load(object sender, EventArgs e)
        {
            txt_code.Encoding = System.Text.Encoding.Default;
            txt_code.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("PHP");

            try
            {
                myThread = new Thread(func_startfind);//定义多线程
                myThread.Start();//启动带参数的多线程

            }
            catch (Exception)
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

        }

        private void 调试选中ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (txt_code.ActiveTextAreaControl.SelectionManager.SelectedText != null && txt_code.ActiveTextAreaControl.SelectionManager.SelectedText != "")
            {
                try
                {
                    TabPage tpage = new TabPage("代码调试");
                    F_Runphp form = new F_Runphp();//动态创建一个窗体
                    form.FormBorderStyle = FormBorderStyle.None;//取消边框
                    form.TopLevel = false;
                    form.Dock = DockStyle.Fill;//控件边缘控制
                    tpage.Controls.Add(form);
                    F_Main.Tab.TabPages.Add(tpage);

                    form.txt_phpcode.Text = "<?php \r\n\r\n" + txt_code.ActiveTextAreaControl.SelectionManager.SelectedText + " \r\n?>";
                    form.Show();
                    F_Main.Tab.SelectedTab = tpage;
                }
                catch (Exception)
                {
                }

            }


        }

        private void 重新载入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                myThread = new Thread(func_startfind);//定义多线程
                myThread.Start();//启动带参数的多线程
            }
            catch (Exception)
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
        }

        private void 复制选中ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txt_code.ActiveTextAreaControl.SelectionManager.SelectedText != null && txt_code.ActiveTextAreaControl.SelectionManager.SelectedText != "")
            {
                Clipboard.SetDataObject(txt_code.ActiveTextAreaControl.SelectionManager.SelectedText);
            }
        }

        int index = 0;
        public void func_searchkey(string safekey)
        {
            if (txt_code.Text.Contains(safekey))
            {
                if (index == -1)
                {
                    index = 0;
                }
                if (txt_code.ActiveTextAreaControl.SelectionManager.SelectedText == safekey)
                {
                    index = txt_code.Text.IndexOf(safekey, index);
                    if (index != -1)
                    {
                        //设置选择的内容
                        Point start = this.txt_code.Document.OffsetToPosition(index);
                        Point end = this.txt_code.Document.OffsetToPosition(index + safekey.Length);
                        this.txt_code.ActiveTextAreaControl.SelectionManager.SetSelection(new DefaultSelection(this.txt_code.Document, start, end));

                        //滚动到内容处
                        this.txt_code.ActiveTextAreaControl.Caret.Position = end;
                        this.txt_code.ActiveTextAreaControl.TextArea.ScrollToCaret();
                        index = index + safekey.Length;
                    }
                }
                else
                {
                    index = txt_code.Text.IndexOf(safekey);
                    if (index != -1)
                    {
                        //设置选择的内容
                        Point start = this.txt_code.Document.OffsetToPosition(index);
                        Point end = this.txt_code.Document.OffsetToPosition(index + safekey.Length);
                        this.txt_code.ActiveTextAreaControl.SelectionManager.SetSelection(new DefaultSelection(this.txt_code.Document, start, end));

                        //滚动到内容处
                        this.txt_code.ActiveTextAreaControl.Caret.Position = end;
                        this.txt_code.ActiveTextAreaControl.TextArea.ScrollToCaret();
                        index = index + safekey.Length + 1;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            func_searchkey(txt_search.Text.Trim());
        }

        private void lbox_var_DoubleClick(object sender, EventArgs e)
        {
            func_searchkey(lbox_var.SelectedItems[0].ToString());
        }

        private void lbox_var_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbox_var.SelectedItems.Count > 0)
            {
                if (lbox_var.SelectedItems[0].ToString() != "--函数列表--" && lbox_var.SelectedItems[0].ToString() != "--变量列表--")
                {
                    lbox_varlist.Items.Clear();
                    lbox_varlist.Items.Add("ID\t详细信息");
                    try
                    {
                        if (var_filetext != "")
                        {
                            Matches = Regex.Matches(
                                        var_filetext,
                                        "\\n.*" + lbox_var.SelectedItems[0].ToString().Replace("$", "\\$").Replace("[", "\\[").Replace("]", "\\]") + ".*\\n");        //从左向右匹配字符串

                            if (Matches.Count > 0)
                            {
                                for (int j = 0; j < Matches.Count; j++)
                                {
                                    if (Matches[j].Success)
                                    {
                                        lbox_varlist.Items.Add(lbox_varlist.Items.Count + "\t" + Matches[j].Value.Trim());
                                    }
                                }
                            }
                        }

                    }
                    catch (Exception)
                    {
                    }

                }
            }

        }

        private void lbox_varlist_DoubleClick(object sender, EventArgs e)
        {
            if (lbox_varlist.SelectedItems.Count > 0)
            {
                if (lbox_varlist.SelectedItems[0].ToString() != "ID	详细信息")
                {
                    //textBox1.Text= lbox_varlist.SelectedItems[0].ToString().Split('\t')[1].Trim();
                    func_searchkey(lbox_varlist.SelectedItems[0].ToString().Split('\t')[1].Trim());
                }

            }
        }

        private void F_code_FormClosing(object sender, FormClosingEventArgs e)
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

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text.Contains("打开"))
            {
                if (!File.Exists(e.ClickedItem.Tag.ToString()))
                {
                    MessageBox.Show("编辑器错误，请在编辑器配置重新设置", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                try
                {
                    //指定进程开所选中的结果文件
                    Process.Start(e.ClickedItem.Tag.ToString(), var_filepath);
                }
                catch (Exception)
                {
                }
            }
        }

        private void F_code_Activated(object sender, EventArgs e)
        {
            MessageBox.Show(var_filepath);
        }

        private void 全局搜索ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txt_code.ActiveTextAreaControl.SelectionManager.SelectedText != null && txt_code.ActiveTextAreaControl.SelectionManager.SelectedText != "")
            {
                try
                {
                    TabPage tpage = new TabPage("全局搜索");
                    F_searchinfile searchinfile = new F_searchinfile();//动态创建一个窗体
                    searchinfile.FormBorderStyle = FormBorderStyle.None;//取消边框
                    searchinfile.TopLevel = false;
                    searchinfile.keyword = txt_code.ActiveTextAreaControl.SelectionManager.SelectedText;
                    searchinfile.Dock = DockStyle.Fill;//控件边缘控制
                    tpage.Controls.Add(searchinfile);
                    F_Main.Tab.TabPages.Add(tpage);

                    searchinfile.Show();
                    F_Main.Tab.SelectedTab = tpage;
                }
                catch (Exception)
                {
                }
            }
        }

        private void 保存文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Text.UTF8Encoding utf8 = new System.Text.UTF8Encoding(false);
                if (F_Main.var_fileencoding == "UTF-8")
                {
                    File.WriteAllText(var_filepath, txt_code.Text, utf8);
                }
                else
                {
                    File.WriteAllText(var_filepath, txt_code.Text, Encoding.GetEncoding(F_Main.var_fileencoding));
                }
            }
            catch (Exception)
            {
                MessageBox.Show("保存失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void F_code_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                try
                {
                    File.WriteAllText(var_filepath, txt_code.Text, Encoding.GetEncoding(F_Main.var_fileencoding));
                }
                catch (Exception)
                {
                    MessageBox.Show("保存失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void 定位函数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txt_code.ActiveTextAreaControl.SelectionManager.SelectedText != null && txt_code.ActiveTextAreaControl.SelectionManager.SelectedText != "")
            {
                try
                {
                    TabPage tpage = new TabPage("函数定位");
                    F_searchinfile searchinfile = new F_searchinfile();//动态创建一个窗体
                    searchinfile.FormBorderStyle = FormBorderStyle.None;//取消边框
                    searchinfile.TopLevel = false;
                    string tmpstr = txt_code.ActiveTextAreaControl.SelectionManager.SelectedText;
                    if (tmpstr.Contains("("))
                    {
                        tmpstr = "function " + tmpstr;
                    }
                    else
                    {
                        tmpstr = "function " + tmpstr + "(";
                    }
                    searchinfile.keyword = tmpstr;
                    searchinfile.Dock = DockStyle.Fill;//控件边缘控制
                    tpage.Controls.Add(searchinfile);
                    F_Main.Tab.TabPages.Add(tpage);

                    searchinfile.Show();
                    F_Main.Tab.SelectedTab = tpage;
                }
                catch (Exception)
                {
                }
            }
        }

        private void 英汉翻译ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txt_code.ActiveTextAreaControl.SelectionManager.SelectedText != null && txt_code.ActiveTextAreaControl.SelectionManager.SelectedText != "")
            {
                try
                {
                    F_Main.F_thisMain.txt_enword.Text = txt_code.ActiveTextAreaControl.SelectionManager.SelectedText.Trim();
                    F_Main.F_thisMain.txt_cnword.Text = toolshelper.func_queryen(txt_code.ActiveTextAreaControl.SelectionManager.SelectedText.Trim());

                }
                catch (Exception)
                {
                    MessageBox.Show("翻译失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void 字符追踪ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txt_code.ActiveTextAreaControl.SelectionManager.SelectedText != null && txt_code.ActiveTextAreaControl.SelectionManager.SelectedText != "")
            {
                //if ((new Regex("^\\w+$").Match(txt_code.ActiveTextAreaControl.SelectionManager.SelectedText)).Success)
                //{
                    lbox_varlist.Items.Clear();
                    lbox_varlist.Items.Add("ID\t详细信息");
                    try
                    {
                        if (var_filetext != "")
                        {
                            Matches = Regex.Matches(
                                        var_filetext,
                                        "\\n.*" + txt_code.ActiveTextAreaControl.SelectionManager.SelectedText.Replace(@"\", @"\\").Replace("$", "\\$").Replace("[", "\\[").Replace("]", "\\]").Replace(".", "\\.").Replace("*", "\\*").Replace("!", "\\!").Replace("+", "\\+").Replace("|", "\\|").Replace("?", "\\?").Replace("^", "\\^").Replace("}", "\\}").Replace("{", "\\{") + ".*\\n"
                                        );        

                            if (Matches.Count > 0)
                            {
                                for (int j = 0; j < Matches.Count; j++)
                                {
                                    if (Matches[j].Success)
                                    {
                                        lbox_varlist.Items.Add(lbox_varlist.Items.Count + "\t" + Matches[j].Value.Trim());
                                    }
                                }
                            }
                        }

                    }
                    catch (Exception)
                    {
                    }
                //}
            }
        }

        private void 复制路径ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(var_filepath.Replace("\\","/"));
        }
    }
}
