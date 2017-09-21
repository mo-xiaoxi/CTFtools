namespace Seay代码审计工具
{
    partial class F_Runphp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_Runphp));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_phpcode = new ICSharpCode.TextEditor.TextEditorControl();
            this.cms_phpcode = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.复制选中ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_result = new System.Windows.Forms.RichTextBox();
            this.cms_runresult = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.复制结果ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.粘贴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全选ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.剪切ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.cms_phpcode.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.cms_runresult.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.txt_phpcode);
            this.groupBox1.Location = new System.Drawing.Point(1, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(903, 419);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PHP代码";
            // 
            // txt_phpcode
            // 
            this.txt_phpcode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txt_phpcode.ContextMenuStrip = this.cms_phpcode;
            this.txt_phpcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_phpcode.Encoding = ((System.Text.Encoding)(resources.GetObject("txt_phpcode.Encoding")));
            this.txt_phpcode.Location = new System.Drawing.Point(3, 17);
            this.txt_phpcode.Name = "txt_phpcode";
            this.txt_phpcode.ShowEOLMarkers = true;
            this.txt_phpcode.ShowSpaces = true;
            this.txt_phpcode.ShowTabs = true;
            this.txt_phpcode.ShowVRuler = true;
            this.txt_phpcode.Size = new System.Drawing.Size(897, 399);
            this.txt_phpcode.TabIndex = 6;
            // 
            // cms_phpcode
            // 
            this.cms_phpcode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.复制选中ToolStripMenuItem});
            this.cms_phpcode.Name = "cms_phpcode";
            this.cms_phpcode.Size = new System.Drawing.Size(99, 26);
            // 
            // 复制选中ToolStripMenuItem
            // 
            this.复制选中ToolStripMenuItem.Name = "复制选中ToolStripMenuItem";
            this.复制选中ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.复制选中ToolStripMenuItem.Text = "复制";
            this.复制选中ToolStripMenuItem.Click += new System.EventHandler(this.复制选中ToolStripMenuItem_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.txt_result);
            this.groupBox2.Location = new System.Drawing.Point(1, 443);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(903, 199);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "执行结果";
            // 
            // txt_result
            // 
            this.txt_result.ContextMenuStrip = this.cms_runresult;
            this.txt_result.DetectUrls = false;
            this.txt_result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_result.Location = new System.Drawing.Point(3, 17);
            this.txt_result.Name = "txt_result";
            this.txt_result.Size = new System.Drawing.Size(897, 179);
            this.txt_result.TabIndex = 0;
            this.txt_result.Text = "";
            // 
            // cms_runresult
            // 
            this.cms_runresult.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.复制结果ToolStripMenuItem,
            this.粘贴ToolStripMenuItem,
            this.全选ToolStripMenuItem,
            this.剪切ToolStripMenuItem,
            this.清空ToolStripMenuItem});
            this.cms_runresult.Name = "cms_runresult";
            this.cms_runresult.Size = new System.Drawing.Size(99, 114);
            // 
            // 复制结果ToolStripMenuItem
            // 
            this.复制结果ToolStripMenuItem.Name = "复制结果ToolStripMenuItem";
            this.复制结果ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.复制结果ToolStripMenuItem.Text = "复制";
            this.复制结果ToolStripMenuItem.Click += new System.EventHandler(this.复制结果ToolStripMenuItem_Click);
            // 
            // 粘贴ToolStripMenuItem
            // 
            this.粘贴ToolStripMenuItem.Name = "粘贴ToolStripMenuItem";
            this.粘贴ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.粘贴ToolStripMenuItem.Text = "粘贴";
            this.粘贴ToolStripMenuItem.Click += new System.EventHandler(this.粘贴ToolStripMenuItem_Click);
            // 
            // 全选ToolStripMenuItem
            // 
            this.全选ToolStripMenuItem.Name = "全选ToolStripMenuItem";
            this.全选ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.全选ToolStripMenuItem.Text = "全选";
            this.全选ToolStripMenuItem.Click += new System.EventHandler(this.全选ToolStripMenuItem_Click);
            // 
            // 剪切ToolStripMenuItem
            // 
            this.剪切ToolStripMenuItem.Name = "剪切ToolStripMenuItem";
            this.剪切ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.剪切ToolStripMenuItem.Text = "剪切";
            this.剪切ToolStripMenuItem.Click += new System.EventHandler(this.剪切ToolStripMenuItem_Click);
            // 
            // 清空ToolStripMenuItem
            // 
            this.清空ToolStripMenuItem.Name = "清空ToolStripMenuItem";
            this.清空ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.清空ToolStripMenuItem.Text = "清空";
            this.清空ToolStripMenuItem.Click += new System.EventHandler(this.清空ToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(820, 424);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "执 行";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // F_Runphp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 649);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "F_Runphp";
            this.Load += new System.EventHandler(this.F_Runphp_Load);
            this.groupBox1.ResumeLayout(false);
            this.cms_phpcode.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.cms_runresult.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button1;
        public ICSharpCode.TextEditor.TextEditorControl txt_phpcode;
        private System.Windows.Forms.RichTextBox txt_result;
        private System.Windows.Forms.ContextMenuStrip cms_phpcode;
        private System.Windows.Forms.ToolStripMenuItem 复制选中ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cms_runresult;
        private System.Windows.Forms.ToolStripMenuItem 复制结果ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 粘贴ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全选ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 剪切ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清空ToolStripMenuItem;

    }
}