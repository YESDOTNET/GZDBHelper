namespace GZDBHelper.MSSQL.Model
{
    /// <summary>
    /// 存储过程参数
    /// </summary>
    public class ModelParameterItem
    {
        /// <summary>
        /// 对象ID
        /// </summary>
        public int ObjectID { get; set; }
        /// <summary>
        /// 对象名称
        /// </summary>
        public string ObjectName { get; set; }
        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParamName { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// 是否为OutPut参数
        /// </summary>
        public bool Is_OutPut { get; set; }
    }
}
