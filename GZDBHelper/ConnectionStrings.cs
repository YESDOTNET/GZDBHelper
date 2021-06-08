using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GZDBHelper
{
    /// <summary>
    /// 数据库链接字符串生成类
    /// </summary>
    public class ConnectionStrings
    {
        /// <summary>
        /// 参考Provider名称
        /// </summary>
        public class ProviderNames
        {
            /// <summary>
            /// Access ProviderName
            /// </summary>
            public const string ProviderNameForAccess = "System.Data.OleDb";
            /// <summary>
            /// Sqlite ProviderName
            /// </summary>
            public const string ProviderNameForSqlite = "System.Data.SQLite";
            /// <summary>
            /// Oracle ProviderName
            /// </summary>
            public const string ProviderNameForOracle = "Oracle.ManagedDataAccess.Client";
            /// <summary>
            /// MSSql ProviderName
            /// </summary>
            public const string ProviderNameForMSSql = "System.Data.SqlClient";
            /// <summary>
            /// MySQL ProviderName
            /// </summary>
            public const string ProviderNameForMySql = "MySql.Data.MySqlClient";
        }


        /// <summary>
        /// 生成MSSQL链接字符串  Server = {0};Database = {1};User ID = {2};Password = {3};Trusted_Connection = False
        /// </summary>
        /// <param name="Server">服务器地址</param>
        /// <param name="Database">数据库名</param>
        /// <param name="UserID">登陆名</param>
        /// <param name="Password">密码</param>
        /// <returns></returns>
        public static string BuildMSSQLConnectionString(string Server, string Database, string UserID, string Password)
        {
            //Data Source = myServerAddress;Initial Catalog = myDataBase;User Id = myUsername;Password = myPassword;
            return String.Format("Data Source = {0};Initial Catalog = {1};User Id = {2};Password = {3};", Server, Database, UserID, Password);
        }

        /// <summary>
        /// 生成MySQL连接字符串 data source={0};port={1};uid={2};pwd={3}; database={4};
        /// </summary>
        /// <param name="Server"></param>
        /// <param name="Port"></param>
        /// <param name="Database"></param>
        /// <param name="UserID"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static string BuildMySQLConnectionString(string Server, int Port, string Database, string UserID, string Password)
        {
            //String connetStr = "server=127.0.0.1;port=3306;user=root;password=root; database=minecraftdb;";;
            //String connetStr = "data source=127.0.0.1;port=3306;uid=root;pwd=root; database=minecraftdb;";;
            return String.Format("data source={0};port={1};uid={2};pwd={3};Database={4};charset=utf8;", Server, Port, UserID, Password, Database);
        }

        /// <summary>
        /// 生成Oracle链接字符串
        /// </summary>
        /// <param name="Host">服务器地址</param>
        /// <param name="Port">端口号</param>
        /// <param name="DataBaseName">数据库名</param>
        /// <param name="UserID">用户名</param>
        /// <param name="Password">密码</param>
        /// <returns></returns>
        public static string BuildOracleConnectionString(string Host, int Port, string DataBaseName, string UserID, string Password)
        {
            //return String.Format("Data Source={0}:{1}/{2}; User Id={3}; password={4}; Pooling=false;", Host, Port, DataBaseName, UserID, Password);
            //string str = $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={Host})(PORT={Port}))(CONNECT_DATA=(SERVICE_NAME={DataBaseName})));Persist Security Info=True;User ID={UserID};Password={Password};";
            //return $"Data Source = (DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = {Host})(PORT = {Port})))(CONNECT_DATA =(SERVICE_NAME = ORCL)));User ID={UserID};PassWord={Password}";
            return $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={Host})(PORT={Port}))(CONNECT_DATA=(SERVICE_NAME={DataBaseName})));User Id={UserID};Password={Password}";


        }

        /// <summary>
        /// 生成Sqlite链接字符串
        /// </summary>
        /// <param name="DataSource">数据库文件路径</param>
        /// <param name="Password">打开密码</param>
        /// <returns></returns>
        public static string BuildSqliteConnectionString(string DataSource, string Password)
        {
            return String.Format("Data Source={0};Pooling=true;password={1}", DataSource, Password);
        }

        /// <summary>
        /// 生成Sqlite链接字符串
        /// </summary>
        /// <param name="DataSource">数据库文件路径</param>
        /// <returns></returns>
        public static string BuildSqliteConnectionString(string DataSource)
        {
            return String.Format("Data Source={0};Pooling=true;", DataSource);
        }


        /// <summary>
        /// 生成Access链接字符串
        /// </summary>
        /// <param name="Provider"></param>
        /// <param name="DataSource"></param>
        /// <returns></returns>
        public static string BuildAccessConnectionStringUseProvider(string Provider, string DataSource)
        {
            return String.Format("Provider={0};Data Source={1};", Provider, DataSource);
        }
        /// <summary>
        /// 生成Access链接字符串
        /// </summary>
        /// <param name="Provider"></param>
        /// <param name="DataSource"></param>
        /// <param name="UserID"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static string BuildAccessConnectionStringUseProvider(string Provider, string DataSource, string UserID, string Password)
        {
            return String.Format("Provider={0};Data Source={0};User Id={1}; Password={2}", Provider, DataSource, UserID, Password);
        }

        /// <summary>
        /// 生成Access链接字符串 Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};
        /// </summary>
        /// <param name="DataSource"></param>
        /// <returns></returns>
        public static string BuildAccessConnectionString(string DataSource)
        {
            return String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};", DataSource);
        }

        /// <summary>
        /// 生成Access链接字符串 Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};
        /// </summary>
        /// <param name="DataSource"></param>
        /// <param name="UserID"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static string BuildAccessConnectionString(string DataSource, string UserID, string Password)
        {
            return String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};User Id={1}; Password={2}", DataSource, UserID, Password);
        }
    }
}
