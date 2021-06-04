using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GZDBHelper
{
    public class ConvertTools
    {

        public int ID { get; set; }
        /// <summary>
        /// 实体类转换成DataTable
        /// </summary>
        /// <param name="modelList">实体类列表</param>
        /// <returns></returns>
        public static DataTable ConvertToDataTable<T>(params T[] modelList)
        {
            if (modelList == null || modelList.Length == 0)
            {
                return null;
            }
            DataTable dt = CreateData(typeof(T));

            Type t = modelList[0].GetType();
            PropertyInfo[] pis = t.GetProperties();

            foreach (object model in modelList)
            {
                DataRow dataRow = dt.NewRow();
                foreach (PropertyInfo propertyInfo in pis)
                {
                    dataRow[propertyInfo.Name] = propertyInfo.GetValue(model, null);
                }
                dt.Rows.Add(dataRow);
            }

            return dt;
        }


        /// <summary>
        /// 根据实体类得到表结构
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        private static DataTable CreateData(Type t)
        {
            DataTable dataTable = new DataTable(t.Name);
            foreach (PropertyInfo propertyInfo in t.GetProperties())
            {
                dataTable.Columns.Add(new DataColumn(propertyInfo.Name, propertyInfo.PropertyType));
            }
            return dataTable;
        }
    }


}
