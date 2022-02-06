using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using GZDBHelper.SQLite.Model;
namespace GZDBHelper
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class Extensions
    {


        /// <summary>
        /// 获得 MSSQL 基本工具
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static SQLiteTools GetSQLiteTools(this IDatabase current)
        {
            return new SQLiteTools(current);
        }

    }
}
