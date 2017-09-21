using System;

namespace CSPluginKernel
{

    //本程序的插件必须实现这个接口
	public interface IPlugin 
    {
        bool Connect(IfuncObject app,IvarObject var);//连接主程序的方法，app为主程序实例
		void OnDestory();
		void OnLoad();
		void Run();
	}


}