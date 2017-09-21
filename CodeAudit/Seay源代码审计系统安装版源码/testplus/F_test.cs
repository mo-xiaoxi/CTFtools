using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CSPluginKernel;//引用核心接口命名空间,用来实现各种接口

namespace testplus
{
    public partial class F_test : Form
    {
        public F_test(IfuncObject app,IvarObject var)
        {
            i_func = app;
            i_attribute = var;
            InitializeComponent();
        }


        private IfuncObject i_func;
        private IvarObject i_attribute;

        private void button1_Click(object sender, EventArgs e)
        {
            i_func.func_I_changtext("测试标题");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            F_test ft = new F_test(i_func,i_attribute);
            ft.FormBorderStyle = FormBorderStyle.None;//取消边框
            ft.TopLevel = false;
            ft.Dock = DockStyle.Fill;//控件边缘控制
            i_func.func_I_addtab(ft, "测试插件添加tab");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(i_attribute.var_ifileencoding);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show(i_attribute.var_iwebpath);
        }

        

    }
}
