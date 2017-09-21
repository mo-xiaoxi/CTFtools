using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.IO;

namespace Seay正则URL解码匹配测试工具
{
    public partial class F_rule_encode : Form
    {
        public F_rule_encode()
        {
            InitializeComponent();
            //线程安全开关，关闭这个就能跨线程访问控件
            CheckForIllegalCrossThreadCalls = false;
        }

        string var_str = "";

        bool r = false;

        string[] rulelist = null;


        public void func_Get_Info()
        {
            lv_info.Items.Clear();
            for (int i = lv_info.Columns.Count - 1; i > 1; i--)
            {
                lv_info.Columns.RemoveAt(i);
            }
            try
            {
                MatchCollection Matches=null;
                if (cb_dxx.Checked && cb_allrows.Checked)
                {
                    Matches = Regex.Matches(
                        var_str,
                        rt_rule.Text,
                        RegexOptions.Multiline|  //多行匹配
                        RegexOptions.RightToLeft);        //从左向右匹配字符串);
                }
                else if (cb_dxx.Checked && cb_allrows.Checked==false)
                {
                    Matches = Regex.Matches(
                        var_str,
                        rt_rule.Text,
                        RegexOptions.RightToLeft);        //从左向右匹配字符串);
                }
                else if (cb_allrows.Checked&& cb_allrows.Checked == false)
                {
                    Matches = Regex.Matches(
                        var_str,
                        rt_rule.Text,
                        RegexOptions.Multiline |           //匹配多行   
                        RegexOptions.RightToLeft);        //从左向右匹配字符串);
                }
                else
                {
                    Matches = Regex.Matches(
                        var_str,
                        rt_rule.Text,
                        RegexOptions.IgnoreCase |           //不区分大小写   
                        RegexOptions.RightToLeft);        //从左向右匹配字符串);
                }
                
                GroupCollection groups = null;
                if (Matches.Count > 0)
                {
                    for (int j = 0; j < Matches.Count; j++)
                    {
                        if (Matches[j].Success)
                        {
                            ListViewItem lvitem = new ListViewItem((lv_info.Items.Count + 1).ToString());

                            groups = Matches[j].Groups;

                            lvitem.SubItems.Add(groups[0].Value);

                            if (groups.Count > 1)
                            {
                                for (int n = 1; n < groups.Count; n++)
                                {
                                    if (j == 0)
                                    {
                                        lv_info.Columns.Add("子匹配" + n);
                                    }
                                    lvitem.SubItems.Add(groups[n].Value);
                                }
                            }
                            lv_info.Items.Add(lvitem);

                        }
                    }
                }
            }
            catch (Exception)
            {
            }

        }

        public void func_Start()
        {
            try
            {
                if (rd_NoDecryption.Checked)
                {
                    var_str = rt_str.Text;
                    func_Get_Info();
                }
                else if (rd_UrlDecryption.Checked)
                {
                    var_str = UrlInfo.func_UrlDencode(rt_str.Text);
                    rt_decryption.Text = var_str;
                    func_Get_Info();
                }
                else if (rd_Base64Decryption.Checked)
                {
                    var_str = Base64Info.func_base64decode(rt_str.Text);
                    rt_decryption.Text = var_str;
                    func_Get_Info();
                }
                else if (rd_HexDecryption.Checked)
                {
                    var_str = HexInfo.func_HexDecode(rt_str.Text);
                    rt_decryption.Text = var_str;
                    func_Get_Info();
                }
            }
            catch (Exception)
            {
            }

        }

        public void func_LoadRule()
        {
            rulelist = global::Seay代码审计工具.Properties.Resources.guize.Split(';');

            System.Collections.ArrayList list = new System.Collections.ArrayList();

            for (int i = 0; i < rulelist.Length; i++)
            {
                ListItem listItem = new ListItem();
                listItem.ValueField = i;
                listItem.TextField = rulelist[i].Split('`')[0];
                list.Add(listItem);
            }

            cmbb_rules.DataSource = list;
            cmbb_rules.DisplayMember = "TextField";
            cmbb_rules.ValueMember = "ValueField";
            
        }

        public void func_LoadSyntax()
        {
            string var_rules = global::Seay代码审计工具.Properties.Resources.relus;

            foreach (string onerule in var_rules.Split(';'))
            {
                ListViewItem lvitem = new ListViewItem(onerule.Split('`')[0]);
                lvitem.SubItems.Add(onerule.Split('`')[1]);
                lv_rules.Items.Add(lvitem);
            }

        }

        private void F_Main_Load(object sender, EventArgs e)
        {
            func_LoadRule();
            func_LoadSyntax();
            cmbb_rules.SelectedIndex = 0;
            cmbb_choose.SelectedIndex = 0;
            lv_rules.Columns[1].Width = lv_rules.Width - lv_rules.Columns[0].Width - 25;
            rt_rule.Text = "";
        }

        private void rt_rule_TextChanged(object sender, EventArgs e)
        {
            func_Start();

        }

        private void rt_str_TextChanged(object sender, EventArgs e)
        {
            func_Start();
        }

        private void 正则搜索ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tc_allcon.SelectedIndex = 0;
        }

        private void 正则语法ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tc_allcon.SelectedIndex = 1;
        }

        private void F_Main_Resize(object sender, EventArgs e)
        {
            lv_rules.Columns[1].Width = lv_rules.Width - lv_rules.Columns[0].Width - 25;
        }

        private void bianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tc_allcon.SelectedIndex = 2;
        }

        private void btn_encode_Click(object sender, EventArgs e)
        {
            if (txt_decode.Text == "")
            {
                MessageBox.Show("请输入要转换的内容", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                if (cmbb_choose.SelectedIndex == 0)
                {
                    txt_encode.Text = md5encode.func_Md5EncryptCode(txt_decode.Text);
                }
                else if (cmbb_choose.SelectedIndex == 1)
                {
                    txt_encode.Text = UrlInfo.func_UrlEncode(txt_decode.Text);
                }
                else if (cmbb_choose.SelectedIndex == 2)
                {
                    txt_encode.Text = Base64Info.func_base64encode(txt_decode.Text);
                }
                else if (cmbb_choose.SelectedIndex == 3)
                {
                    txt_encode.Text = HexInfo.func_HexEncode(txt_decode.Text);
                }
                else if (cmbb_choose.SelectedIndex == 4)
                {
                    if (rb_MySqlChar.Checked == true)
                    {
                        txt_encode.Text = ascii_info.func_asciiEncode(txt_decode.Text, "sqlchar");
                    }
                    else if (rb_fromchar.Checked == true)
                    {
                        txt_encode.Text = ascii_info.func_asciiEncode(txt_decode.Text, "fromchar");
                    }
                    else if(rb_MsSqlChar.Checked)
	                {
                        txt_encode.Text = ascii_info.func_asciiEncode(txt_decode.Text, "mssqlchar");
	                }
                    else
                    {
                        txt_encode.Text = ascii_info.func_asciiEncode(txt_decode.Text, "");
                    }
                }
                else if (cmbb_choose.SelectedIndex == 5)
                {
                    txt_encode.Text = UnicodeInfo.func_UnicodeEncode(txt_decode.Text);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("转换失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void btn_decode_Click(object sender, EventArgs e)
        {
            if (txt_decode.Text == "")
            {
                MessageBox.Show("请输入要转换的内容", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                if (cmbb_choose.SelectedIndex == 1)
                {
                    txt_encode.Text = UrlInfo.func_UrlDencode(txt_decode.Text);
                }
                else if (cmbb_choose.SelectedIndex == 2)
                {
                    txt_encode.Text = Base64Info.func_base64decode(txt_decode.Text);
                }
                else if (cmbb_choose.SelectedIndex == 3)
                {
                    txt_encode.Text = HexInfo.func_HexDecode(txt_decode.Text);
                }
                else if (cmbb_choose.SelectedIndex == 4)
                {
                    txt_encode.Text = ascii_info.func_asciiDecode(txt_decode.Text);
                }
                else if (cmbb_choose.SelectedIndex == 5)
                {
                    txt_encode.Text = UnicodeInfo.func_UnicodeDecode(txt_decode.Text);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("转换失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void cmbb_choose_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbb_choose.SelectedIndex != 4)
            {
                rb_MySqlChar.Enabled = false;
                rb_fromchar.Enabled = false;
                rb_MsSqlChar.Enabled = false;

                rb_none.Checked = true;
                txt_encode.Text = ""; 
                
            }
            else
            {
                txt_encode.Text = "ascii解码以逗号分隔，如 97,98,99,100";
                rb_MySqlChar.Enabled = true;
                rb_fromchar.Enabled = true;
                rb_MsSqlChar.Enabled = true;
            }
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("http://www.cnseay.com/");
            }
            catch (Exception)
            {
                MessageBox.Show("访问出错", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pOST提交ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tc_allcon.SelectedIndex = 3;
        }

        private void linkLabel3_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("http://www.cnseay.com/1105/");
            }
            catch (Exception)
            {
            }
        }


        private void cmbb_rules_SelectedIndexChanged(object sender, EventArgs e)
        {
            rt_rule.Text += rulelist[cmbb_rules.SelectedIndex].Split('`')[1];
        }

        private void 作者博客ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("http://www.cnseay.com/");
            }
            catch (Exception)
            {
            }
        }


        private void 导出结果ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lv_rules.Items.Count > 0)
            {
                try
                {
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string tmpstra = "";
                        foreach (ListViewItem item in lv_info.Items)
                        {
                            for (int i = 0; i < item.SubItems.Count; i++)
                            {
                                tmpstra += item.SubItems[i].Text+"\t";
                            }
                            tmpstra += "\r\n";
                        }
                        File.WriteAllText(saveFileDialog1.FileName, tmpstra);
                        MessageBox.Show("保存成功");
                    }
                   
                }
                catch (Exception)
                {
                    MessageBox.Show("保存失败");
                }
                
                
            }
        }
    }
}
