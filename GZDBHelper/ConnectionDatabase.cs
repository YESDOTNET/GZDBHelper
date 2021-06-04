using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace GZDBHelper
{
    /// <summary>
    /// 数据库操作对象
    /// </summary>
    public class ConnectionDatabase : IDatabase
    {
        private readonly string _ConnectionString;
        private readonly DbProviderFactory _ProviderFactory;

        private int _commandtimeout = 30;
        /// <summary>
        /// 获得数据库连接字符串
        /// </summary>
        public DbConnection GetDbConnection()
        {
            return this.CreateConnection();
        }
        /// <summary>
        /// 获得事务
        /// </summary>
        public DbTransaction GetDbTransaction()
        {
            return null;
        }

        /// <summary>
        /// 执行超时
        /// </summary>
        public int CommandTimeout
        {
            get { return _commandtimeout; }
            set { _commandtimeout = value; }
        }

        private Action<DbDataAdapter> CustomerDbDataAdapter;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="providerName">指定提供程序名称的 System.Data.Common.DbProviderFactory 的一个实例</param>
        /// /// <param name="cAdapter"></param>
        public ConnectionDatabase(string connectionString, string providerName, Action<DbDataAdapter> cAdapter)
        {
            _ConnectionString = connectionString;
            _ProviderFactory = DbProviderFactories.GetFactory(providerName);
            CustomerDbDataAdapter = cAdapter;
        }
        /// <summary>
        /// 创建数据库连接并打开
        /// </summary>
        /// <returns></returns>
        private DbConnection CreateConnection()
        {
            try
            {
                var connection = _ProviderFactory.CreateConnection();
                connection.ConnectionString = _ConnectionString;
                connection.Open();
                return connection;
            }
            catch (Exception exception)
            {
                _log.LogException(exception);
                throw exception;
            }
        }


        /// <summary>
        /// 获得数据库操作命令对象
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        private CommandDatabase GetCommandDataBase(DbConnection conn)
        {
            var c = new CommandDatabase(_ProviderFactory, conn, CommandTimeout, CustomerDbDataAdapter);
            return c;
        }

        /// <summary>
        /// 获得数据库操作命令对象
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        private CommandDatabase GetCommandDataBase(DbConnection conn, DbTransaction trans)
        {
            var c = new CommandDatabase(_ProviderFactory, trans, CommandTimeout, CustomerDbDataAdapter);
            return c;
        }

        #region SQL语句
        /// <summary>
        /// 执行SQL语句，并返回受影响行数
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql, IDbParms parameters)
        {
            using (var connection = CreateConnection())
            {
                var db = GetCommandDataBase(connection);
                return db.ExecuteNonQuery(sql, parameters);
            }
        }
        /// <summary>
        /// 执行SQL语句，并返回指定对象集合
        /// </summary>
        /// <typeparam name="T">要返回的对象类型</typeparam>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">查询参数</param>
        /// <param name="action">转换委托</param>
        /// <returns></returns>
        public List<T> ExecuteDataReader<T>(string sql, IDbParms parameters, Func<DbDataReader, T> action)
        {
            using (var connection = CreateConnection())
            {
                var db = GetCommandDataBase(connection);
                return db.ExecuteDataReader(sql, parameters, action);
                // 这里一定要用yield，这样可以延迟执行，直接用return db.ExecuteDataReader(sql, parameters, action)在执行dr.Read()的时候，cmd对象早就释放掉了
                //foreach (var r in db.ExecuteDataReader(sql, parameters, action))
                //    yield return r;
            }
        }
        /// <summary>
        /// 执行SQL语句，委托处理结果
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">查询参数</param>
        /// <param name="action">处理委托</param>
        public void ExecuteDataReader(string sql, IDbParms parameters, Action<DbDataReader> action)
        {
            using (var connection = CreateConnection())
            {
                var db = GetCommandDataBase(connection);
                db.ExecuteDataReader(sql, parameters, action);
            }
        }
        /// <summary>
        /// 执行SQL语句，返回DataTable结构
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="TableName">表名</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        public DataTable GetTable(string sql, string TableName, IDbParms parameters = null)
        {
            using (var connection = CreateConnection())
            {
                var db = GetCommandDataBase(connection);
                return db.GetTable(sql, TableName, parameters);

            }
        }
        /// <summary>
        /// 执行SQL语句，返回DataSet结构
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        public DataSet GetDataSet(string sql, IDbParms parameters = null)
        {
            using (var connection = CreateConnection())
            {
                var db = GetCommandDataBase(connection);
                return db.GetDataSet(sql, parameters);
            }
        }

        /// <summary>
        /// 执行SQL语句，返回第一行第一列
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        public T ExecuteScalar<T>(string sql, IDbParms parameters)
        {
            using (var connection = CreateConnection())
            {
                var db = GetCommandDataBase(connection);
                return db.ExecuteScalar<T>(sql, parameters);
            }

        }


        /// <summary>
        /// 执行SQL语句，判断是否有返回数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public bool HasRow(string sql, IDbParms parameters)
        {
            using (var connection = CreateConnection())
            {
                var db = GetCommandDataBase(connection);
                return db.HasRow(sql, parameters);
            }
        }

        #endregion SQL语句


        #region 存储过程
        /// <summary>
        ///  执行存储过程，并返回受影响行数
        /// </summary>
        /// <param name="StoredProcedureName">存储过程名称</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        public int ExecuteNonQuerySP(string StoredProcedureName, IDbParms parameters)
        {
            using (var connection = CreateConnection())
            {
                var db = GetCommandDataBase(connection);
                return db.ExecuteNonQuerySP(StoredProcedureName, parameters);
            }
        }
        /// <summary>
        /// 执行存储过程，并返回指定对象集合
        /// </summary>
        /// <typeparam name="T">要返回的对象类型</typeparam>
        /// <param name="StoredProcedureName">存储过程名称</param>
        /// <param name="parameters">查询参数</param>
        /// <param name="action">转换委托</param>
        /// <returns></returns>
        public List<T> ExecuteDataReaderSP<T>(string StoredProcedureName, IDbParms parameters, Func<DbDataReader, T> action)
        {
            using (var connection = CreateConnection())
            {
                var db = GetCommandDataBase(connection);
                return db.ExecuteDataReaderSP(StoredProcedureName, parameters, action);
                // 这里一定要用yield，这样可以延迟执行，直接用return db.ExecuteDataReader(sql, parameters, action)在执行dr.Read()的时候，cmd对象早就释放掉了
                //foreach (var r in db.ExecuteDataReaderSP(StoredProcedureName, parameters, action))
                //    yield return r;
            }
        }
        /// <summary>
        /// 执行存储过程，委托处理结果
        /// </summary>
        /// <param name="StoredProcedureName">存储过程名称</param>
        /// <param name="parameters"></param>
        /// <param name="action"></param>
        public void ExecuteDataReaderSP(string StoredProcedureName, IDbParms parameters, Action<DbDataReader> action)
        {
            using (var connection = CreateConnection())
            {
                var db = GetCommandDataBase(connection);
                db.ExecuteDataReaderSP(StoredProcedureName, parameters, action);
            }
        }
        /// <summary>
        /// 执行存储过程，返回DataTable结构
        /// </summary>
        /// <param name="StoredProcedureName">存储过程名称</param>
        /// <param name="TableName">表名</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        public DataTable GetTableSP(string StoredProcedureName, string TableName, IDbParms parameters)
        {
            using (var connection = CreateConnection())
            {
                var db = GetCommandDataBase(connection);
                return db.GetTableSP(StoredProcedureName, TableName, parameters);
            }
        }
        /// <summary>
        /// 执行存储过程，返回DataSet结构
        /// </summary>
        /// <param name="StoredProcedureName">存储过程名称</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        public DataSet GetDataSetSP(string StoredProcedureName, IDbParms parameters)
        {
            using (var connection = CreateConnection())
            {
                var db = GetCommandDataBase(connection);
                return db.GetDataSetSP(StoredProcedureName, parameters);
            }
        }

        /// <summary>
        /// 执行存储过程，返回第一行第一列
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="StoredProcedureName">存储过程名称</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        public T ExecuteScalarSP<T>(string StoredProcedureName, IDbParms parameters)
        {
            using (var connection = CreateConnection())
            {
                var db = GetCommandDataBase(connection);
                return db.ExecuteScalarSP<T>(StoredProcedureName, parameters);
            }

        }


        /// <summary>
        /// 执行存储过程，判断是否有返回数据
        /// </summary>
        /// <param name="StoredProcedureName">存储过程名称</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        public bool HasRowSP(string StoredProcedureName, IDbParms parameters)
        {
            using (var connection = CreateConnection())
            {
                var db = GetCommandDataBase(connection);
                return db.HasRowSP(StoredProcedureName, parameters);
            }
        }
        #endregion 存储过程

        /// <summary>
        /// 外部在同一链接下执行，如果要获取输出的参数值，用此方法执行，配合GetParamValue执行，或者多次提交
        /// </summary>
        /// <param name="action"></param>
        public void Excute(Action<IDatabase> action)
        {
            using (var connection = CreateConnection())
            {
                var db = GetCommandDataBase(connection);
                db.Excute(action);
            }
        }

        /// <summary>
        /// 更新表格数据到数据库
        /// </summary>
        /// <param name="dt">数据</param>
        /// <param name="SelectSQL">查询语句</param>
        /// <returns></returns>
        public bool Update(DataTable dt, string SelectSQL)
        {
            using (var connection = CreateConnection())
            {
                var db = GetCommandDataBase(connection);
                return db.Update(dt, SelectSQL);
            }
        }


        /// <summary>
        /// 更新表格数据到数据库
        /// </summary>
        /// <param name="dt">数据</param>
        /// <param name="SelectSQL">查询语句</param>
        /// <param name="SetAllValues">更新所有值，True时更新所有字段，False时只更新有改变的字段</param>
        /// <returns></returns>
        public bool Update(DataTable dt, string SelectSQL, bool SetAllValues)
        {
            using (var connection = CreateConnection())
            {
                var db = GetCommandDataBase(connection);
                return db.Update(dt, SelectSQL, SetAllValues);
            }
        }


        /// <summary>
        /// 更新表格数据到数据库
        /// </summary>
        /// <param name="dt">数据</param>
        /// <param name="SelectSQL">查询语句</param>
        /// <param name="SetAllValues">更新所有值，True时更新所有字段，False时只更新有改变的字段</param>
        /// <param name="ConflictOption">指定将如何检测和解决对数据源的相互冲突的更改。</param>
        /// <returns></returns>
        public bool Update(DataTable dt, string SelectSQL, bool SetAllValues, ConflictOption ConflictOption)
        {
            using (var connection = CreateConnection())
            {
                var db = GetCommandDataBase(connection);
                return db.Update(dt, SelectSQL, SetAllValues, ConflictOption);
            }
        }

        /// <summary>
        /// 更具模型提交添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objs"></param>
        /// <returns></returns>
        public bool Insert<T>(params T[] objs)
        {
            using (var connection = CreateConnection())
            {
                var db = GetCommandDataBase(connection);
                return db.Insert<T>(objs);
            }
        }
        /// <summary>
        /// 根据模型提交更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objs"></param>
        /// <returns></returns>
        public bool Update<T>(params T[] objs)
        {
            using (var connection = CreateConnection())
            {
                var db = GetCommandDataBase(connection);
                return db.Update<T>(objs);
            }
        }
        /// <summary>
        /// 根据模型删除操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objs"></param>
        /// <returns></returns>
        public bool Delete<T>(params T[] objs)
        {
            using (var connection = CreateConnection())
            {
                var db = GetCommandDataBase(connection);
                return db.Delete<T>(objs);
            }
        }


        /// <summary>
        /// 在事物内执行
        /// </summary>
        /// <param name="action"></param>
        public bool ExecuteTransaction(Func<IDatabase, bool> action)
        {

            using (var connection = CreateConnection())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        //using (var cmd = connection.CreateCommand())
                        //{
                        //    cmd.Transaction = transaction;
                        var db = GetCommandDataBase(connection, transaction);
                        db.ExecuteTransaction(action);

                        //}

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
            //return success;
        }

        /// <summary>
        /// 在事物内执行
        /// </summary>
        /// <param name="action"></param>
        public bool ExecuteTransaction(Action<IDatabase> action)
        {
            bool success = false;
            using (var connection = CreateConnection())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {

                        //using (var cmd = connection.CreateCommand())
                        //{
                        //    cmd.Transaction = transaction;

                        var db = GetCommandDataBase(connection, transaction);
                        success = db.ExecuteTransaction(action);
                        //}
                        if (success == false)
                        {
                            transaction.Rollback();
                        }
                        else
                            transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        success = false;
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            return success;
        }
        /*
        /// <summary>
        /// 同步数据
        /// </summary>
        /// <param name="table"></param>
        /// <param name="batchSize"></param>
        public void BulkCopy(DataTable table, int batchSize)
        {
            using (var connection = CreateConnection())
            {
                var db = GetCommandDataBase(connection);
                db.BulkCopy(table, batchSize);
            }
        }

        /// <summary>
        /// 同步数据
        /// </summary>
        /// <param name="table"></param>
        /// <param name="batchSize"></param>
        public void BulkCopy(DataTable table, List<SqlBulkCopyColumnMapping> ColumnMappings, int batchSize)
        {
            using (var connection = CreateConnection())
            {
                var db = GetCommandDataBase(connection);
                db.BulkCopy(table, ColumnMappings, batchSize);
            }
        }
        */
        /// <summary>
        /// 重载方法，返回链接字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _ConnectionString;
        }



        ///// <summary>
        ///// 不能直接获取参数值，若要获取参数值，请在CommandDatabase中获取！
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="ParameterName"></param>
        ///// <returns></returns>
        //[Obsolete("不能直接获取参数值，若要获取参数值，请在CommandDatabase中获取！", true)]
        //public T GetParamValue<T>(string ParameterName)
        //{
        //    throw new Exception("不能直接获取参数值，若要获取参数值，请在CommandDatabase中获取！");
        //}
    }
}