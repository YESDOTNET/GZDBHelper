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
        int CommandTimeOut { get; set; }


      


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