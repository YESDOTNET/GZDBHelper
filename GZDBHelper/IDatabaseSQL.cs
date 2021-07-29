using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;

namespace GZDBHelper
{
    /// <summary>
    /// 数据库操作接口
    /// </summary>
    public partial interface IDatabase
    {
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
        [Obsolete("弃用，请改用：ExecuteDataList 方法", true)]
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
        /// 执行SQL语句，返回第一行第一列
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


    }


}