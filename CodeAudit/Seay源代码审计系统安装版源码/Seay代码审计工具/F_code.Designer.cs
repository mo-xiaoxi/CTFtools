namespace Seay代码审计工具
{
    partial class F_code
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_code));
            this.cms_codems = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.字符追踪ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查询函数ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.定位函数ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全局搜索ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.调试选中ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.复制选中ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.英汉翻译ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.复制路径ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_search = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lbox_var = new System.Windows.Forms.ListBox();
            this.txt_code = new ICSharpCode.TextEditor.TextEditorControl();
            this.lbox_varlist = new System.Windows.Forms.ListBox();
            this.cms_codems.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cms_codems
            // 
            this.cms_codems.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.字符追踪ToolStripMenuItem,
            this.查询函数ToolStripMenuItem,
            this.定位函数ToolStripMenuItem,
            this.全局搜索ToolStripMenuItem,
            this.调试选中ToolStripMenuItem,
            this.复制选中ToolStripMenuItem,
            this.英汉翻译ToolStripMenuItem,
            this.保存文件ToolStripMenuItem,
            this.复制路径ToolStripMenuItem});
            this.cms_codems.Name = "contextMenuStrip1";
            this.cms_codems.Size = new System.Drawing.Size(125, 202);
            this.cms_codems.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // 字符追踪ToolStripMenuItem
            // 
            this.字符追踪ToolStripMenuItem.Name = "字符追踪ToolStripMenuItem";
            this.字符追踪ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.字符追踪ToolStripMenuItem.Text = "全文追踪";
            this.字符追踪ToolStripMenuItem.Click += new System.EventHandler(this.字符追踪ToolStripMenuItem_Click);
            // 
            // 查询函数ToolStripMenuItem
            // 
            this.查询函数ToolStripMenuItem.Name = "查询函数ToolStripMenuItem";
            this.查询函数ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.查询函数ToolStripMenuItem.Text = "查询函数";
            this.查询函数ToolStripMenuItem.Click += new System.EventHandler(this.查询函数ToolStripMenuItem_Click);
            // 
            // 定位函数ToolStripMenuItem
            // 
            this.定位函数ToolStripMenuItem.Name = "定位函数ToolStripMenuItem";
            this.定位函数ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.定位函数ToolStripMenuItem.Text = "定位函数";
            this.定位函数ToolStripMenuItem.Click += new System.EventHandler(this.定位函数ToolStripMenuItem_Click);
            // 
            // 全局搜索ToolStripMenuItem
            // 
            this.全局搜索ToolStripMenuItem.Name = "全局搜索ToolStripMenuItem";
            this.全局搜索ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.全局搜索ToolStripMenuItem.Text = "全局搜索";
            this.全局搜索ToolStripMenuItem.Click += new System.EventHandler(this.全局搜索ToolStripMenuItem_Click);
            // 
            // 调试选中ToolStripMenuItem
            // 
            this.调试选中ToolStripMenuItem.Name = "调试选中ToolStripMenuItem";
            this.调试选中ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.调试选中ToolStripMenuItem.Text = "调试选中";
            this.调试选中ToolStripMenuItem.Click += new System.EventHandler(this.调试选中ToolStripMenuItem_Click);
            // 
            // 复制选中ToolStripMenuItem
            // 
            this.复制选中ToolStripMenuItem.Name = "复制选中ToolStripMenuItem";
            this.复制选中ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.复制选中ToolStripMenuItem.Text = "复制选中";
            this.复制选中ToolStripMenuItem.Click += new System.EventHandler(this.复制选中ToolStripMenuItem_Click);
            // 
            // 英汉翻译ToolStripMenuItem
            // 
            this.英汉翻译ToolStripMenuItem.Name = "英汉翻译ToolStripMenuItem";
            this.英汉翻译ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.英汉翻译ToolStripMenuItem.Text = "英汉互译";
            this.英汉翻译ToolStripMenuItem.Click += new System.EventHandler(this.英汉翻译ToolStripMenuItem_Click);
            // 
            // 保存文件ToolStripMenuItem
            // 
            this.保存文件ToolStripMenuItem.Name = "保存文件ToolStripMenuItem";
            this.保存文件ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.保存文件ToolStripMenuItem.Text = "保存文件";
            this.保存文件ToolStripMenuItem.Click += new System.EventHandler(this.保存文件ToolStripMenuItem_Click);
            // 
            // 复制路径ToolStripMenuItem
            // 
            this.复制路径ToolStripMenuItem.Name = "复制路径ToolStripMenuItem";
            this.复制路径ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.复制路径ToolStripMenuItem.Text = "复制路径";
            this.复制路径ToolStripMenuItem.Click += new System.EventHandler(this.复制路径ToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.lbox_var);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txt_code);
            this.splitContainer1.Panel2.Controls.Add(this.lbox_varlist);
            this.splitContainer1.Size = new System.Drawing.Size(942, 554);
            this.splitContainer1.SplitterDistance = 191;
            this.splitContainer1.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.txt_search);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(181, 73);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "文字查找";
            // 
            // txt_search
            // 
            this.txt_search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_search.Location = new System.Drawing.Point(6, 20);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(172, 21);
            this.txt_search.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(6, 44);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(169, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = " 查 找 ";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbox_var
            // 
            this.lbox_var.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbox_var.BackColor = System.Drawing.SystemColors.Control;
            this.lbox_var.FormattingEnabled = true;
            this.lbox_var.ItemHeight = 12;
            this.lbox_var.Location = new System.Drawing.Point(0, 80);
            this.lbox_var.Name = "lbox_var";
            this.lbox_var.Size = new System.Drawing.Size(181, 472);
            this.lbox_var.TabIndex = 4;
            this.lbox_var.SelectedIndexChanged += new System.EventHandler(this.lbox_var_SelectedIndexChanged);
            this.lbox_var.DoubleClick += new System.EventHandler(this.lbox_var_DoubleClick);
            // 
            // txt_code
            // 
            this.txt_code.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_code.ContextMenuStrip = this.cms_codems;
            this.txt_code.Encoding = ((System.Text.Encoding)(resources.GetObject("txt_code.Encoding")));
            this.txt_code.LineViewerStyle = ICSharpCode.TextEditor.Document.LineViewerStyle.FullRow;
            this.txt_code.Location = new System.Drawing.Point(3, 3);
            this.txt_code.Name = "txt_code";
            this.txt_code.ShowEOLMarkers = true;
            this.txt_code.ShowSpaces = true;
            this.txt_code.ShowTabs = true;
            this.txt_code.ShowVRuler = true;
            this.txt_code.Size = new System.Drawing.Size(741, 439);
            this.txt_code.TabIndex = 5;
            // 
            // lbox_varlist
            // 
            this.lbox_varlist.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbox_varlist.BackColor = System.Drawing.SystemColors.Control;
            this.lbox_varlist.FormattingEnabled = true;
            this.lbox_varlist.ItemHeight = 12;
            this.lbox_varlist.Location = new System.Drawing.Point(3, 448);
            this.lbox_varlist.Name = "lbox_varlist";
            this.lbox_varlist.Size = new System.Drawing.Size(741, 100);
            this.lbox_varlist.TabIndex = 4;
            this.lbox_varlist.DoubleClick += new System.EventHandler(this.lbox_varlist_DoubleClick);
            // 
            // F_code
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 554);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer1);
            this.KeyPreview = true;
            this.Name = "F_code";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Activated += new System.EventHandler(this.F_code_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.F_code_FormClosing);
            this.Load += new System.EventHandler(this.F_code_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.F_code_KeyDown);
            this.cms_codems.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip cms_codems;
        private System.Windows.Forms.ToolStripMenuItem 查询函数ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 调试选中ToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem 复制选中ToolStripMenuItem;
        private System.Windows.Forms.ListBox lbox_var;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lbox_varlist;
        private ICSharpCode.TextEditor.TextEditorControl txt_code;
        public System.Windows.Forms.TextBox txt_search;
        private System.Windows.Forms.ToolStripMenuItem 全局搜索ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 定位函数ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 英汉翻译ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 字符追踪ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 复制路径ToolStripMenuItem;

    }
}