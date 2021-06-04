using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GZDBHelper
{
    /// <summary>
    /// 数据库初始化配置接口
    /// </summary>
    public interface IDBConfig
    {
        /// <summary>
        /// 根据数据库别名获取数据库操作IDatabase对象
        /// </summary>
        /// <param name="DBCode"></param>
        /// <returns></returns>
        IDatabase GetDBConnectionInfo(string DBCode);

        /// <summary>
        /// 刷新数据库集合，多数据库项目中使用
        /// </summary>
        void RefreshDBList();
    }
}
