namespace Seay代码审计工具
{
    partial class F_tmpstr
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_tmpstr));
            this.txt_tmpstr = new ICSharpCode.TextEditor.TextEditorControl();
            this.cms_tmp = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.保存内容ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.另存文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.save_tmpfile = new System.Windows.Forms.SaveFileDialog();
            this.cms_tmp.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_tmpstr
            // 
            this.txt_tmpstr.ContextMenuStrip = this.cms_tmp;
            this.txt_tmpstr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_tmpstr.Encoding = ((System.Text.Encoding)(resources.GetObject("txt_tmpstr.Encoding")));
            this.txt_tmpstr.Location = new System.Drawing.Point(0, 0);
            this.txt_tmpstr.Name = "txt_tmpstr";
            this.txt_tmpstr.ShowEOLMarkers = true;
            this.txt_tmpstr.ShowSpaces = true;
            this.txt_tmpstr.ShowTabs = true;
            this.txt_tmpstr.ShowVRuler = true;
            this.txt_tmpstr.Size = new System.Drawing.Size(1049, 620);
            this.txt_tmpstr.TabIndex = 0;
            // 
            // cms_tmp
            // 
            this.cms_tmp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.保存内容ToolStripMenuItem,
            this.另存文件ToolStripMenuItem});
            this.cms_tmp.Name = "cms_tmp";
            this.cms_tmp.Size = new System.Drawing.Size(125, 48);
            // 
            // 保存内容ToolStripMenuItem
            // 
            this.保存内容ToolStripMenuItem.Name = "保存内容ToolStripMenuItem";
            this.保存内容ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.保存内容ToolStripMenuItem.Text = "保存内容";
            this.保存内容ToolStripMenuItem.Click += new System.EventHandler(this.保存内容ToolStripMenuItem_Click);
            // 
            // 另存文件ToolStripMenuItem
            // 
            this.另存文件ToolStripMenuItem.Name = "另存文件ToolStripMenuItem";
            this.另存文件ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.另存文件ToolStripMenuItem.Text = "另存文件";
            this.另存文件ToolStripMenuItem.Click += new System.EventHandler(this.另存文件ToolStripMenuItem_Click);
            // 
            // save_tmpfile
            // 
            this.save_tmpfile.DefaultExt = "txt";
            this.save_tmpfile.Filter = "txt|*.txt";
            this.save_tmpfile.Title = "保存记录";
            // 
            // F_tmpstr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1049, 620);
            this.ControlBox = false;
            this.Controls.Add(this.txt_tmpstr);
            this.KeyPreview = true;
            this.Name = "F_tmpstr";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.F_tmpstr_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.F_tmpstr_KeyDown);
            this.cms_tmp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ICSharpCode.TextEditor.TextEditorControl txt_tmpstr;
        private System.Windows.Forms.ContextMenuStrip cms_tmp;
        private System.Windows.Forms.ToolStripMenuItem 保存内容ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 另存文件ToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog save_tmpfile;
    }
}