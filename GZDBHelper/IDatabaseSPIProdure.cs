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
        /// 执行存储过程，并返回受影响行数
        /// </summary>
        /// <param name="procedure"></param>
        /// <returns></returns>
        int ExecuteNonQuerySP(IProcedure procedure);
        /// <summary>
        /// 执行存储过程，返回数据集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procedure"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        List<T> ExecuteDataListSP<T>(IProcedure procedure, Func<DbDataReader, T> action);
        /// <summary>
        /// 执行存储过程，返回数据集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procedure"></param>
        /// <returns></returns>
        List<T> ExecuteDataListSP<T>(IProcedure procedure) where T : new();
        /// <summary>
        /// 执行存储过程，返回第一行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procedure"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        T ExecuteDataFirstSP<T>(IProcedure procedure, Func<DbDataReader, T> action);

        /// <summary>
        /// 执行存储过程，返回第一行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procedure"></param>
        /// <returns></returns>
        T ExecuteDataFirstSP<T>(IProcedure procedure) where T : new();

        /// <summary>
        /// 执行存储过程，委托处理结果
        /// </summary>
        /// <param name="procedure"></param>
        /// <param name="action"></param>
        void ExecuteDataReaderSP(IProcedure procedure, Action<DbDataReader> action);

        /// <summary>
        /// 执行存储过程，返回第一行第一列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procedure"></param>
        /// <returns></returns>
        T ExecuteScalarSP<T>(IProcedure procedure);

        /// <summary>
        /// 执行存储过程，返回DataTable结构
        /// </summary>
        /// <param name="procedure"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        DataTable GetTableSP(IProcedure procedure, string TableName);

        /// <summary>
        /// 执行存储过程，返回DataSet结构
        /// </summary>
        /// <param name="procedure"></param>
        /// <returns></returns>
        DataSet GetDataSetSP(IProcedure procedure);

        /// <summary>
        /// 执行存储过程，判断是否有返回数据
        /// </summary>
        /// <param name="procedure"></param>
        /// <returns></returns>
        bool HasRowSP(IProcedure procedure);
    }

 
}