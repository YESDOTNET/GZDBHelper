using System;
using System.Collections.Generic;
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
 
    }
}
