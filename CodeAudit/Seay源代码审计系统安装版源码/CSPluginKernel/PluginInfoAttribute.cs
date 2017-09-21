using System;

namespace CSPluginKernel
{
	/// <summary>
	/// 
	/// </summary>
    //插件版本，作者信息类
	public class PluginInfoAttribute : System.Attribute
	{
		/// <summary>
		/// Deprecated. Do not use.
		/// </summary>
		public PluginInfoAttribute() {}

		public PluginInfoAttribute( string name , string version , string author , string webpage , bool loadWhenStart )
		{
			_Name = name;
			_Version = version;
			_Author = author;
			_Webpage = webpage;
			_LoadWhenStart = loadWhenStart;
		}

		public string Name {
			get{
				return _Name;
			}
		}

		public string Version {
			get{
				return _Version;
			}
		}

		public string Author {
			get{
				return _Author;
			}	 
		}

		public string Webpage {
			get{
				return _Webpage;
			}
		}

		public bool LoadWhenStart {
			get{
				return _LoadWhenStart;
			}
		}

		/// <summary>
		/// 用来存储一些有用的信息
		/// </summary>
		public object Tag {
			get{
				return _Tag;
			}
			set{
				_Tag = value;
			}
		}

		public int Index {
			get{
				return _Index;
			}
			set{
				_Index = value;
			}
		}

		private string _Name = "";
		private string _Version = "";
		private string _Author = "";
		private string _Webpage = "";
		private object _Tag = null;
		private int    _Index = 0;

		private bool _LoadWhenStart = true;
	}
}
