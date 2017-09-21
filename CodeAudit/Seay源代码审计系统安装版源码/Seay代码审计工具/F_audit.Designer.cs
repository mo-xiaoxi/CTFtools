namespace Seay代码审计工具
{
    partial class F_audit
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.cms_father = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.打开文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.复制路径ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.生成报表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_state = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.sfd_report = new System.Windows.Forms.SaveFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbb_url = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.txt_cookie = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pgb_scan = new System.Windows.Forms.ProgressBar();
            this.lv_buglist = new Seay代码审计工具.ListViewNF();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cms_father.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = " 开 始 ";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(93, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = " 停 止 ";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cms_father
            // 
            this.cms_father.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开文件ToolStripMenuItem,
            this.复制路径ToolStripMenuItem,
            this.生成报表ToolStripMenuItem});
            this.cms_father.Name = "cms_father";
            this.cms_father.Size = new System.Drawing.Size(125, 70);
            this.cms_father.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.comm_editplus_ItemClicked);
            // 
            // 打开文件ToolStripMenuItem
            // 
            this.打开文件ToolStripMenuItem.Name = "打开文件ToolStripMenuItem";
            this.打开文件ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.打开文件ToolStripMenuItem.Text = "打开文件";
            // 
            // 复制路径ToolStripMenuItem
            // 
            this.复制路径ToolStripMenuItem.Name = "复制路径ToolStripMenuItem";
            this.复制路径ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.复制路径ToolStripMenuItem.Text = "复制路径";
            // 
            // 生成报表ToolStripMenuItem
            // 
            this.生成报表ToolStripMenuItem.Name = "生成报表ToolStripMenuItem";
            this.生成报表ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.生成报表ToolStripMenuItem.Text = "生成报告";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(279, 556);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "状态：";
            // 
            // lbl_state
            // 
            this.lbl_state.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_state.AutoSize = true;
            this.lbl_state.BackColor = System.Drawing.Color.Transparent;
            this.lbl_state.Location = new System.Drawing.Point(312, 556);
            this.lbl_state.Name = "lbl_state";
            this.lbl_state.Size = new System.Drawing.Size(71, 12);
            this.lbl_state.TabIndex = 2;
            this.lbl_state.Text = "等待扫描...";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(174, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 0;
            this.button3.Text = "生成报告";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // sfd_report
            // 
            this.sfd_report.DefaultExt = "html";
            this.sfd_report.Filter = "html|*.html";
            this.sfd_report.Title = "生成报表";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(271, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "站点地址：";
            this.label2.Visible = false;
            // 
            // cmbb_url
            // 
            this.cmbb_url.FormattingEnabled = true;
            this.cmbb_url.Location = new System.Drawing.Point(334, 14);
            this.cmbb_url.Name = "cmbb_url";
            this.cmbb_url.Size = new System.Drawing.Size(269, 20);
            this.cmbb_url.TabIndex = 4;
            this.cmbb_url.Text = "http://localhost/";
            this.cmbb_url.Visible = false;
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 200;
            // 
            // txt_cookie
            // 
            this.txt_cookie.Location = new System.Drawing.Point(663, 13);
            this.txt_cookie.Name = "txt_cookie";
            this.txt_cookie.Size = new System.Drawing.Size(314, 21);
            this.txt_cookie.TabIndex = 14;
            this.txt_cookie.Visible = false;
            this.txt_cookie.Enter += new System.EventHandler(this.txt_cookie_Enter);
            this.txt_cookie.Leave += new System.EventHandler(this.txt_cookie_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(616, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "Cookie：";
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(12, 556);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "进度：";
            // 
            // pgb_scan
            // 
            this.pgb_scan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pgb_scan.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.pgb_scan.Location = new System.Drawing.Point(49, 556);
            this.pgb_scan.Maximum = 1000;
            this.pgb_scan.Name = "pgb_scan";
            this.pgb_scan.Size = new System.Drawing.Size(220, 13);
            this.pgb_scan.Step = 1;
            this.pgb_scan.TabIndex = 15;
            // 
            // lv_buglist
            // 
            this.lv_buglist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lv_buglist.BackColor = System.Drawing.Color.White;
            this.lv_buglist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader3,
            this.columnHeader5,
            this.columnHeader4});
            this.lv_buglist.ContextMenuStrip = this.cms_father;
            this.lv_buglist.FullRowSelect = true;
            this.lv_buglist.Location = new System.Drawing.Point(1, 41);
            this.lv_buglist.Name = "lv_buglist";
            this.lv_buglist.Size = new System.Drawing.Size(1010, 509);
            this.lv_buglist.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lv_buglist.TabIndex = 1;
            this.lv_buglist.UseCompatibleStateImageBehavior = false;
            this.lv_buglist.View = System.Windows.Forms.View.Details;
            this.lv_buglist.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lv_buglist_ColumnClick);
            this.lv_buglist.DoubleClick += new System.EventHandler(this.lv_buglist_DoubleClick);
            this.lv_buglist.MouseHover += new System.EventHandler(this.lv_buglist_MouseHover);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            this.columnHeader1.Width = 50;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "漏洞描述";
            this.columnHeader3.Width = 350;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "文件路径";
            this.columnHeader5.Width = 270;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "漏洞详细";
            this.columnHeader4.Width = 320;
            // 
            // F_audit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1013, 572);
            this.ControlBox = false;
            this.Controls.Add(this.pgb_scan);
            this.Controls.Add(this.txt_cookie);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbb_url);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_state);
            this.Controls.Add(this.lv_buglist);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Name = "F_audit";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.F_audit_FormClosing);
            this.Load += new System.EventHandler(this.F_audit_Load);
            this.Resize += new System.EventHandler(this.F_audit_Resize);
            this.cms_father.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_state;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        public System.Windows.Forms.ContextMenuStrip cms_father;
        private System.Windows.Forms.ToolStripMenuItem 打开文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 生成报表ToolStripMenuItem;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.SaveFileDialog sfd_report;
        private System.Windows.Forms.ToolStripMenuItem 复制路径ToolStripMenuItem;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox cmbb_url;
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.TextBox txt_cookie;
        public System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ProgressBar pgb_scan;
        public ListViewNF lv_buglist;
    }
}