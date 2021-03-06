using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using GZDBHelper.MSSQL.Model;
#if NET40 || NET45 || NET46
using System.Data.SqlClient;
#else
using Microsoft.Data.SqlClient;
#endif
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
        public static SQLServerTools GetMSSQLTools(this IDatabase current)
        {
            return new SQLServerTools(current);
        }

        static SqlBulkCopy CreateSqlBulkCopy(SqlConnection conn, SqlTransaction tran)
        {
            if (tran != null)
            {
                return new SqlBulkCopy(tran.Connection, SqlBulkCopyOptions.Default, tran);
            }
            else
                return new SqlBulkCopy(conn);
        }

        /// <summary>
        /// SQL Server 批量插入数据
        /// </summary>
        /// <param name="current"></param>
        /// <param name="table"></param>
        /// <param name="batchSize"></param>
        public static void BulkCopy(this IDatabase current, DataTable table, int batchSize)
        {
            using (var Conn = current.GetDbConnection())
            {
                var Trans = (SqlTransaction)current.GetDbTransaction();
                using (SqlBulkCopy bulkcopy = CreateSqlBulkCopy((SqlConnection)Conn, Trans))
                {
                    if (table != null && table.Rows.Count > 0)
                    {
                        bulkcopy.DestinationTableName = table.TableName;
                        //bulkcopy.BatchSize = 100;
                        bulkcopy.BatchSize = batchSize;
                        bulkcopy.WriteToServer(table);

                    }
                }
            }
        }

        /// <summary>
        /// SQL Server 批量插入数据
        /// </summary>
        /// <param name="current"></param>
        /// <param name="table"></param>
        /// <param name="ColumnMappings"></param>
        /// <param name="batchSize"></param>
        public static void BulkCopy(this IDatabase current, DataTable table, List<SqlBulkCopyColumnMapping> ColumnMappings, int batchSize)
        {
            using (var Conn = current.GetDbConnection())
            {
                var Trans = (SqlTransaction)current.GetDbTransaction();
                using (SqlBulkCopy bulkcopy = CreateSqlBulkCopy((SqlConnection)Conn, Trans))
                {

                    if (table != null && table.Rows.Count > 0)
                    {
                        bulkcopy.DestinationTableName = table.TableName;
                        //bulkcopy.BatchSize = 100;
                        bulkcopy.BatchSize = batchSize;
                        foreach (SqlBulkCopyColumnMapping key in ColumnMappings)
                        {
                            bulkcopy.ColumnMappings.Add(key);
                        }
                        bulkcopy.WriteToServer(table);

                    }
                }
            }
        }



    }
}
