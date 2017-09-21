using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Net;
using Seay正则URL解码匹配测试工具;
using CSPluginKernel;
using System.Reflection;
using System.Collections;

namespace Seay代码审计工具
{
    public partial class F_Main : Form, CSPluginKernel.IfuncObject,CSPluginKernel.IvarObject
    {
        public F_Main()
        {
            InitializeComponent();
            //线程安全开关，关闭这个就能跨线程访问控件
            CheckForIllegalCrossThreadCalls = false;
            F_thisMain = this;
        }

        /// <summary>
        /// 新建项目选择的路径，放到这里方便全局调用
        /// </summary>
        public static string var_webpath { get; set; }

        /// <summary>
        /// 选择的文件编码
        /// </summary>
        public static string var_fileencoding { get; set; }

        public static F_Main F_thisMain { get; set; }

        public static TabControl Tab = null;

        private ArrayList plugins = new ArrayList();//定义一个存放“插件类实例”的动态数组
        private ArrayList piProperties = new ArrayList();//定义一个存放“信息属性”的动态数组

        
        private void F_Main_Load(object sender, EventArgs e)
        {
            cmbb_coding.SelectedIndex = 0;
            Tab = tab_main;

            //自载入定义编辑器
            toolshelper.func_loadeditplus();

            //版本，升级需要更改这里
            this.Tag = "2.1";
            label2.Text = "当前版本：V"+ this.Tag.ToString();

            //起一个线程来自动检测更新
            ParameterizedThreadStart pt = new ParameterizedThreadStart(func_update);
            Thread th_count = new Thread(pt);
            th_count.Start(true);
            
            try
            {
                //载入插件
                this.LoadAllPlugins();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            
        }


        /// <summary>
        /// 选择目录后载入文件列表
        /// </summary>
        /// <param name="WebRootFolder">目录路径</param>
        /// <param name="Root">节点</param>
        public void func_ViewLoadFile(string WebRootFolder, TreeNode Root)
        {
            DirectoryInfo RootFolder = new DirectoryInfo(WebRootFolder);

            //遍历主文件夹文件
            foreach (FileInfo RootFile in RootFolder.GetFiles())
            {
                try
                {
                    TreeNode aNode = new TreeNode(RootFile.Name);
                    aNode.Tag = RootFile.FullName;
                    Root.Nodes.Add(aNode);
                }
                catch (Exception)
                {
                    continue;
                }

            }

            //遍历子文件夹
            foreach (DirectoryInfo NextFolder in RootFolder.GetDirectories())
            {
                try
                {
                    TreeNode aNode = new TreeNode(NextFolder.Name);
                    //aNode.Tag = NextFolder.FullName;
                    Root.Nodes.Add(aNode);
                    func_ViewLoadFile(NextFolder.FullName, aNode);
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }


        /// <summary>
        /// 载入所选目录文件列表
        /// </summary>
        public void func_loadfiles()
        {
            if (fbd_dirBrowser.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string filepath = fbd_dirBrowser.SelectedPath;

                    var_webpath = filepath;

                    var_iwebpath = filepath;

                    tv_files.Nodes.Clear();

                    TreeNode WebRoot = new TreeNode(filepath.Substring(filepath.LastIndexOf('\\') + 1));

                    func_ViewLoadFile(filepath, WebRoot);
                    tv_files.Nodes.Add(WebRoot);
                    WebRoot.Expand();
                }
                catch (Exception)
                {

                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            func_loadfiles();
        }

        private void tv_files_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (tv_files.SelectedNode.Tag != null)
                {
                    if (File.Exists(tv_files.SelectedNode.Tag.ToString()))
                    {
                        string selectfile = tv_files.SelectedNode.Tag.ToString();

                        TabPage tpage = new TabPage(selectfile.Substring(selectfile.LastIndexOf('\\') + 1));
                        //tpage.MouseDoubleClick += new MouseEventHandler(tabPage_DoubleClick);
                        F_code form = new F_code();//动态创建一个窗体
                        form.FormBorderStyle = FormBorderStyle.None;//取消边框
                        form.TopLevel = false;
                        form.Dock = DockStyle.Fill;//控件边缘控制
                        form.var_filepath = selectfile;
                        tpage.Controls.Add(form);
                        tpage.Tag = selectfile;
                        tab_main.TabPages.Add(tpage);

                        form.Show();
                        tab_main.SelectedTab = tpage;

                        this.Text = "Seay源代码审计系统  " + selectfile;

                    }
                }
            }
            catch (Exception)
            {
            }

        }

        private void tab_main_DoubleClick(object sender, EventArgs e)
        {
            if (tab_main.SelectedTab.Text=="首页")
            {
                return;
            }
            if (tab_main.TabCount > 0)
            {
                tab_main.TabPages.Remove(tab_main.SelectedTab);
                tab_main.SelectedIndex = tab_main.TabPages.Count - 1;
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            tv_files.Nodes.Clear();
            foreach (TabPage item in tab_main.TabPages)
            {
                if (item.Text!="首页")
                {
                    tab_main.TabPages.Remove(item);
                }
            }

            this.Text = "Seay源代码审计系统  --www.cnseay.com";

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            try
            {
                TabPage tpage = new TabPage("函数查询");
                F_funcquery form = new F_funcquery();//动态创建一个窗体
                form.FormBorderStyle = FormBorderStyle.None;//取消边框
                form.TopLevel = false;
                form.Dock = DockStyle.Fill;//控件边缘控制
                tpage.Controls.Add(form);
                tab_main.TabPages.Add(tpage);

                form.Show();
                tab_main.SelectedTab = tpage;
            }
            catch (Exception)
            {
            }
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            try
            {
                TabPage tpage = new TabPage("代码调试");
                F_Runphp form = new F_Runphp();//动态创建一个窗体
                form.FormBorderStyle = FormBorderStyle.None;//取消边框
                form.TopLevel = false;
                form.txt_phpcode.Text = "<?php \r\n\r\n\r\n?>";
                form.Dock = DockStyle.Fill;//控件边缘控制
                tpage.Controls.Add(form);
                tab_main.TabPages.Add(tpage);

                form.Show();
                tab_main.SelectedTab = tpage;
            }
            catch (Exception)
            {
            }
        }

        private void 规则管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TabPage tpage = new TabPage("规则管理");
                F_SysConfig sysconfig = new F_SysConfig();//动态创建一个窗体
                sysconfig.FormBorderStyle = FormBorderStyle.None;//取消边框
                sysconfig.TopLevel = false;
                sysconfig.Dock = DockStyle.Fill;//控件边缘控制
                tpage.Controls.Add(sysconfig);
                tab_main.TabPages.Add(tpage);

                sysconfig.Show();
                tab_main.SelectedTab = tpage;
            }
            catch (Exception)
            {
            }
        }

        private void 编辑器配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TabPage tpage = new TabPage("编辑器配置");
                F_Editplus editplus = new F_Editplus();//动态创建一个窗体
                editplus.FormBorderStyle = FormBorderStyle.None;//取消边框
                editplus.TopLevel = false;
                editplus.Dock = DockStyle.Fill;//控件边缘控制
                tpage.Controls.Add(editplus);
                tab_main.TabPages.Add(tpage);

                editplus.Show();
                tab_main.SelectedTab = tpage;
            }
            catch (Exception)
            {
            }
        }
        F_audit audit = null;
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            try
            {
                TabPage tpage = new TabPage("自动审计");
                audit = new F_audit();//动态创建一个窗体
                audit.FormBorderStyle = FormBorderStyle.None;//取消边框
                audit.TopLevel = false;
                audit.var_model = "自动审计";
                audit.Dock = DockStyle.Fill;//控件边缘控制
                tpage.Controls.Add(audit);
                tab_main.TabPages.Add(tpage);

                audit.Show();
                tab_main.SelectedTab = tpage;
            }
            catch (Exception)
            {
            }
        }

        private void cmbb_coding_SelectedIndexChanged(object sender, EventArgs e)
        {
            var_fileencoding = cmbb_coding.Text;

            var_ifileencoding = var_fileencoding;
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            try
            {
                TabPage tpage = new TabPage("全局搜索");
                F_searchinfile searchinfile = new F_searchinfile();//动态创建一个窗体
                searchinfile.FormBorderStyle = FormBorderStyle.None;//取消边框
                searchinfile.TopLevel = false;
                searchinfile.Dock = DockStyle.Fill;//控件边缘控制
                tpage.Controls.Add(searchinfile);
                tab_main.TabPages.Add(tpage);

                searchinfile.Show();
                tab_main.SelectedTab = tpage;
            }
            catch (Exception)
            {
            }
        }

        private void F_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Application.ExitThread();
                Application.Exit();
            }
            catch (Exception)
            {
            }
        }

        private void 作者博客ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("http://www.cnseay.com/");
            }
            catch (Exception)
            {
            }

        }

        private void tab_main_Click(object sender, EventArgs e)
        {
            if (tab_main.SelectedTab != null)
            {
                if (audit!=null)
                {
                    audit.lv_buglist.Focus();
                }
                this.Text = "Seay源代码审计系统  " + tab_main.SelectedTab.Tag;
            }
        }

        /// <summary>
        /// 自动升级函数
        /// </summary>
        /// <param name="start">是否是软件启动</param>
        public void func_update(object start)
        {
            string ver = "";
            try
            {
                ver = toolshelper.Get_NewVerSion((bool)start);
                toolshelper.func_countuse();
            }
            catch (Exception)
            {
            }
            
            if (ver != "")
            {
                if ((bool)start)
                {
                    //判断是否为最新版
                    if (ver != this.Tag.ToString())
                    {
                        DialogResult dr = MessageBox.Show("最新版" + ver + "，是否升级", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dr == DialogResult.Yes)
                        {
                            Process.Start("upgrade.exe");
                        }
                    }
                }
                else
                {
                    //判断是否为最新版
                    if (ver == this.Tag.ToString())
                    {
                        MessageBox.Show("当前版本为最新版", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        DialogResult dr = MessageBox.Show("最新版" + ver + "，是否升级", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dr == DialogResult.Yes)
                        {
                            Process.Start("upgrade.exe");
                        }
                    }
                }
            }
            try
            {
                Thread.CurrentThread.Abort();
            }
            catch (Exception)
            {
            }
        }

        private void 升级检测ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ParameterizedThreadStart pt = new ParameterizedThreadStart(func_update);
            Thread th_count = new Thread(pt);
            th_count.Start(false);
        }

        private void 关于系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TabPage item in tab_main.TabPages)
            {
                if (item.Text == "首页")
                {
                    tab_main.SelectedTab = item;
                    return;
                }
            }
        }

        private void 使用帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("http://www.cnseay.com/2951/");
            }
            catch (Exception)
            {

            }

        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < tab_main.TabPages.Count; i++)
            {
                if (tab_main.TabPages[i].Text == "临时记录")
                {
                    tab_main.SelectedIndex = i;
                    return;
                }
            }

            try
            {
                TabPage tpage = new TabPage("临时记录");
                F_tmpstr form = new F_tmpstr();//动态创建一个窗体
                form.FormBorderStyle = FormBorderStyle.None;//取消边框
                form.TopLevel = false;
                form.Dock = DockStyle.Fill;//控件边缘控制
                tpage.Controls.Add(form);
                tab_main.TabPages.Add(tpage);

                form.Show();
                tab_main.SelectedTab = tpage;
            }
            catch (Exception)
            {
            }
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            if (File.Exists(Application.StartupPath+"\\bin\\mysql\\mysql.exe"))
            {
                try
                {
                    Process.Start(Application.StartupPath + "\\bin\\mysql\\mysql.exe");
                }
                catch (Exception)
                {
                    MessageBox.Show("数据库管理系统启动失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {

            try
            {
                TabPage tpage = new TabPage("正则编码");
                F_rule_encode form = new F_rule_encode();//动态创建一个窗体
                form.FormBorderStyle = FormBorderStyle.None;//取消边框
                form.TopLevel = false;
                form.Dock = DockStyle.Fill;//控件边缘控制
                tpage.Controls.Add(form);
                tab_main.TabPages.Add(tpage);

                form.Show();
                tab_main.SelectedTab = tpage;
            }
            catch (Exception)
            {
            }
        }

        private void 路径泄露审计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TabPage tpage = new TabPage("信息泄露审计");
                F_audit audit = new F_audit();//动态创建一个窗体
                audit.FormBorderStyle = FormBorderStyle.None;//取消边框
                audit.TopLevel = false;
                audit.var_model = "信息泄露审计";
                audit.cmbb_url.Visible = true;
                audit.label2.Visible = true;
                audit.txt_cookie.Visible = true;
                audit.label3.Visible = true;
                audit.Dock = DockStyle.Fill;//控件边缘控制
                tpage.Controls.Add(audit);
                tab_main.TabPages.Add(tpage);

                audit.Show();
                tab_main.SelectedTab = tpage;
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 加载插件
        /// </summary>
        private void LoadAllPlugins()
        {
            string[] files = Directory.GetFiles(Application.StartupPath + @"\plugins\");
            int i = 0;
            PluginInfoAttribute typeAttribute = new PluginInfoAttribute();
            foreach (string file in files)//遍历指定路径下的文件名
            {
                string ext = file.Substring(file.LastIndexOf("."));//检索文件类型
                if (ext != ".dll") //查找.DLL文件(筛选1)
                    continue;//如果文件类型非.DLL执行下一轮循环
                try
                {
                    Assembly tmp = Assembly.LoadFile(file);//载入DLL，并实例化反射对象
                    Type[] types = tmp.GetTypes();//获取模块里面的类

                    foreach (Type t in types)//遍历模块里面的类
                    {
                        if (IsValidPlugin(t))//如果包含接口"CSPluginKernel.IPlugin"，说明插件适配与该主程序(筛选2)
                        {
                            plugins.Add(tmp.CreateInstance(t.FullName));//将类实例化并添加到plugins动态数组里。
                            object[] attbs = t.GetCustomAttributes(typeAttribute.GetType(), false);//获取程序集“信息属性”，版本，作者等。
                            PluginInfoAttribute attribute = null;//定义信息属性对象，并指为空
                            foreach (object attb in attbs)//遍历信息属性集合
                            {
                                PluginInfoAttribute temP = attb as PluginInfoAttribute;//强制类型转换
                                if (temP != null)
                                {
                                    attribute = temP;//这里temP等价于(PluginInfoAttribute)attb，
                                    //另外在这时为对象初始化，attribute将不为null
                                    attribute.Index = i;//为Index属性赋值
                                    i++;
                                    break;
                                }
                            }
                            //如果读到“信息属性”，并实例化“信息属性”对象
                            if (attribute != null)
                                this.piProperties.Add(attribute);//将信息属性对象添加到piProperties动态数组
                            else
                                throw new Exception("未定义插件属性");//抛出异常
                        }
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
            //遍历“信息属性”动态数组piProperties
            foreach (PluginInfoAttribute pia in piProperties)
            {
                ToolStripMenuItem ts = new ToolStripMenuItem(pia.Name + pia.Version);
                ts.Click += new EventHandler(RunPlugin);//为该菜单项注册单击事件

                toolStripDropDownButton1.DropDownItems.Add(ts);
                pia.Tag = ts;//将增加的信息菜单项添加到pia的附加信息里
            }
            //遍历“插件对象”动态数组
            foreach (IPlugin pi in plugins)
            {　 //插件与主程序进行连接，如果成功则调用插件的OnLoad方法
                if (pi.Connect((IfuncObject)this, (IvarObject)this))
                {
                    pi.OnLoad();
                }
                else
                {
                    MessageBox.Show("Can not connect plugin!");
                }
            }
        }

        ///判断模块的类是否满足预定义接口
        private bool IsValidPlugin(Type t)
        {
            bool ret = false;
            Type[] interfaces = t.GetInterfaces();
            foreach (Type theInterface in interfaces)
            {
                if (theInterface.FullName == "CSPluginKernel.IPlugin")
                {
                    ret = true;
                    break;
                }
            }
            return ret;
        }

        //插件菜单项点击事件
        private void RunPlugin(object sender, EventArgs e)
        {　 //遍历信息属性集　
            foreach (PluginInfoAttribute pia in piProperties)
                if (pia.Tag.Equals(sender))//如果是单击了该菜单项(sender表示事件的发送者，这里代表插件启动菜单项)
                    ((IPlugin)plugins[pia.Index]).Run();//加载运行插件
        }

        #region IfuncObject 成员

        /// <summary>
        /// 更改窗体标题的接口
        /// </summary>
        /// <param name="test"></param>
        public void func_I_changtext(string test)
        {
            this.Text = test;
        }

        public void func_I_addtab(object obj, string title)
        {
            try
            {
                Control form = obj as Control;
                TabPage tpage = new TabPage(title);
                tpage.Controls.Add(form);
                tab_main.TabPages.Add(tpage);

                form.Show();
                tab_main.SelectedTab = tpage;
            }
            catch (Exception)
            {
            }
        }

        #endregion

        #region IvarObject成员

        public string var_iwebpath { get; set; }

        public string var_ifileencoding { get; set; }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            if (txt_enword.Text == "")
            {
                MessageBox.Show("请输入要翻译的单词或语句", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                txt_cnword.Text = toolshelper.func_queryen(txt_enword.Text.Trim());
            }
            catch (Exception)
            {
                MessageBox.Show("翻译失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txt_enword_Enter(object sender, EventArgs e)
        {
            txt_enword.Multiline = true;
            txt_enword.Height = 200;
        }

        private void txt_enword_Leave(object sender, EventArgs e)
        {
            txt_enword.Multiline = false;
        }

        private void txt_cnword_Enter(object sender, EventArgs e)
        {
            txt_cnword.Multiline = true;
            txt_cnword.Height = 200;
        }

        private void txt_cnword_Leave(object sender, EventArgs e)
        {
            txt_cnword.Multiline = false;
        }        
    }
}
