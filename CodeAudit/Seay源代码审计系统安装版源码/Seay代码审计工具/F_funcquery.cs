using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Seay代码审计工具
{
    public partial class F_funcquery : Form
    {
        public F_funcquery()
        {
            InitializeComponent();
        }

        public string funcname { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbb_func.Text=="")
            {
                MessageBox.Show("请输入要查询的函数","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            lbl_state.Text = "正在使出吃奶的力气加载...";
            wb_Browser.Navigate("http://www.php.net/manual/zh/function."+cmbb_func.Text.Trim().Replace("_","-")+".php");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            wb_Browser.GoBack();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            wb_Browser.GoForward();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            lbl_state.Text = "正在使出吃奶的力气加载...";
            wb_Browser.Refresh();
        }

        private void F_funcquery_Load(object sender, EventArgs e)
        {
            if (funcname!=null&&funcname!="")
            {
                wb_Browser.Navigate("http://www.php.net/manual/zh/function." + funcname + ".php");
                lbl_state.Text = "正在使出吃奶的力气加载...";
            }
        }

        private void wb_Browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            lbl_state.Text = "";
        }

        private void cmbb_func_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==13)
            {
                if (cmbb_func.Text == "")
                {
                    MessageBox.Show("请输入要查询的函数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                lbl_state.Text = "正在使出吃奶的力气加载...";
                wb_Browser.Navigate("http://www.php.net/manual/zh/function." + cmbb_func.Text.Trim().Replace("_", "-") + ".php");
            }
        }
    }
}
