using System;
using System.Collections.Generic;
using System.Text;
using CSPluginKernel;
using System.Windows.Forms;//引用核心接口命名空间,用来实现各种接口

namespace testplus
{

    /// 构造插件信息属性实例
    [PluginInfo("测试插件", "1.0", "作者", "http://www.cnseay.com/", true)]

    public class Class1 : IPlugin//实现插件公共接口
    {
		#region IPlugin 成员

        public bool Connect(IfuncObject app,IvarObject var) 
        {
            try
            {
                i_func = app;
                i_attribute = var;
                return true;
            }
            catch
            {
                return false;
            }
		}

		public void OnLoad() 
        {

		}

		public void OnDestory() {}

		#endregion

		private IfuncObject i_func;
        private IvarObject i_attribute;

        public void Run()
        {
            F_test ft = new F_test(i_func, i_attribute);
            ft.FormBorderStyle = FormBorderStyle.None;//取消边框
            ft.TopLevel = false;
            ft.Dock = DockStyle.Fill;//控件边缘控制
            i_func.func_I_addtab(ft, "测试插件添加tab");
            
        }

    }
}
