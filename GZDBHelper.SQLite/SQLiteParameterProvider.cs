using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
namespace GZDBHelper.MSSQL
{
    /// <summary>
    /// 查询参数生成基类
    /// </summary>
    public class SQLiteParameterProvider : IDbParms
    {
        /// <summary>
        /// 参数集合
        /// </summary>
        private List<DbParameter> Params;

        /// <summary>
        /// 构造函数
        /// </summary>
        public SQLiteParameterProvider()
        {
            Params = new List<DbParameter>();
        }




        /// <summary>
        /// 获得参数集合
        /// </summary>
        /// <returns></returns>
        public DbParameter[] GetParms()
        {
            return Params.ToArray(); ;
        }

        /// <summary>
        /// 更改查询参数的值
        /// </summary>
        /// <param name="ParamName">要更改的查询参数名称</param>
        /// <param name="Value">新的参数值</param>
        public void SetParmValue(string ParamName, object Value)
        {
            var v = Params.Where(p => p.ParameterName == ParamName).FirstOrDefault();
            if (v != null)
                v.Value = Value;
        }


        /// <summary>
        /// 返回键值对参数 键：参数名，值：参数值
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> GetParmArrary()
        {
            return null;
        }


        /// <summary>
        /// 获取参数值
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="ParameterName">参数名</param>
        /// <returns></returns>
        public T GetParamValue<T>(string ParameterName)
        {
            try
            {
                foreach (DbParameter param in Params)
                {
                    if (param.ParameterName == ParameterName)
                    {
                        return (T)param.Value;
                    }
                }
                return default(T);
            }
            catch /*(Exception ex)*/
            {
                return default(T);
            }
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        public void AddParameter(string parameterName, object value)
        {
            SQLiteParameter parm = new SQLiteParameter(parameterName, value);
            Params.Add(parm);
        }
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="dbType"></param>
        /// <param name="value"></param>
        public void AddParameter(string parameterName, SqlDbType dbType, object value)
        {
            SQLiteParameter parm = new SQLiteParameter(parameterName, dbType);
            parm.Value = value;
            Params.Add(parm);
        }
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        /// <param name="value"></param>
        public void AddParameter(string parameterName, DbType dbType, int size, object value)
        {
            SQLiteParameter parm = new SQLiteParameter(parameterName, dbType, size);
            parm.Value = value;
            Params.Add(parm);
        }
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        /// <param name="value"></param>
        /// <param name="direction"></param>
        public void AddParameter(string parameterName, DbType dbType, int size, object value, ParameterDirection direction)
        {
            SQLiteParameter parm = new SQLiteParameter(parameterName, dbType, size);
            parm.Value = value;
            parm.Direction = direction;
            Params.Add(parm);
        }
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        /// <param name="value"></param>
        /// <param name="sourceColumn"></param>
        public void AddParameter(string parameterName, DbType dbType, int size, object value, string sourceColumn)
        {
            SQLiteParameter parm = new SQLiteParameter(parameterName, dbType, size, sourceColumn);
            parm.Value = value;
            Params.Add(parm);
        }

    }
}
