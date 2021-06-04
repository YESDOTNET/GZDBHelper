using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace GZDBHelper.Attributes
{
    /// <summary>
    /// 数据表映射
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class SourceTableNameAttribute : Attribute
    {
        /// <summary>
        /// 数据库对应表名
        /// </summary>
        public string SourceTableName { get; private set; }
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="TableName">数据源列名</param>
        public SourceTableNameAttribute(string TableName)
        {
            SourceTableName = TableName;
        }
    }
}
