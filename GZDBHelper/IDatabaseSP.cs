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
        /// <param name="StoredProcedureName"></param>
        /// <param name="parameters"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        List<T> ExecuteDataListSP<T>(string StoredProcedureName, IDbParms parameters, Func<DbDataReader, T> action);
       
        /// <summary>
        /// 执行存储过程，返回数据集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="StoredProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        List<T> ExecuteDataListSP<T>(string StoredProcedureName, IDbParms parameters) where T : new();
       

        /// <summary>
        /// 执行存储过程，返回第一行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="StoredProcedureName"></param>
        /// <param name="parameters"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        T ExecuteDataFirstSP<T>(string StoredProcedureName, IDbParms parameters, Func<DbDataReader, T> action);

        /// <summary>
        /// 执行存储过程，返回第一行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="StoredProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        T ExecuteDataFirstSP<T>(string StoredProcedureName, IDbParms parameters) where T : new();
   

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
     


    }


}