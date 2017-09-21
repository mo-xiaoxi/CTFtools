using System;
using System.Drawing;

namespace CSPluginKernel {

    public interface IfuncObject
    {
		//void Alert( string msg );// 产生一条信息
		//void ShowInStatusBar( string msg ); // 将指定的信息显示在状态栏

		//void SetDelegate( Delegates whichOne , EventHandler target );
        void func_I_changtext(string text);

        void func_I_addtab(object control, string title);
	}

	/// <summary>
	/// 编辑器对象必须实现这个接口
	/// </summary>
    public interface IvarObject{

        //void func_I_changtext { get; set; }
		//Color SelectionColor { get; set; }
        //Font SelectionFont { get; set; }
        //int SelectionStart { get; set; }
        //int SelectionLength { get; set; }
        //string SelectionRTF { get; set; }

        //bool HasChanges { get; }

        //void Select( int start , int length );
        //void AppendText( string str );

        //void SaveFile( string fileName );
        //void SaveFile();

        //void OpenFile( string fileName );

        /// <summary>
        /// 选择的路径
        /// </summary>
        string var_iwebpath { get; set; }

        /// <summary>
        /// 文件编码
        /// </summary>
        string var_ifileencoding { get; set; }


	}

	public enum Delegates {
		Delegate_ActiveDocumentChanged ,
	}

}