using GZDBHelper.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Text;

namespace GZDBHelper
{
    internal class CommandDatabase : IDatabase
    {
        //private DbCommand Command;
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private DbConnection Conn { get; set; }
        /// <summary>
        /// 事务
        /// </summary>
        private DbTransaction Trans { get; set; }

        private readonly DbProviderFactory _ProviderFactory;

        private int _commandtimeout = 30;

        public DbConnection GetDbConnection()
        {
            return Conn;
        }

        public DbTransaction GetDbTransaction()
        {
            return Trans;
        }

        /// <summary>
        /// 命令i超时时间
        /// </summary>
        public int CommandTimeout
        {
            get { return _commandtimeout; }
            set { _commandtimeout = value; }
        }


        private DbDataAdapter CreateDataAdapter()
        {
            return _ProviderFactory.CreateDataAdapter();
        }

        private Action<DbDataAdapter> CustomerDbDataAdapter;


        /// <summary>
        /// 构造函数
        /// </summary>
        internal CommandDatabase(DbProviderFactory ProviderFactory, DbConnection conn, int commandTimeout, Action<DbDataAdapter> cAdapter)
        {
            _ProviderFactory = ProviderFactory;
            //Command = cmd;
            Conn = conn;
            CommandTimeout = commandTimeout;
            CustomerDbDataAdapter = cAdapter;
        }



        /// <summary>
        /// 构造函数
        /// </summary>
        internal CommandDatabase(DbProviderFactory ProviderFactory, DbTransaction trans, int commandTimeout, Action<DbDataAdapter> cAdapter)
        {
            _ProviderFactory = ProviderFactory;
            //Command = cmd;
            Trans = trans;
            CommandTimeout = commandTimeout;
            CustomerDbDataAdapter = cAdapter;
        }

        private DbCommand PrepareCommand(string sql, IDbParms parameters, CommandType CommandType)
        {
            DbCommand Command;
            if (Trans != null)
            {

                //Command = _ProviderFactory.CreateCommand();
                Command = Trans.Connection.CreateCommand();
                Command.Transaction = Trans;
            }
            else
            {
                //Command = _ProviderFactory.CreateCommand();
                Command = Conn.CreateCommand();
                //Command = _ProviderFactory.CreateCommand();
                //Command.Connection = Conn;
            }
            Command.CommandType = CommandType;
            Command.CommandText = sql;
            Command.CommandTimeout = CommandTimeout;
            //Command.SetParameters(parameters);
            if (parameters != null)
                SetParameters(Command, parameters);

            //Command.Disposed += Command_Disposed;
            return Command;
        }

        //private void Command_Disposed(object sender, EventArgs e)
        //{

        //}

        //private void ClearCommandParams()
        //{
        //    Command.Parameters.Clear();
        //}

        private void SetParameters(DbCommand Command, IDbParms parameters)
        {
            if (parameters == null) return;
            var parms = parameters.GetParms();
            var pv = parameters.GetParmArrary();

            if (parms != null && parms.Length > 0)
                Command.Parameters.AddRange(parms);

            if (pv != null)
            {
                foreach (string key in pv.Keys)
                {
                    var cp = Command.CreateParameter();
                    cp.ParameterName = key;
                    cp.Value = pv[key];
                    Command.Parameters.Add(cp);
                }
            }
        }


        #region 基本查询
        private int ExecuteNonQuery(CommandType CommandType, string sql, IDbParms parameters)
        {
            using (var Command = PrepareCommand(sql, parameters, CommandType))
            {
                int query = Command.ExecuteNonQuery();
                return query;
            }
        }

        private List<T> ExecuteDataList<T>(CommandType CommandType, string sql, IDbParms parameters, Func<DbDataReader, T> action)
        {
            using (var Command = PrepareCommand(sql, parameters, CommandType))
            {
                List<T> lst = new List<T>();
                using (var dr = Command.ExecuteReader())
                {
                    while (dr.Read())
                        lst.Add(action.Invoke(dr));
                    //yield return action.Invoke(dr);
                }
                //ClearCommandParams();
                return lst;
            }

        }

        private List<T> ExecuteDataList<T>(CommandType CommandType, string sql, IDbParms parameters) where T : new()
        {
            using (var Command = PrepareCommand(sql, parameters, CommandType))
            {
                List<T> lst = new List<T>();
                using (var dr = Command.ExecuteReader())
                {
                    while (dr.Read())
                        lst.Add(dr.ToObject<T>());
                    //yield return action.Invoke(dr);
                }
                //ClearCommandParams();
                return lst;
            }

        }

        private T ExecuteDataFirst<T>(CommandType CommandType, string sql, IDbParms parameters, Func<DbDataReader, T> action)
        {
            using (var Command = PrepareCommand(sql, parameters, CommandType))
            {
                T result = default(T);
                using (var dr = Command.ExecuteReader())
                {
                    if (dr.Read())
                        result = action.Invoke(dr);
                    //yield return action.Invoke(dr);
                }
                //ClearCommandParams();
                return result;
            }

        }

        private T ExecuteDataFirst<T>(CommandType CommandType, string sql, IDbParms parameters) where T : new()
        {
            using (var Command = PrepareCommand(sql, parameters, CommandType))
            {
                T result = default(T);
                using (var dr = Command.ExecuteReader())
                {
                    if (dr.Read())
                        result = dr.ToObject<T>();
                    //yield return action.Invoke(dr);
                }
                //ClearCommandParams();
                return result;
            }

        }



        private void ExecuteDataReader(CommandType CommandType, string sql, IDbParms parameters, Action<DbDataReader> action)
        {
            using (var Command = PrepareCommand(sql, parameters, CommandType))
            {
                using (var dr = Command.ExecuteReader())
                {

                    while (dr.Read())
                        action.Invoke(dr);
                }
                //ClearCommandParams();
            }
        }

        private T ExecuteScalar<T>(CommandType CommandType, string sql, IDbParms parameters)
        {
            using (var Command = PrepareCommand(sql, parameters, CommandType))
            {
                var v = Command.ExecuteScalar();
                //ClearCommandParams();
                //return (T)v;
                if (v == DBNull.Value || v == null)
                    return default(T);
                else
                    return (T)v;
            }
        }



        private DataTable GetTable(CommandType CommandType, string TableName, string sql, IDbParms parameters)
        {
            using (var DataAdapter = CreateDataAdapter())
            {

                using (var Command = PrepareCommand(sql, parameters, CommandType))
                {
                    DataAdapter.SelectCommand = Command;
                    DataTable dt = new DataTable(TableName);
                    DataAdapter.Fill(dt);
                    //ClearCommandParams();
                    return dt;
                }
            }
        }

        private DataSet GetDataSet(CommandType CommandType, string sql, IDbParms parameters)
        {
            DataSet ds = new DataSet();
            using (var DataAdapter = CreateDataAdapter())
            {
                using (var Command = PrepareCommand(sql, parameters, CommandType))
                {
                    DataAdapter.SelectCommand = Command;
                    DataAdapter.Fill(ds);
                }
                //ClearCommandParams();
                return ds;
            }
        }



        private bool HasRow(CommandType CommandType, string sql, IDbParms parameters)
        {
            using (var Command = PrepareCommand(sql, parameters, CommandType))
            {
                bool success = false;
                using (var dr = Command.ExecuteReader())
                {
                    success = dr.HasRows;
                }
                //ClearCommandParams();
                return success;
            }
        }
        #endregion

        #region SQL语句
        /// <summary>
        /// 执行SQL语句，并返回受影响行数
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql, IDbParms parameters)
        {
            return ExecuteNonQuery(CommandType.Text, sql, parameters);
        }

        /// <summary>
        /// 执行SQL语句，并返回指定对象集合
        /// </summary>
        /// <typeparam name="T">要返回的对象类型</typeparam>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">查询参数</param>
        /// <param name="action">转换委托</param>
        /// <returns></returns>
        [Obsolete("弃用，请改用：ExecuteDataList 方法", true)]
        public List<T> ExecuteDataReader<T>(string sql, IDbParms parameters, Func<DbDataReader, T> action)
        {
            return ExecuteDataList(CommandType.Text, sql, parameters, action);
        }


        /// <summary>
        /// 执行SQL语句，并返回数据集合
        /// </summary>
        /// <typeparam name="T">要返回的对象类型</typeparam>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">查询参数</param>
        /// <param name="action">转换委托</param>
        /// <returns></returns>
        public List<T> ExecuteDataList<T>(string sql, IDbParms parameters, Func<DbDataReader, T> action)
        {
            return ExecuteDataList(CommandType.Text, sql, parameters, action);
        }

        /// <summary>
        /// 执行SQL语句，并返回数据集合
        /// </summary>
        /// <typeparam name="T">要返回的对象类型</typeparam>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        public List<T> ExecuteDataList<T>(string sql, IDbParms parameters) where T : new()
        {
            return ExecuteDataList<T>(CommandType.Text, sql, parameters);
        }
        /// <summary>
        /// 执行SQL语句，返回第一行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public T ExecuteDataFirst<T>(string sql, IDbParms parameters, Func<DbDataReader, T> action)
        {
            return ExecuteDataFirst<T>(CommandType.Text, sql, parameters, action);
        }
        /// <summary>
        /// 执行SQL语句，返回第一行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public T ExecuteDataFirst<T>(string sql, IDbParms parameters) where T : new()
        {
            return ExecuteDataFirst<T>(CommandType.Text, sql, parameters);
        }


        /// <summary>
        /// 执行SQL语句，委托处理结果
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">查询参数</param>
        /// <param name="action">处理委托</param>
        public void ExecuteDataReader(string sql, IDbParms parameters, Action<DbDataReader> action)
        {
            ExecuteDataReader(CommandType.Text, sql, parameters, action);
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
            return ExecuteScalar<T>(CommandType.Text, sql, parameters);
        }
        /// <summary>
        /// 执行SQL语句，返回DataTable结构
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="TableName">表名</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        public DataTable GetTable(string sql, string TableName, IDbParms parameters)
        {
            return GetTable(CommandType.Text, TableName, sql, parameters);
        }

        /// <summary>
        /// 执行SQL语句，返回DataSet结构
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        public DataSet GetDataSet(string sql, IDbParms parameters)
        {
            return GetDataSet(CommandType.Text, sql, parameters);
        }


        /// <summary>
        /// 执行SQL语句，判断是否有返回数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public bool HasRow(string sql, IDbParms parameters)
        {
            return HasRow(CommandType.Text, sql, parameters);
        }
        #endregion

        #region 存储过程
        /// <summary>
        ///  执行存储过程，并返回受影响行数
        /// </summary>
        /// <param name="StoredProcedureName">存储过程名称</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        public int ExecuteNonQuerySP(string StoredProcedureName, IDbParms parameters)
        {
            return ExecuteNonQuery(CommandType.StoredProcedure, StoredProcedureName, parameters);
        }

        /// <summary>
        /// 执行存储过程，并返回指定对象集合
        /// </summary>
        /// <typeparam name="T">要返回的对象类型</typeparam>
        /// <param name="StoredProcedureName">存储过程名称</param>
        /// <param name="parameters">查询参数</param>
        /// <param name="action">转换委托</param>
        /// <returns></returns>
        [Obsolete("弃用，请改用：ExecuteDataListSP 方法", true)]
        public List<T> ExecuteDataReaderSP<T>(string StoredProcedureName, IDbParms parameters, Func<DbDataReader, T> action)
        {
            return ExecuteDataList<T>(CommandType.StoredProcedure, StoredProcedureName, parameters, action);
        }

        /// <summary>
        /// 执行存储过程，并返回数据集合
        /// </summary>
        /// <typeparam name="T">要返回的对象类型</typeparam>
        /// <param name="StoredProcedureName">存储过程名称</param>
        /// <param name="parameters">查询参数</param>
        /// <param name="action">转换委托</param>
        /// <returns></returns>
        public List<T> ExecuteDataListSP<T>(string StoredProcedureName, IDbParms parameters, Func<DbDataReader, T> action)
        {
            return ExecuteDataList<T>(CommandType.StoredProcedure, StoredProcedureName, parameters, action);
        }
        /// <summary>
        /// 执行存储过程，并返回数据集合
        /// </summary>
        /// <typeparam name="T">要返回的对象类型</typeparam>
        /// <param name="StoredProcedureName">存储过程名称</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        public List<T> ExecuteDataListSP<T>(string StoredProcedureName, IDbParms parameters) where T : new()
        {
            return ExecuteDataList<T>(CommandType.StoredProcedure, StoredProcedureName, parameters);
        }

        /// <summary>
        /// 执行存储过程，返回第一行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="StoredProcedureName"></param>
        /// <param name="parameters"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public T ExecuteDataFirstSP<T>(string StoredProcedureName, IDbParms parameters, Func<DbDataReader, T> action)
        {
            return ExecuteDataFirst<T>(CommandType.StoredProcedure, StoredProcedureName, parameters, action);
        }
        /// <summary>
        /// 执行存储过程，返回第一行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="StoredProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public T ExecuteDataFirstSP<T>(string StoredProcedureName, IDbParms parameters) where T : new()
        {
            return ExecuteDataFirst<T>(CommandType.StoredProcedure, StoredProcedureName, parameters);
        }

        /// <summary>
        /// 执行存储过程，委托处理结果
        /// </summary>
        /// <param name="StoredProcedureName">存储过程名称</param>
        /// <param name="parameters"></param>
        /// <param name="action"></param>
        public void ExecuteDataReaderSP(string StoredProcedureName, IDbParms parameters, Action<DbDataReader> action)
        {
            ExecuteDataReader(CommandType.StoredProcedure, StoredProcedureName, parameters, action);
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
            return ExecuteScalar<T>(CommandType.StoredProcedure, StoredProcedureName, parameters);
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
            return GetTable(CommandType.StoredProcedure, TableName, StoredProcedureName, parameters);
        }

        /// <summary>
        /// 执行存储过程，返回DataSet结构
        /// </summary>
        /// <param name="StoredProcedureName">存储过程名称</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        public DataSet GetDataSetSP(string StoredProcedureName, IDbParms parameters)
        {
            return GetDataSet(CommandType.StoredProcedure, StoredProcedureName, parameters);
        }


        /// <summary>
        /// 执行存储过程，判断是否有返回数据
        /// </summary>
        /// <param name="StoredProcedureName">存储过程名称</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        public bool HasRowSP(string StoredProcedureName, IDbParms parameters)
        {
            return HasRow(CommandType.StoredProcedure, StoredProcedureName, parameters);
        }

        #endregion

        #region 存储过程 IProcedure
        /// <summary>
        ///  执行存储过程，并返回受影响行数
        /// </summary>
        /// <param name="procedure">查询参数</param>
        /// <returns></returns>
        public int ExecuteNonQuerySP(IProcedure procedure)
        {
            return ExecuteNonQuery(CommandType.StoredProcedure, procedure.SPName, procedure.GetParameters());
        }


        /// <summary>
        /// 执行存储过程，并返回数据集合
        /// </summary>
        /// <typeparam name="T">要返回的对象类型</typeparam>
        /// <param name="procedure">查询参数</param>
        /// <param name="action">转换委托</param>
        /// <returns></returns>
        public List<T> ExecuteDataListSP<T>(IProcedure procedure, Func<DbDataReader, T> action)
        {
            return ExecuteDataList<T>(CommandType.StoredProcedure, procedure.SPName, procedure.GetParameters(), action);
        }
        /// <summary>
        /// 执行存储过程，并返回数据集合
        /// </summary>
        /// <typeparam name="T">要返回的对象类型</typeparam>
        /// <param name="procedure">查询参数</param>
        /// <returns></returns>
        public List<T> ExecuteDataListSP<T>(IProcedure procedure) where T : new()
        {
            return ExecuteDataList<T>(CommandType.StoredProcedure, procedure.SPName, procedure.GetParameters());
        }

        /// <summary>
        /// 执行存储过程，返回第一行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procedure">查询参数</param>
        /// <param name="action"></param>
        /// <returns></returns>
        public T ExecuteDataFirstSP<T>(IProcedure procedure, Func<DbDataReader, T> action)
        {
            return ExecuteDataFirst<T>(CommandType.StoredProcedure, procedure.SPName, procedure.GetParameters(), action);
        }
        /// <summary>
        /// 执行存储过程，返回第一行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procedure">查询参数</param>
        /// <returns></returns>
        public T ExecuteDataFirstSP<T>(IProcedure procedure) where T : new()
        {
            return ExecuteDataFirst<T>(CommandType.StoredProcedure, procedure.SPName, procedure.GetParameters());
        }

        /// <summary>
        /// 执行存储过程，委托处理结果
        /// </summary>
        /// <param name="procedure">查询参数</param>
        /// <param name="action"></param>
        public void ExecuteDataReaderSP(IProcedure procedure, Action<DbDataReader> action)
        {
            ExecuteDataReader(CommandType.StoredProcedure, procedure.SPName, procedure.GetParameters(), action);
        }

        /// <summary>
        /// 执行存储过程，返回第一行第一列
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="procedure">查询参数</param>
        /// <returns></returns>
        public T ExecuteScalarSP<T>(IProcedure procedure)
        {
            return ExecuteScalar<T>(CommandType.StoredProcedure, procedure.SPName, procedure.GetParameters());
        }
        /// <summary>
        /// 执行存储过程，返回DataTable结构
        /// </summary>
        /// <param name="procedure">查询参数</param>
        /// <param name="TableName">表名</param>
        /// <returns></returns>
        public DataTable GetTableSP(IProcedure procedure, string TableName)
        {
            return GetTable(CommandType.StoredProcedure, TableName, procedure.SPName, procedure.GetParameters());
        }

        /// <summary>
        /// 执行存储过程，返回DataSet结构
        /// </summary>
        /// <param name="procedure">查询参数</param>
        /// <returns></returns>
        public DataSet GetDataSetSP(IProcedure procedure)
        {
            return GetDataSet(CommandType.StoredProcedure, procedure.SPName, procedure.GetParameters());
        }


        /// <summary>
        /// 执行存储过程，判断是否有返回数据
        /// </summary>
        /// <param name="procedure">查询参数</param>
        /// <returns></returns>
        public bool HasRowSP(IProcedure procedure)
        {
            return HasRow(CommandType.StoredProcedure, procedure.SPName, procedure.GetParameters());
        }

        #endregion

        /// <summary>
        /// 更新表格数据到数据库
        /// </summary>
        /// <param name="dt">数据</param>
        /// <param name="SelectSQL">查询语句</param>
        /// <returns></returns>
        public bool Update(DataTable dt, string SelectSQL)
        {
            return Update(dt, SelectSQL, false, ConflictOption.CompareRowVersion);
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
            return Update(dt, SelectSQL, SetAllValues, ConflictOption.CompareRowVersion);
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
            return Update(dt, SelectSQL, SetAllValues, ConflictOption, CustomerDbDataAdapter);
        }
        /// <summary>
        /// 更新表格数据到数据库
        /// </summary>
        /// <param name="dt">数据</param>
        /// <param name="SelectSQL">查询语句</param>
        /// <param name="SetAllValues">更新所有值，True时更新所有字段，False时只更新有改变的字段</param>
        /// <param name="ConflictOption">指定将如何检测和解决对数据源的相互冲突的更改。</param>
        /// <param name="DbDataAdapterHelper"></param>
        /// <returns></returns>
        public bool Update(DataTable dt, string SelectSQL, bool SetAllValues, ConflictOption ConflictOption, Action<DbDataAdapter> DbDataAdapterHelper)
        {
            using (var Command = PrepareCommand(SelectSQL, null, CommandType.Text))
            {
                try
                {
                    using (var DataAdapter = CreateDataAdapter())
                    {
                        DataAdapter.SelectCommand = Command;
                        using (var cmdBuilder = _ProviderFactory.CreateCommandBuilder())
                        {
                            cmdBuilder.DataAdapter = DataAdapter;
                            DbDataAdapterHelper?.Invoke(DataAdapter);

                            cmdBuilder.SetAllValues = SetAllValues;
                            cmdBuilder.ConflictOption = ConflictOption;

                            cmdBuilder.GetDeleteCommand(true);
                            cmdBuilder.GetInsertCommand(true);
                            cmdBuilder.GetUpdateCommand(true);

                            //var deleteDataAdapter = cmdBuilder.GetDeleteCommand(true);
                            //var insertDataAdapter = cmdBuilder.GetInsertCommand(true);
                            //var updateDataAdapter = cmdBuilder.GetUpdateCommand(true);
                            //if (cmdBuilder.DataAdapter.DeleteCommand == null)
                            //    cmdBuilder.DataAdapter.DeleteCommand = deleteDataAdapter;
                            //if (cmdBuilder.DataAdapter.InsertCommand == null)
                            //    cmdBuilder.DataAdapter.InsertCommand = insertDataAdapter;
                            //if (cmdBuilder.DataAdapter.UpdateCommand == null)
                            //    cmdBuilder.DataAdapter.UpdateCommand = updateDataAdapter;
                            int count = cmdBuilder.DataAdapter.Update(dt.Select("", "", DataViewRowState.Added))//增加
                                        + cmdBuilder.DataAdapter.Update(dt.Select("", "", DataViewRowState.Deleted))//删除
                                        + cmdBuilder.DataAdapter.Update(dt.Select("", "", DataViewRowState.ModifiedCurrent));//修改


                            if (count > 0)
                                dt.AcceptChanges();
                            return true;
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        //public bool UpdateEx(DataTable dt, DataColumnMappingCollection col)
        //{
        //    DataTableMapping a = new DataTableMapping();
        //    DataColumnMapping b = new DataColumnMapping();

        //    dt.Columns[0].ColumnMapping
        //    return true;
        //}



        //private void CommandDatabase_RowUpdating(object sender, SqlRowUpdatingEventArgs e)
        //{
        //    e.Command.CommandText = "";
        //}

        public bool Insert<T>(params T[] objs)
        {
            DataTable dt = ConvertToTable<T>(objs);
            dt.AcceptChanges();
            foreach (DataRow row in dt.Rows)
            {
                row.SetAdded();
            }
            string selectText = GetDataTableSelectText(dt);
            return Update(dt, selectText, true, ConflictOption.CompareAllSearchableValues);
        }

        public bool Update<T>(params T[] objs)
        {
            DataTable dt = ConvertToTable<T>(objs);
            dt.AcceptChanges();
            foreach (DataRow row in dt.Rows)
            {
                row.SetModified();
            }
            string selectText = GetDataTableSelectText(dt);
            //这里SetAllValue只能是true，因为并没有修改列的信息存储
            //ConflictOption.OverwriteChanges，where中只包含主键，不然会引起并发错误
            return Update(dt, selectText, true, ConflictOption.OverwriteChanges);
        }

        public bool Delete<T>(params T[] objs)
        {
            DataTable dt = ConvertToTable<T>(objs);
            dt.AcceptChanges();
            foreach (DataRow row in dt.Rows)
            {
                row.Delete();
            }
            string selectText = GetDataTableSelectText(dt);
            return Update(dt, selectText, true, ConflictOption.CompareAllSearchableValues);
        }





        public string GetDataTableSelectText(DataTable dt)
        {
            StringBuilder str = new StringBuilder();
            foreach (DataColumn col in dt.Columns)
            {
                str.Append("," + col.ColumnName);
            }

            str.Remove(0, 1);
            str.Insert(0, "SELECT ");
            str.Append(" FROM " + dt.TableName);

            return str.ToString();
        }

        public DataTable ConvertToTable<T>(params T[] objs)
        {
            Type t = typeof(T);

            var ts = t.GetCustomAttributes(typeof(SourceTableNameAttribute), true);
            DataTable dataTable = new DataTable();
            if (ts.Length > 0)
            {
                dataTable.TableName = (ts[0] as SourceTableNameAttribute).SourceTableName;
            }
            else
            {
                throw new ArgumentNullException("T类型" + t.FullName + "中没有定义SourceTableNameAttribute特性");
            }



            Dictionary<string, string> mapping = new Dictionary<string, string>();

            PropertyInfo[] pis = t.GetProperties();

            foreach (PropertyInfo propertyInfo in pis)
            {
                var v = propertyInfo.GetCustomAttributes(typeof(SourceColumnNameAttribute), true);
                if (v.Length > 0)
                {
                    var n = (v[0] as SourceColumnNameAttribute);
                    mapping[propertyInfo.Name] = n.SourceColumnName;
                }
                else
                    mapping[propertyInfo.Name] = propertyInfo.Name;
                dataTable.Columns.Add(new DataColumn(mapping[propertyInfo.Name], propertyInfo.PropertyType));
            }


            foreach (object model in objs)
            {
                DataRow dataRow = dataTable.NewRow();
                foreach (PropertyInfo propertyInfo in pis)
                {
                    dataRow[mapping[propertyInfo.Name]] = propertyInfo.GetValue(model, null);
                }
                dataTable.Rows.Add(dataRow);
            }

            return dataTable;

        }


        /// <summary>
        /// 在事物内执行
        /// </summary>
        /// <param name="action"></param>
        public bool ExecuteTransaction(Action<IDatabase> action)
        {
            if (action != null)
                action.Invoke(this);
            return true;
        }

        public bool ExecuteTransaction(Func<IDatabase, bool> action)
        {
            if (action != null)
                return action.Invoke(this);
            return false;
        }
        /// <summary>
        /// 外部在同一链接下执行，如果要获取输出的参数值，用此方法执行，配合GetParamValue执行，或者多次提交
        /// </summary>
        /// <param name="action"></param>
        public void Excute(Action<IDatabase> action)
        {
            if (action != null)
                action.Invoke(this);
        }



        /*
        /// <summary>
        /// 同步数据
        /// </summary>
        /// <param name="table"></param>
        /// <param name="batchSize"></param>
        public void BulkCopy(DataTable table, int batchSize)
        {
            if (Conn is SqlConnection)
            {
                using (var bulkcopy = new SqlBulkCopy((SqlConnection)Conn))
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
            else
            {
                throw new Exception("非SQLServer数据库不允许使用BulkCopy方法");
            }
        }
        /// <summary>
        /// 同步数据
        /// </summary>
        /// <param name="table"></param>
        /// <param name="batchSize"></param>
        public void BulkCopy(DataTable table, List<SqlBulkCopyColumnMapping> ColumnMappings, int batchSize)
        {
            using (var bulkcopy = new SqlBulkCopy((SqlConnection)Conn))
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


        public void Test(params string[] str)
        {
            SqlCommand f;
            //f.Parameters.Add()
            DataColumnMapping cm = new DataColumnMapping("fs", "fs");
        }

        */

        ///// <summary>
        ///// 获取参数值
        ///// </summary>
        ///// <typeparam name="T">返回类型</typeparam>
        ///// <param name="ParameterName">参数名</param>
        ///// <returns></returns>
        //public T GetParamValue<T>(string ParameterName)
        //{
        //    try
        //    {
        //        foreach (DbParameter param in Command.Parameters)
        //        {
        //            if (param.ParameterName == ParameterName)
        //            {
        //                return (T)param.Value;
        //            }
        //        }
        //        return default(T);
        //    }
        //    catch /*(Exception ex)*/
        //    {
        //        return default(T);
        //    }
        //}

    }
}