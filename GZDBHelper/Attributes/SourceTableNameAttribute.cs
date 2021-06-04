using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace GZDBHelper.Attributes
{
    /// <summary>
    /// ���ݱ�ӳ��
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class SourceTableNameAttribute : Attribute
    {
        /// <summary>
        /// ���ݿ��Ӧ����
        /// </summary>
        public string SourceTableName { get; private set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="TableName">����Դ����</param>
        public SourceTableNameAttribute(string TableName)
        {
            SourceTableName = TableName;
        }
    }
}
