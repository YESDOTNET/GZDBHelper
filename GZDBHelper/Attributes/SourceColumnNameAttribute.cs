using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GZDBHelper.Attributes
{
    /// <summary>
    /// 数据源映射
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class SourceColumnNameAttribute : Attribute
    {
        /// <summary>
        /// 数据源列名
        /// </summary>
        public string SourceColumnName { get; private set; }
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="ColumnName">数据源列名</param>
        public SourceColumnNameAttribute(string ColumnName)
        {
            SourceColumnName = ColumnName;
        }
    }
}
