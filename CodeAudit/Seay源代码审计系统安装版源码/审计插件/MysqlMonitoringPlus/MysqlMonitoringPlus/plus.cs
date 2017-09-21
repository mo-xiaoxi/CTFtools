using System;
using System.Collections.Generic;
using System.Text;
using CSPluginKernel;

namespace MysqlMonitoringPlus
{
    /// 构造插件信息属性实例
    [PluginInfo("Mysql监控", "1.0", "Seay", "http://www.cnseay.com/", true)]

    public class plus : IPlugin//实现插件公共接口
    {
        #region IPlugin 成员

        public bool Connect(IfuncObject app, IvarObject var)
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

        public void OnDestory() { }

        #endregion

        private IfuncObject i_func;
        private IvarObject i_attribute;

        public void Run()
        {
            F_MysqlMonitoring F_mysql = new F_MysqlMonitoring();
            F_mysql.Show();

        }

    }
}
