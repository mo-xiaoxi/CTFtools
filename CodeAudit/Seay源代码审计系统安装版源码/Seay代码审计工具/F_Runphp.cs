using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using ICSharpCode.TextEditor.Document;

namespace Seay代码审计工具
{
    public partial class F_Runphp : Form
    {
        public F_Runphp()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txt_phpcode.Text == "")
            {
                return;
            }
            try
            {
                StreamWriter sw = new StreamWriter(Application.StartupPath + "\\bin\\php.php");
                sw.Write(txt_phpcode.Text);
                sw.Close();

                string cmd = "cd bin&&php.exe php.php";
                Process seay = new Process();
                seay.StartInfo.FileName = "cmd.exe";
                seay.StartInfo.Arguments = "/c " + cmd;
                seay.StartInfo.UseShellExecute = false;
                seay.StartInfo.RedirectStandardInput = true;
                seay.StartInfo.RedirectStandardOutput = true;
                seay.StartInfo.RedirectStandardError = true;

                seay.StartInfo.CreateNoWindow = true;
                seay.Start();

                txt_result.Text = seay.StandardOutput.ReadToEnd();

            }
            catch (Exception)
            {
                
            }
        }


        private void F_Runphp_Load(object sender, EventArgs e)
        {
            txt_phpcode.Encoding = System.Text.Encoding.Default;
            txt_phpcode.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("PHP");
        }

        private void 复制选中ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txt_phpcode.ActiveTextAreaControl.SelectionManager.SelectedText != null && txt_phpcode.ActiveTextAreaControl.SelectionManager.SelectedText != "")
            {
                Clipboard.SetDataObject(txt_phpcode.ActiveTextAreaControl.SelectionManager.SelectedText);
            }
        }

        private void 复制结果ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(txt_result.SelectedText);
            }
            catch (Exception)
            {
            }
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txt_result.Paste();
        }

        private void 全选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txt_result.SelectAll();
        }

        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txt_result.SelectedText == String.Empty)
                return;
            else
                txt_result.Cut();
        }

        private void 清空ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txt_result.Clear();
        }
    }
}
