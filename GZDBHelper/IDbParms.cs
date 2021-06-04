using System;
using System.Collections.Generic;
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
 
    }
}
