using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;

namespace GZDBHelper
{
    /// <summary>
    /// 数据库操作接口
    /// </summary>
    public interface IDatabase
    {
        /// <summary>
        /// 获得数据库连接对象
        /// </summary>
        /// <returns></returns>
        DbConnection GetDbConnection();
        /// <summary>
        /// 获得事务
        /// </summary>
        /// <returns></returns>
        DbTransaction GetDbTransaction();
        /// <summary>
        /// 命令超时时间设置，单位毫秒
        /// </summary>
        int CommandTimeout { get; set; }


        ///// <summary>
        ///// 这些方法用于创建提供程序对数据源类的实现的实例
        ///// </summary>
        //DbProviderFactory ProviderFactory { get; } 

        #region SQL语句
        /// <summary>
        /// 执行SQL语句，并返回受影响行数
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        int ExecuteNonQuery(string sql, IDbParms parameters);

        /// <summary>
        /// 执行SQL语句，并返回指定对象集合
        /// </summary>
        /// <typeparam name="T">要返回的对象类型</typeparam>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">查询参数</param>
        /// <param name="action">转换委托</param>
        /// <returns></returns>
        [Obsolete("弃用，请改用：ExecuteDataList 方法",true)]
        List<T> ExecuteDataReader<T>(string sql, IDbParms parameters, Func<DbDataReader, T> action);

        /// <summary>
        /// 执行SQL语句，返回数据集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        List<T> ExecuteDataList<T>(string sql, IDbParms parameters, Func<DbDataReader, T> action);
        /// <summary>
        /// 执行SQL语句，返回数据集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        List<T> ExecuteDataList<T>(string sql, IDbParms parameters) where T : new();

        /// <summary>
        /// 执行SQL语句，返回第一行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        T ExecuteDataFirst<T>(string sql, IDbParms parameters, Func<DbDataReader, T> action);
        /// <summary>
        /// 执行SQL语句，返回第一行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        T ExecuteDataFirst<T>(string sql, IDbParms parameters) where T : new();

        /// <summary>
        /// 执行SQL语句，委托处理结果
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">查询参数</param>
        /// <param name="action">处理委托</param>
        void ExecuteDataReader(string sql, IDbParms parameters, Action<DbDataReader> action);

        /// <summary>
        /// 执行SQL语句，返回第一行数据
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        T ExecuteScalar<T>(string sql, IDbParms parameters);
        /// <summary>
        /// 执行SQL语句，返回DataTable结构
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="TableName">表名</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        DataTable GetTable(string sql, string TableName, IDbParms parameters);
        /// <summary>
        /// 执行SQL语句，返回DataSet结构
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        DataSet GetDataSet(string sql, IDbParms parameters);
        /// <summary>
        /// 执行SQL语句，判断是否有返回数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        bool HasRow(string sql, IDbParms parameters);

        #endregion


        #region 存储过程
        /// <summary>
        ///  执行存储过程，并返回受影响行数
        /// </summary>
        /// <param name="StoredProcedureName">存储过程名称</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        int ExecuteNonQuerySP(string StoredProcedureName, IDbParms parameters);
        /// <summary>
        /// 执行存储过程，并返回指定对象集合
        /// </summary>
        /// <typeparam name="T">要返回的对象类型</typeparam>
        /// <param name="StoredProcedureName">存储过程名称</param>
        /// <param name="parameters">查询参数</param>
        /// <param name="action">转换委托</param>
        /// <returns></returns>
        [Obsolete("弃用，请改用：ExecuteDataListSP 方法", true)]
        List<T> ExecuteDataReaderSP<T>(string StoredProcedureName, IDbParms parameters, Func<DbDataReader, T> action);

        /// <summary>
        /// 执行存储过程，返回数据集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        List<T> ExecuteDataListSP<T>(string sql, IDbParms parameters, Func<DbDataReader, T> action);
        /// <summary>
        /// 执行存储过程，返回数据集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        List<T> ExecuteDataListSP<T>(string sql, IDbParms parameters) where T : new();

        /// <summary>
        /// 执行存储过程，返回第一行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        T ExecuteDataFirstSP<T>(string sql, IDbParms parameters, Func<DbDataReader, T> action);
        /// <summary>
        /// 执行存储过程，返回第一行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        T ExecuteDataFirstSP<T>(string sql, IDbParms parameters) where T : new();

        /// <summary>
        /// 执行存储过程，委托处理结果
        /// </summary>
        /// <param name="StoredProcedureName">存储过程名称</param>
        /// <param name="parameters"></param>
        /// <param name="action"></param>
        void ExecuteDataReaderSP(string StoredProcedureName, IDbParms parameters, Action<DbDataReader> action);

        /// <summary>
        /// 执行存储过程，返回第一行第一列
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="StoredProcedureName">存储过程名称</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        T ExecuteScalarSP<T>(string StoredProcedureName, IDbParms parameters);
        /// <summary>
        /// 执行存储过程，返回DataTable结构
        /// </summary>
        /// <param name="StoredProcedureName">存储过程名称</param>
        /// <param name="TableName">表名</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        DataTable GetTableSP(string StoredProcedureName, string TableName, IDbParms parameters);
        /// <summary>
        /// 执行存储过程，返回DataSet结构
        /// </summary>
        /// <param name="StoredProcedureName">存储过程名称</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        DataSet GetDataSetSP(string StoredProcedureName, IDbParms parameters);
        /// <summary>
        /// 执行存储过程，判断是否有返回数据
        /// </summary>
        /// <param name="StoredProcedureName">存储过程名称</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        bool HasRowSP(string StoredProcedureName, IDbParms parameters);

        #endregion

        /// <summary>
        /// 更新表格数据到数据库
        /// </summary>
        /// <param name="dt">数据</param>
        /// <param name="SelectSQL">查询语句</param>
        /// <returns></returns>
        bool Update(DataTable dt, string SelectSQL);
        /// <summary>
        /// 更新表格数据到数据库
        /// </summary>
        /// <param name="dt">数据</param>
        /// <param name="SelectSQL">查询语句</param>
        /// <param name="SetAllValues">更新所有值，True时更新所有字段，False时只更新有改变的字段</param>
        /// <returns></returns>

        bool Update(DataTable dt, string SelectSQL, bool SetAllValues);
        /// <summary>
        /// 更新表格数据到数据库
        /// </summary>
        /// <param name="dt">数据</param>
        /// <param name="SelectSQL">查询语句</param>
        /// <param name="SetAllValues">更新所有值，True时更新所有字段，False时只更新有改变的字段</param>
        /// <param name="ConflictOption">指定将如何检测和解决对数据源的相互冲突的更改。</param>
        /// <returns></returns>
        bool Update(DataTable dt, string SelectSQL, bool SetAllValues, ConflictOption ConflictOption);

        /// <summary>
        /// 新增【模型】
        /// </summary>
        /// <param name="objs"></param>
        bool Insert<T>(params T[] objs);
        /// <summary>
        /// 更新【模型】
        /// </summary>
        /// <param name="objs"></param>
        bool Update<T>(params T[] objs);
        /// <summary>
        /// 删除【模型】
        /// </summary>
        /// <param name="objs"></param>
        bool Delete<T>(params T[] objs);


        /// <summary>
        /// 在事物内执行
        /// </summary>
        /// <param name="action"></param>
        bool ExecuteTransaction(Action<IDatabase> action);
        /// <summary>
        /// 在事务内运行
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        bool ExecuteTransaction(Func<IDatabase, bool> action);
        ///// <summary>
        ///// 在事物内执行 手动提交或回滚事务
        ///// </summary>
        ///// <param name="action"></param>
        //void ExecuteTransaction(Action<IDatabase, DbTransaction> action);

        /// <summary>
        /// 外部在同一链接下执行，如果要获取输出的参数值，用此方法执行，配合GetParamValue执行，或者多次提交
        /// </summary>
        /// <param name="action"></param>
        void Excute(Action<IDatabase> action);


        /*

        /// <summary>
        /// 同步数据
        /// </summary>
        /// <param name="table"></param>
        /// <param name="batchSize"></param>
        void BulkCopy(DataTable table, int batchSize = 100);

        void BulkCopy(DataTable table, List<SqlBulkCopyColumnMapping> ColumnMappings, int batchSize = 100);

        */

        ///// <summary>
        ///// 获取参数值
        ///// </summary>
        ///// <typeparam name="T">返回类型</typeparam>
        ///// <param name="ParameterName">参数名</param>
        ///// <returns></returns>
        //T GetParamValue<T>(string ParameterName);

    }
}