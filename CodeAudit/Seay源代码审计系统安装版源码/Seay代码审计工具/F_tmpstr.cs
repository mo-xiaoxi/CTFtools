using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Seay代码审计工具
{
    public partial class F_tmpstr : Form
    {
        public F_tmpstr()
        {
            InitializeComponent();
        }

        string var_tmpfile = Application.StartupPath + "\\config\\tmp.txt";

        public void func_loadtmpfile()
        {
            try
            {
                if (File.Exists(var_tmpfile))
                {
                    txt_tmpstr.Text = File.ReadAllText(var_tmpfile);
                }
            }
            catch (Exception)
            {
            }
            
        }

        private void F_tmpstr_Load(object sender, EventArgs e)
        {
            func_loadtmpfile();
        }

        private void 保存内容ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllText(var_tmpfile, txt_tmpstr.Text, Encoding.GetEncoding("UTF-8"));
            }
            catch (Exception)
            {
                MessageBox.Show("保存失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void 另存文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (save_tmpfile.ShowDialog()==DialogResult.OK)
            {
                try
                {
                    File.WriteAllText(save_tmpfile.FileName, txt_tmpstr.Text, Encoding.GetEncoding("UTF-8"));
                    MessageBox.Show("另存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("另存失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void F_tmpstr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                try
                {
                    File.WriteAllText(var_tmpfile, txt_tmpstr.Text, Encoding.GetEncoding("UTF-8"));
                }
                catch (Exception)
                {
                    MessageBox.Show("保存失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
