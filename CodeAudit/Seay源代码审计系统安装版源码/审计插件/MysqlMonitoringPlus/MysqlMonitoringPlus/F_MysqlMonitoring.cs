using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace MysqlMonitoringPlus
{
    public partial class F_MysqlMonitoring : Form
    {
        public F_MysqlMonitoring()
        {
            InitializeComponent();
        }

        //断点时间
        string var_datatime = "";

        BindingSource Bs = null;

        #region  建立MySql数据库连接
        /// <summary>    
        /// 建立数据库连接.    
        /// </summary>    
        /// <returns>返回MySqlConnection对象</returns>
        public MySqlConnection func_getmysqlcon()
        {
            string M_str_sqlcon = "server=" + txt_host.Text + ";user id=" + txt_user.Text + ";password=" + txt_pass.Text + ";database=mysql";

            MySqlConnection myCon = new MySqlConnection(M_str_sqlcon);
            return myCon;
        }
        #endregion


        #region  执行MySqlCommand命令
        /// <summary>    
        /// 执行MySqlCommand    
        /// </summary>    
        /// <param name="M_str_sqlstr">SQL语句</param>    
        public int func_getmysqlcom(string M_str_sqlstr)
        {
            int count = 0;
            MySqlConnection mysqlcon = this.func_getmysqlcon();
            mysqlcon.Open();
            MySqlCommand mysqlcom = new MySqlCommand(M_str_sqlstr, mysqlcon);
            count = mysqlcom.ExecuteNonQuery();
            mysqlcom.Dispose();
            mysqlcon.Close();
            mysqlcon.Dispose();
            return count;
        }
        #endregion

        #region  创建MySqlDataReader对象
        /// <summary>   
        /// 创建一个MySqlDataReader对象    
        /// </summary>    
        /// <param name="M_str_sqlstr">SQL语句</param>    
        /// <returns>返回MySqlDataReader对象</returns>    
        public DataSet func_getmysqlread(string M_str_sqlstr)
        {
            MySqlConnection mysqlcon = this.func_getmysqlcon();
            mysqlcon.Open();

            MySqlDataAdapter sda = new MySqlDataAdapter(M_str_sqlstr, mysqlcon);
            //MySqlCommand mysqlcom = new MySqlCommand(M_str_sqlstr, mysqlcon);
            
            DataSet ds = new DataSet();
            sda.Fill(ds);
            //MySqlDataReader mysqlread = mysqlcom.ExecuteReader(CommandBehavior.CloseConnection);
            return ds;
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            var_datatime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            txt_break.Text = "断点：" + var_datatime;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                
                func_getmysqlcom("set global general_log=on;SET GLOBAL log_output='table';");
                
                string sql = "select event_time,argument from mysql.general_log where command_type='Query' and argument not like 'set global general_log=on;SET GLOBAL log_output%' and argument not like 'select event_time,argument from%' and event_time>'" + var_datatime + "'";
                DataSet ds = func_getmysqlread(sql);
                DataTableCollection tables = ds.Tables;
                DataView view1 = new DataView(tables[0]);

                Bs = new BindingSource();
                Bs.DataSource = view1;
                
                dataGridView1.DataSource = Bs;

                
                txt_count.Text = "行数：" + Bs.Count;
                
                dataGridView1.Columns[0].HeaderText = "查询时间";
                dataGridView1.Columns[1].HeaderText = "查询语句";

                dataGridView1.Columns[0].Width = 150;
                //dataGridView1.Columns[1].Width = dataGridView1.Width - dataGridView1.Columns[0].Width - 10;
                dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                
            }
            catch (Exception)
            {
                MessageBox.Show("数据库出错，请检查连接信息以及确认mysql版本在5.1.6以上","提示");
            }
        }

        private void F_MysqlMonitoring_Load(object sender, EventArgs e)
        {
            var_datatime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            txt_break.Text = "断点：" + var_datatime;
            
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count>0)
            {
                Clipboard.SetDataObject(dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
            }
        }



        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void txt_searchkey_TextChanged(object sender, EventArgs e)
        {
            if (Bs!=null)
            {
                Bs.RemoveFilter();
                if (txt_searchkey.Text != "")
                {
                    Bs.Filter = "argument like '%" + txt_searchkey.Text.Replace("'","\\'") + "%'";
                    //dataGridView1.Refresh();
                    txt_count.Text = "行数：" + Bs.Count;
                }
                else
                {
                    txt_count.Text = "行数：" + Bs.Count;
                }
            }            
        }
    }
}
