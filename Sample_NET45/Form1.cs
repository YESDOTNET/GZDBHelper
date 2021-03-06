using GZDBHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample_NET45
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 数据库服务器
            string server = txt_MSSQL_Server.Text;
            // 用户名
            string user = txt__MSSQL_user.Text;
            // 密码
            string pwd = txt_MSSQL_pwd.Text;
            // 数据库名称
            string dbname = txt_MSSQL_dbName.Text;

            // 生成数据库连接字符串
            var connStr = GZDBHelper.ConnectionStrings.BuildMSSQLConnectionString(server, dbname, user, pwd);
            //DbProviderFactories
            //var obj = new System.Data.SqlClient.SqlClientFactory();
            // 创建数据库对象
            //var db = GZDBHelper.DatabaseFactory.CreateDatabase(connStr, GZDBHelper.ConnectionStrings.ProviderNames.ProviderNameForMSSql, null);
            var db = GZDBHelper.DatabaseFactory.CreateDatabase(connStr, System.Data.SqlClient.SqlClientFactory.Instance, null);

            var data2 = db.GetMSSQLTools().GetObjects_Size(ObjectType.Table);

            // 执行SQL语句
            DataTable data = db.GetTable("select database_id,name from sys.databases", "databases", null);

            //GZDBHelper.SqlParameterProvider parm = new GZDBHelper.SqlParameterProvider();
            //parm.AddParameter("@isid", SqlDbType.Int, 1);
            //string sql = "delete from tb_test where isid=@isid";
            //db.ExecuteDataReader(sql, parm);
        }

        // 测试SQLite数据库
        private void btn_testSQLite_Click(object sender, EventArgs e)
        {
            // SQLite 文件位置
            string dbFile = @"C:\Users\XQ-Garson\Desktop\TestDB.db";

            // 生成数据库连接
            var connStr = GZDBHelper.ConnectionStrings.BuildSqliteConnectionString(dbFile);

            // 创建数据库对象
            //var db = GZDBHelper.DatabaseFactory.CreateDatabase(connStr, GZDBHelper.ConnectionStrings.ProviderNames.ProviderNameForSqlite, null);
            var db = GZDBHelper.DatabaseFactory.CreateDatabase(connStr, System.Data.SQLite.SQLiteFactory.Instance, null);
            // 执行SQL获取数据
            DataTable data = db.GetTable("select * from tb_test", "tb_test", null);

        }

        private void btn_testMySQL_Click(object sender, EventArgs e)
        {
            // 数据库服务器
            string server = txt_MySQL_Server.Text;
            // 端口号
            int port = int.Parse(txt_MySQL_Port.Text);
            // 用户名
            string user = txt_MySQL_User.Text;
            // 密码
            string pwd = txt_MySQL_Pwd.Text;
            // 数据库名称
            string dbname = txt_MySQL_dbName.Text;

            // 生成数据库连接
            var connStr = GZDBHelper.ConnectionStrings.BuildMySQLConnectionString(server, port, dbname, user, pwd);

            // 创建数据库对象
            //var db = GZDBHelper.DatabaseFactory.CreateDatabase(connStr, GZDBHelper.ConnectionStrings.ProviderNames.ProviderNameForMySql, null);

            var db = GZDBHelper.DatabaseFactory.CreateDatabase(connStr, MySql.Data.MySqlClient.MySqlClientFactory.Instance, null);

            DataTable data = db.GetTable("select * from tb_employee;", "tb_employee", null);
        }

        private void btn_testOracle_Click(object sender, EventArgs e)
        {
            //string Host = "192.168.68.129";
            //int Port = 1521;
            //string DataBaseName = "orcl.localdomain";
            //string UserID = "HR";
            //string Password = "123456";

            string Host = txt_Oracle_Host.Text;
            int Port = int.Parse(txt_Oracle_Port.Text);
            string DataBaseName = txt_Oracle_DataBaseName.Text;
            string UserID = txt_Oracle_User.Text;
            string Password = txt_Oracle_PWD.Text;
            // 生成数据库连接
            var connStr = GZDBHelper.ConnectionStrings.BuildOracleConnectionString(Host, Port, DataBaseName, UserID, Password);

            // 创建数据库对象
            //var db = GZDBHelper.DatabaseFactory.CreateDatabase(connStr, GZDBHelper.ConnectionStrings.ProviderNames.ProviderNameForOracle, null);

            var db = GZDBHelper.DatabaseFactory.CreateDatabase(connStr, Oracle.ManagedDataAccess.Client.OracleClientFactory.Instance, null);

            DataTable data = db.GetTable("SELECT * from JOBS;", "JOBS", null);
        }
    }
}
