using System;
using System.Collections.Generic;
using System.Text;

namespace GZDBHelper.MSSQL.Model
{
    /// <summary>
    /// 数据库列表对象
    /// </summary>
    public class ModelDBItem
    {
        /// <summary>
        /// 数据库编号
        /// </summary>
        public int dbid { get; set; }
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string name { get; set; }
    }
    /// <summary>
    /// 数据库对象模型
    /// </summary>
    public class ModelObjectItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 对象名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 对象类型
        /// </summary>
        public string xtype { get; set; }

    }
}
