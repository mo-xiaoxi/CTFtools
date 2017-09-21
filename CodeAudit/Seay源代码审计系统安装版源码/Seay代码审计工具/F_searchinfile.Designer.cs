namespace Seay代码审计工具
{
    partial class F_searchinfile
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbb_keyword = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lv_searchresult = new Seay代码审计工具.ListViewNF();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cms_filesearch = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.清空结果ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.复制路径ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chk_daxiaoxie = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_state = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.chk_reg = new System.Windows.Forms.CheckBox();
            this.cms_filesearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(40, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "内容(支持正则)：";
            // 
            // cmbb_keyword
            // 
            this.cmbb_keyword.FormattingEnabled = true;
            this.cmbb_keyword.Location = new System.Drawing.Point(138, 15);
            this.cmbb_keyword.Name = "cmbb_keyword";
            this.cmbb_keyword.Size = new System.Drawing.Size(273, 20);
            this.cmbb_keyword.TabIndex = 1;
            this.cmbb_keyword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbb_keyword_KeyPress);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(420, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = " 查 找 ";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lv_searchresult
            // 
            this.lv_searchresult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lv_searchresult.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lv_searchresult.ContextMenuStrip = this.cms_filesearch;
            this.lv_searchresult.FullRowSelect = true;
            this.lv_searchresult.Location = new System.Drawing.Point(2, 44);
            this.lv_searchresult.Name = "lv_searchresult";
            this.lv_searchresult.Size = new System.Drawing.Size(922, 503);
            this.lv_searchresult.TabIndex = 3;
            this.lv_searchresult.UseCompatibleStateImageBehavior = false;
            this.lv_searchresult.View = System.Windows.Forms.View.Details;
            this.lv_searchresult.DoubleClick += new System.EventHandler(this.lv_searchresult_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "文件路径";
            this.columnHeader2.Width = 280;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "内容详细";
            this.columnHeader3.Width = 550;
            // 
            // cms_filesearch
            // 
            this.cms_filesearch.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.清空结果ToolStripMenuItem,
            this.复制路径ToolStripMenuItem});
            this.cms_filesearch.Name = "cms_filesearch";
            this.cms_filesearch.Size = new System.Drawing.Size(123, 48);
            this.cms_filesearch.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cms_filesearch_ItemClicked);
            // 
            // 清空结果ToolStripMenuItem
            // 
            this.清空结果ToolStripMenuItem.Name = "清空结果ToolStripMenuItem";
            this.清空结果ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.清空结果ToolStripMenuItem.Text = "打开文件";
            // 
            // 复制路径ToolStripMenuItem
            // 
            this.复制路径ToolStripMenuItem.Name = "复制路径ToolStripMenuItem";
            this.复制路径ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.复制路径ToolStripMenuItem.Text = "复制路径";
            // 
            // chk_daxiaoxie
            // 
            this.chk_daxiaoxie.AutoSize = true;
            this.chk_daxiaoxie.BackColor = System.Drawing.Color.Transparent;
            this.chk_daxiaoxie.Location = new System.Drawing.Point(648, 19);
            this.chk_daxiaoxie.Name = "chk_daxiaoxie";
            this.chk_daxiaoxie.Size = new System.Drawing.Size(96, 16);
            this.chk_daxiaoxie.TabIndex = 5;
            this.chk_daxiaoxie.Text = "不区分大小写";
            this.chk_daxiaoxie.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(15, 552);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "状态：";
            // 
            // lbl_state
            // 
            this.lbl_state.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_state.AutoSize = true;
            this.lbl_state.BackColor = System.Drawing.Color.Transparent;
            this.lbl_state.Location = new System.Drawing.Point(49, 552);
            this.lbl_state.Name = "lbl_state";
            this.lbl_state.Size = new System.Drawing.Size(71, 12);
            this.lbl_state.TabIndex = 6;
            this.lbl_state.Text = "等待扫描...";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(501, 14);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = " 停 止";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // chk_reg
            // 
            this.chk_reg.AutoSize = true;
            this.chk_reg.BackColor = System.Drawing.Color.Transparent;
            this.chk_reg.Location = new System.Drawing.Point(594, 19);
            this.chk_reg.Name = "chk_reg";
            this.chk_reg.Size = new System.Drawing.Size(48, 16);
            this.chk_reg.TabIndex = 5;
            this.chk_reg.Text = "正则";
            this.chk_reg.UseVisualStyleBackColor = false;
            // 
            // F_searchinfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 570);
            this.ControlBox = false;
            this.Controls.Add(this.lbl_state);
            this.Controls.Add(this.chk_reg);
            this.Controls.Add(this.chk_daxiaoxie);
            this.Controls.Add(this.lv_searchresult);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmbb_keyword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "F_searchinfile";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.F_searchinfile_FormClosing);
            this.Load += new System.EventHandler(this.F_searchinfile_Load);
            this.Resize += new System.EventHandler(this.F_searchinfile_Resize);
            this.cms_filesearch.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private ListViewNF lv_searchresult;
        private System.Windows.Forms.CheckBox chk_daxiaoxie;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_state;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox chk_reg;
        private System.Windows.Forms.ContextMenuStrip cms_filesearch;
        private System.Windows.Forms.ToolStripMenuItem 清空结果ToolStripMenuItem;
        private System.Windows.Forms.ComboBox cmbb_keyword;
        private System.Windows.Forms.ToolStripMenuItem 复制路径ToolStripMenuItem;
    }
}