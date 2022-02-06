using System.Collections.Generic;
using System.Data;
using System.Linq;
using GZDBHelper.SQLite.Model;
namespace GZDBHelper
{
    /// <summary>
    /// MSSQL 数据库 集成
    /// </summary>
    public class SQLiteTools
    {
        private IDatabase db;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_db"></param>
        public SQLiteTools(IDatabase _db)
        {
            db = _db;
        }


        /// <summary>
        /// 获得数据库所有表结构
        /// </summary>
        /// <returns></returns>
        public List<ModelTable> GetTables()
        {
            string sql = $@"SELECT name FROM sqlite_master WHERE type='table' ORDER BY name;  ";

            return db.ExecuteDataList<ModelTable>(sql, null);

        }

    }
}
