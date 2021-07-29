using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GZDBHelper
{
    /// <summary>
    /// 存储过程
    /// </summary>
    public interface IProcedure
    {
        /// <summary>
        /// 存储过程名字
        /// </summary>
        string SPName { get; }
        /// <summary>
        /// 获得存储过程参数
        /// </summary>
        /// <returns></returns>
        IDbParms GetParameters();
    }
}
