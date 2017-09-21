using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;

namespace Seay代码审计工具
{
    public partial class F_SysConfig : Form
    {
        public F_SysConfig()
        {
            InitializeComponent();
        }

        string Configpath = Application.StartupPath + "/config/rule.bin";

        private void button1_Click(object sender, EventArgs e)
        {
            Match mt = new Regex(txt_rule.Text).Match(txt_teststr.Text);

            if (mt.Success)
            {
                MessageBox.Show(mt.Value, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("匹配失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        /// <summary>
        /// 遍历扫描规则文件，列出规则
        /// </summary>
        public void func_Getrule()
        {
            if (LV_rules.Items.Count > 0)
            {
                LV_rules.Items.Clear();
            }

            if (!File.Exists(Configpath))
            {
                return;
            }

            StreamReader sr = null;

            try
            {
                string rulestr = "";

                sr = new StreamReader(Configpath, Encoding.GetEncoding("GBK"));
                while ((rulestr = sr.ReadLine()) != null)
                {
                    ListViewItem item = new ListViewItem((LV_rules.Items.Count + 1).ToString());

                    if (rulestr.Split('谶')[0] == "0")
                    {
                        item.Tag = "0";
                    }
                    else
                    {
                        item.Tag = "1";
                    }

                    item.SubItems.Add(rulestr.Split('谶')[1]);

                    item.SubItems.Add(rulestr.Split('谶')[2]);

                    LV_rules.Items.Add(item);
                    
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                try
                {
                    sr.Close();
                }
                catch (Exception)
                {

                }
            }
        }

        /// <summary>
        /// 添加或者删除柜子
        /// </summary>
        /// <param name="Isadd">是否是添加</param>
        public void func_AddOrDelrule(Boolean Isadd)
        {
            if (Isadd)
            {
                //检查是否已经存在该规则
                for (int i = 0; i < LV_rules.Items.Count; i++)
                {
                    if (txt_rule.Text == LV_rules.Items[i].SubItems[2].Text)
                    {
                        MessageBox.Show("规则已存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }

            StreamWriter sw = null;
            try
            {
                //如果不存在文件则创建一个
                if (!File.Exists(Configpath))
                {
                    File.Create(Configpath);
                }

                if (Isadd)
                {
                    //把要添加的规则加到listview里
                    ListViewItem item = new ListViewItem(LV_rules.Items.Count.ToString() + 1);
                    if (chk_disable.Checked)
                    {
                        item.Tag = "0";
                    }
                    else
                    {
                        item.Tag = "1";
                    }
                    item.SubItems.Add(txt_rule.Text);
                    item.SubItems.Add(txt_explain.Text);
                    LV_rules.Items.Add(item);
                }

                //把listview里面的写入到规则配置文件里
                sw = new StreamWriter(Configpath, false, Encoding.GetEncoding("GBK"));

                for (int i = 0; i < LV_rules.Items.Count; i++)
                {
                    String Str = "";

                    if (LV_rules.Items[i].Tag.ToString()=="0")
                    {
                        Str = "0谶";
                    }
                    else
                    {
                        Str = "1谶";
                    }

                    for (int s = 1; s < 3; s++)
                    {
                        Str += LV_rules.Items[i].SubItems[s].Text + "谶";
                    }
                    Str = Str.Remove(Str.LastIndexOf('谶'));
                    sw.WriteLine(Str);
                }
                if (Isadd)
                {
                    MessageBox.Show("添加成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
            }
            catch (Exception)
            {
                if (Isadd)
                {
                    MessageBox.Show("添加失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("删除失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            finally
            {
                try
                {
                    sw.Close();
                }
                catch (Exception)
                {

                }
            }
        }

        /// <summary>
        /// 修改规则
        /// </summary>
        public void func_updaterule()
        {
            StreamWriter sw = null;
            try
            {
                //如果不存在文件则创建一个
                if (!File.Exists(Configpath))
                {
                    File.Create(Configpath);
                }

                //先把ListView里面的规则修改好
                LV_rules.SelectedItems[0].SubItems[1].Text = txt_rule.Text;
                LV_rules.SelectedItems[0].SubItems[2].Text = txt_explain.Text;

                if (chk_disable.Checked)
                {
                    LV_rules.SelectedItems[0].Tag = "0";
                }
                else
                {
                    LV_rules.SelectedItems[0].Tag = "1";
                }
                

                //再把listview里面的写入到规则配置文件里
                sw = new StreamWriter(Configpath, false, Encoding.GetEncoding("GBK"));

                for (int i = 0; i < LV_rules.Items.Count; i++)
                {
                    String Str = "";

                    if (LV_rules.Items[i].Tag.ToString() == "0")
                    {
                        Str = "0谶";
                    }
                    else
                    {
                        Str = "1谶";
                    }

                    for (int s = 1; s < 3; s++)
                    {
                        Str += LV_rules.Items[i].SubItems[s].Text + "谶";
                    }
                    Str = Str.Remove(Str.LastIndexOf('谶'));
                    sw.WriteLine(Str);
                }
                MessageBox.Show("修改成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("修改失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                try
                {
                    sw.Close();
                }
                catch (Exception)
                {

                }
            }
        }


        private void F_SysConfig_Load(object sender, EventArgs e)
        {
            func_Getrule();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txt_rule.Text == "")
            {
                MessageBox.Show("请输入规则", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txt_explain.Text == "")
            {
                MessageBox.Show("请输入规则说明", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            func_AddOrDelrule(true);
            func_Getrule();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            func_updaterule();
        }

        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            func_Getrule();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LV_rules.SelectedItems.Count>0)
            {
                LV_rules.Items.Remove(LV_rules.SelectedItems[0]);
                func_AddOrDelrule(false);
            }
        }

        private void F_SysConfig_Resize(object sender, EventArgs e)
        {
            //随窗体大小改变Listview的列
            LV_rules.Columns[2].Width = LV_rules.Width - (LV_rules.Columns[0].Width + LV_rules.Columns[1].Width) - 23;
        }

        private void LV_rules_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LV_rules.SelectedItems.Count>0)
            {
                txt_rule.Text = LV_rules.SelectedItems[0].SubItems[1].Text;
                txt_explain.Text = LV_rules.SelectedItems[0].SubItems[2].Text;
                if (LV_rules.SelectedItems[0].Tag.ToString()=="0")
                {
                    chk_disable.Checked = true;
                }
                else
                {
                    chk_disable.Checked = false;
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start("http://www.cnseay.com/1105/"); 
            }
            catch (Exception)
            {

            }
        }
    }
}
