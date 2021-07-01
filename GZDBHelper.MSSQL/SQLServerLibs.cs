using System.Collections.Generic;
using System.Linq;
using GZDBHelper.MSSQL.Model;
#if NET40 || NET45 || NET46
using System.Data.SqlClient;
#else
#endif
namespace GZDBHelper
{
    /// <summary>
    /// MSSQL 数据库 集成
    /// </summary>
    public class SQLServerLibs
    {
        private IDatabase db;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_db"></param>
        public SQLServerLibs(IDatabase _db)
        {
            db = _db;
        }

        /// <summary>
        /// 获得数据库列表
        /// </summary>
        /// <returns></returns>
        public List<ModelDBItem> GetDBList()
        {
            string sql = "SELECT dbid,name FROM  master..sysdatabases WHERE name NOT IN ( 'master', 'model', 'msdb', 'tempdb', 'northwind','pubs' ) order by name";
            return db.ExecuteDataList<ModelDBItem>(sql, null);
        }
        /// <summary>
        /// 获得数据库对象
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public List<ModelObjectItem> GetObjects(ObjectType objectType)
        {
            List<string> xtype = new List<string>();
            switch (objectType)
            {
                case ObjectType.Table:
                    xtype.Add("U");
                    break;
                case ObjectType.View:
                    xtype.Add("V");
                    break;
            }
            if (xtype.Count > 0)
            {
                string inStr = string.Join(",", xtype.Select(a => "'" + a + "'"));
                string sql = $"SELECT name FROM YiDianTongV2..sysobjects Where xtype in ({inStr}) ORDER BY xtype, name";

                return db.ExecuteDataList<ModelObjectItem>(sql, null);
            }
            else
                return null;
        }
    }
    /// <summary>
    /// 数据库对象类型
    /// </summary>
    public enum ObjectType
    {
        /// <summary>
        /// 表
        /// </summary>
        Table,
        /// <summary>
        /// 视图
        /// </summary>
        View
    }
}
