using System;
using System.Data.Common;
namespace GZDBHelper
{



    /// <summary>
    /// 数据库操作对象生成工厂
    /// </summary>
    public class DatabaseFactory
    {
        ///// <summary>
        ///// 创建数据库操作对象
        ///// </summary>
        ///// <param name="connectionString"></param>
        ///// <param name="providerName"></param>
        ///// <returns></returns>
        //public static IDatabase CreateDatabase(string connectionString, string providerName)
        //{
        //    return new ConnectionDatabase(connectionString, providerName, null);
        //}

        /// <summary>
        /// 创建数据库操作对象
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="providerName"></param>
        /// <param name="CustomerDbDataAdapter"></param>
        /// <returns></returns>
        public static IDatabase CreateDatabase(string connectionString, string providerName, Action<DbDataAdapter> CustomerDbDataAdapter)
        {
            return new ConnectionDatabase(connectionString, providerName, CustomerDbDataAdapter);
        }

        /// <summary>
        /// 验证链接字符串是否正确
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="providerName"></param>
        /// <returns></returns>
        public static bool Validate(string connectionString, string providerName)
        {
            try
            {
                var fac = DbProviderFactories.GetFactory(providerName);
                using (var conn = fac.CreateConnection())
                {
                    conn.ConnectionString = connectionString;
                    conn.Open();
                    conn.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _log.LogException(ex);
                return false;
            }
        }

    }
}