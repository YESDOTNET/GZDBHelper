using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace GZDBHelper
{
    /// <summary>
    /// ��ѯ�����ṩ��ӿ�
    /// </summary>
    public interface IDbParms
    {
        /// <summary>
        /// ���ز�������
        /// </summary>
        /// <returns></returns>
        DbParameter[] GetParms();
        /// <summary>
        /// ���ؼ�ֵ�ԣ��������Ͳ���ֵ
        /// </summary>
        /// <returns></returns>
        Dictionary<string, object> GetParmArrary();
        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="parameterName">��������</param>
        /// <param name="value">����ֵ</param>
        void AddParameter(string parameterName, object value);

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="parameterName">��������</param>
        /// <param name="value">����ֵ</param>
        /// <param name="direction">����Direction</param>
        void AddParameter(string parameterName, object value, ParameterDirection direction);

    }
}
