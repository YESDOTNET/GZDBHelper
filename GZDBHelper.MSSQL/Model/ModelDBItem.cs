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
    /// 包含文件信息
    /// </summary>
    public class ModelDBItem2 : ModelDBItem
    {
        /// <summary>
        /// 数据库文件及大小
        /// </summary>
        public List<FileModel> FileData { get; set; }

        /// <summary>
        /// 数据库文件
        /// </summary>
        public class FileModel
        {
            /// <summary>
            /// 数据库文件
            /// </summary>
            public string FileName { get; set; }

            /// <summary>
            /// 数据库文件路径
            /// </summary>
            public string FileFullName { get; set; }
            /// <summary>
            /// 文件大小（以 8 KB 页为单位）
            /// </summary>
            public long SizeValue { get; set; }
            /// <summary>
            /// 数据库大小MB
            /// </summary>
            public string SizeDescription { get; set; }
        }

    }

    /// <summary>
    /// 数据库对象模型
    /// </summary>
    public class ModelObjectItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ObjectID { get; set; }
        /// <summary>
        /// 对象名称
        /// </summary>
        public string ObjectName { get; set; }
        /// <summary>
        /// 对象类型
        /// </summary>
        public string ObjectType { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime ModifyDate { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

    }
    /// <summary>
    /// 
    /// </summary>
    public class ModelObjectItem2 : ModelObjectItem
    {
        /// <summary>
        /// 数据行数
        /// </summary>
        public int rows { get; set; }
        /// <summary>
        /// 预留大小
        /// </summary>
        public string reserved { get; set; }
        /// <summary>
        /// 数据大小
        /// </summary>
        public string data { get; set; }
        /// <summary>
        /// 索引大小
        /// </summary>
        public string index_size { get; set; }
        /// <summary>
        /// 未使用大小
        /// </summary>
        public string unused { get; set; }
    }

    /// <summary>
    /// 表字段模型
    /// </summary>
    public class ModelObjectColumnItem
    {
        /// <summary>
        /// 表ID
        /// </summary>
        public int ObjectID { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string ObjectName { get; set; }
        /// <summary>
        /// 字段名
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 字段类型ID
        /// </summary>
        public int TypeID { get; set; }
        /// <summary>
        /// 字段类型描述
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        public int MaxLength { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Precision { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Scale { get; set; }
        /// <summary>
        /// 字段描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 是否为Timestamp字段
        /// </summary>
        public bool IsTimestamp { get; set; }
        /// <summary>
        /// 是否自增字段
        /// </summary>
        public bool IsIdentity { get; set; }
        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsPK { get; set; }

        /// <summary>
        /// 是否外键
        /// </summary>
        public bool IsFK { get; set; }
    }
}
