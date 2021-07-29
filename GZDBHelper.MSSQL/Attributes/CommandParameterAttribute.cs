using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace GZDBHelper.Attributes
{
    /// <summary>
    /// 命令参数
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CommandParameterAttribute : Attribute
    {
        /// <summary>
        /// 参数名称,不包含@符号
        /// </summary>
        public string SPName { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public SqlDbType DbType { get; private set; }
        /// <summary>
        /// 长度
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// 参数类型
        /// </summary>
        public ParameterDirection Direction { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="SPName">参数名称，不包含@符号</param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        /// <param name="direction"></param>
        public CommandParameterAttribute(string SPName,SqlDbType dbType, int size, ParameterDirection direction = ParameterDirection.Input)
        {
            this.DbType = dbType;
            this.Size = size;
            this.Direction = direction;
        }

    }
}
