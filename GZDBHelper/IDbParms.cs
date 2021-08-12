using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace GZDBHelper
{
    /// <summary>
    /// 查询参数提供类接口
    /// </summary>
    public interface IDbParms
    {
        /// <summary>
        /// 返回参数集合
        /// </summary>
        /// <returns></returns>
        DbParameter[] GetParms();
        /// <summary>
        /// 返回键值对，参数名和参数值
        /// </summary>
        /// <returns></returns>
        Dictionary<string, object> GetParmArrary();
        /// <summary>
        /// 添加命令参数
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="value">参数值</param>
        void AddParameter(string parameterName, object value);

        /// <summary>
        /// 添加命令参数
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="value">参数值</param>
        /// <param name="direction">参数Direction</param>
        void AddParameter(string parameterName, object value, ParameterDirection direction);

    }
}
