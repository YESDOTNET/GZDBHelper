using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace GZDBHelper
{
    /// <summary>
    /// 
    /// </summary>
    public static class DbDataReaderExtensions
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
#if NET40
                return (T)render.GetValue(ordinal);
#else
                return render.GetFieldValue<T>(ordinal);
#endif
            }
            return default(T);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="render"></param>
        /// <returns></returns>
        public static T ToObject<T>(this DbDataReader render) where T : new()
        {
            var flags = System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.Instance
                | System.Reflection.BindingFlags.IgnoreCase;

            Type t = typeof(T);
            T data = new T();
            for (int i = 0; i < render.FieldCount; i++)
            {
                string name = render.GetName(i);
                var p = t.GetProperty(name, flags);
                if (p != null)
                    p.SetValue(data, CheckType(render[i], p.PropertyType), null);
            }

            return data;

        }


        // <summary>
        /// 对可空类型进行判断转换(*要不然会报错)
        /// </summary>
        /// <param name="value">DataReader字段的值</param>
        /// <param name="conversionType">该字段的类型</param>
        /// <returns></returns>
        private static object CheckType(object value, Type conversionType)
        {
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null || value==DBNull.Value)
                    return null;
                System.ComponentModel.NullableConverter nullableConverter = new System.ComponentModel.NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }
            return Convert.ChangeType(value, conversionType);
        }
    }
}
