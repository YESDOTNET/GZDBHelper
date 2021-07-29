using GZDBHelper.Attributes;
using System;
using System.Linq;

namespace GZDBHelper
{


    /// <summary>
    /// 存储过程基类
    /// </summary>
    public class ProcedureBase : IProcedure
    {
        /// <summary>
        /// 存储过程名称
        /// </summary>
        public string SPName { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="spname"></param>
        public ProcedureBase(string spname)
        {
            this.SPName = spname;
        }
        /// <summary>
        /// 获得存储过程参数
        /// </summary>
        /// <returns></returns>
        internal IDbParms GetParameters()
        {
            SqlParameterProvider parameters = new SqlParameterProvider();

            var properties = this.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            foreach (var p in properties)
            {
                CommandParameterAttribute attr = (CommandParameterAttribute)p.GetCustomAttributes(typeof(CommandParameterAttribute), false).FirstOrDefault();
                if (attr == null)
                    continue;

                string SPName = attr.SPName;
                if (String.IsNullOrWhiteSpace(SPName))
                    SPName = p.Name;

                object value = p.GetValue(this, null);

                parameters.AddParameter("@" + SPName, attr.DbType, attr.Size, value, attr.Direction);
            }

            return parameters;
        }

        IDbParms IProcedure.GetParameters()
        {
            throw new NotImplementedException();
        }
    }
}
