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
    public partial class F_Editplus : Form
    {
        public F_Editplus()
        {
            InitializeComponent();
        }

        string configpath = Application.StartupPath + "\\config\\editor.bin";

        private void F_Editplus_Resize(object sender, EventArgs e)
        {
            //随窗体大小改变Listview的列
            LV_Editplus.Columns[2].Width = LV_Editplus.Width - (LV_Editplus.Columns[0].Width + LV_Editplus.Columns[1].Width) - 23;
        }

         /// <summary>
        /// 遍历编辑器配置文件
        /// </summary>
        public void func_Geteditplus()
        {
            if (!File.Exists(configpath))
            {
                return;
            }
           
            if (LV_Editplus.Items.Count > 0)
            {
                LV_Editplus.Items.Clear();
            }

            if (true)
            {
                
            }

            StreamReader sr = null;

            try
            {
                string Str_Editplusname = "";
                string Str_EditPath = "";

                sr = new StreamReader(configpath, Encoding.GetEncoding("GBK"));
                while ((Str_Editplusname = sr.ReadLine()) != null)
                {
                    Str_EditPath = Str_Editplusname.Substring((Str_Editplusname.IndexOf('=')) + 1);
                    Str_Editplusname = Str_Editplusname.Remove(Str_Editplusname.IndexOf('='));

                    string Id = (LV_Editplus.Items.Count + 1).ToString();

                    ListViewItem item = new ListViewItem(Id);

                    item.SubItems.Add(Str_Editplusname);

                    item.SubItems.Add(Str_EditPath);

                    LV_Editplus.Items.Add(item);
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
                    throw;
                }
            }
        }


        public void func_AddOrDeledit(Boolean Isadd)
        {
            if (Isadd)
            {
                //检查正则表达式中是否已经存在该表达式
                for (int i = 0; i < LV_Editplus.Items.Count; i++)
                {
                    if (txt_plusPath.Text == LV_Editplus.Items[i].SubItems[2].Text)
                    {
                        MessageBox.Show("编辑器已存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }

            StreamWriter sw = null;
            try
            {
                //如果不存在文件则创建一个
                if (!File.Exists(configpath))
                {
                    File.Create(configpath);
                }

                if (Isadd)
                {
                    //把要添加的表达式加到listview里
                    ListViewItem item = new ListViewItem(LV_Editplus.Items.Count.ToString() + 1);
                    item.SubItems.Add(txt_plusName.Text);
                    item.SubItems.Add(txt_plusPath.Text);
                    LV_Editplus.Items.Add(item);
                }

                //把listview里面的写入到编辑器配置文件里
                sw = new StreamWriter(configpath, false, Encoding.GetEncoding("GBK"));

                for (int i = 0; i < LV_Editplus.Items.Count; i++)
                {
                    String Str = "";
                    for (int s = 1; s < 3; s++)
                    {
                        Str += LV_Editplus.Items[i].SubItems[s].Text + "=";
                    }
                    Str = Str.Remove(Str.LastIndexOf('='));
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
        /// 删除编辑器
        /// </summary>
        public void func_updateedit()
        {
            StreamWriter sw = null;
            try
            {
                //如果不存在文件则创建一个
                if (!File.Exists(configpath))
                {
                    File.Create(configpath);
                }

                //先把ListView里面的表达式修改好
                LV_Editplus.SelectedItems[0].SubItems[1].Text = txt_plusName.Text;
                LV_Editplus.SelectedItems[0].SubItems[2].Text = txt_plusPath.Text;
                //再把listview里面的写入到表达式配置文件里
                sw = new StreamWriter(configpath, false, Encoding.GetEncoding("GBK"));

                for (int i = 0; i < LV_Editplus.Items.Count; i++)
                {
                    String Str = "";
                    for (int s = 1; s < 3; s++)
                    {
                        Str += LV_Editplus.Items[i].SubItems[s].Text + "=";
                    }
                    Str = Str.Remove(Str.LastIndexOf('='));
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

        private void F_Editplus_Load(object sender, EventArgs e)
        {
            func_Geteditplus();
        }

        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            func_Geteditplus();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LV_Editplus.SelectedItems.Count > 0)
            {
                LV_Editplus.Items.Remove(LV_Editplus.SelectedItems[0]);
                func_AddOrDeledit(false);
            }
            toolshelper.func_loadeditplus();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txt_plusName.Text == "")
            {
                MessageBox.Show("请输入编辑器名称", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txt_plusPath.Text == "")
            {
                MessageBox.Show("请输入编辑器路径", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            func_AddOrDeledit(true);
            func_Geteditplus();
            toolshelper.func_loadeditplus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            func_updateedit();
            toolshelper.func_loadeditplus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (ofd_selectfile.ShowDialog()==DialogResult.OK)
            {
                txt_plusPath.Text = ofd_selectfile.FileName;
            }
        }

        private void txt_plusPath_DoubleClick(object sender, EventArgs e)
        {
            if (ofd_selectfile.ShowDialog() == DialogResult.OK)
            {
                txt_plusPath.Text = ofd_selectfile.FileName;
            }
        }

        private void LV_Editplus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LV_Editplus.SelectedItems.Count>0)
            {
                txt_plusName.Text = LV_Editplus.SelectedItems[0].SubItems[1].Text;
                txt_plusPath.Text = LV_Editplus.SelectedItems[0].SubItems[2].Text;
            }
        }
    }
}
