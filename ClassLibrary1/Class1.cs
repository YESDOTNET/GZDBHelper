using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace ClassLibrary1
{
    public static class Class1
    {
        /// <summary>
        /// 获得字段值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="render"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T GetFieldValue<T>(this DbDataReader render, string name)
        {

            int ordinal = render.GetOrdinal(name);
            if (ordinal >= 0)
            {
                return (T)render.GetValue(ordinal);
            }
            return default(T);
        }
    }
}
