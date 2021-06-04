//using System;
//using System.Data.Common;
//using System.Collections;

//namespace GZFramework.DB.Core
//{
//    internal static class DbCommandExtensions1
//    {
//        public static void SetParameters(this DbCommand cmd, object parameters)
//        {
//            cmd.Parameters.Clear();

//            if (parameters == null)
//                return;
//            if (parameters is ParametersProvide)
//            {
//                var listed = (ParametersProvide)parameters;
//                foreach (var parameter in listed.Parameters)
//                {
//                    //AddParameter(cmd, parameter._ParameterName, parameter._Size, parameter._Value);
//                    AddParameter(cmd, parameter);
//                }
//                return;
//            }
//            if (parameters is IDbParms)
//            {
//                cmd.Parameters.AddRange((parameters as IDbParms).GetParms());
//                return;
//            }

//            SetParameters_Dictionary(cmd, parameters);
//        }

//        private static void SetParameters_Dictionary(DbCommand cmd, object parameters)
//        {
//            if (parameters is IDictionary)
//            {
//                var listed = (IDictionary)parameters;
//                foreach (var Key in listed.Keys)
//                {
//                    AddParameter(cmd, Key.ToString(), listed[Key]);
//                }
//            }
//            else
//            {
//                SetParameters_Type(cmd, parameters);
//            }
//        }
//        private static void SetParameters_Type(DbCommand cmd, object parameters)
//        {
//            var t = parameters.GetType();
//            var parameterInfos = t.GetProperties();
//            foreach (var pi in parameterInfos)
//            {
//                object value = pi.GetValue(parameters, null);

//                if (pi.PropertyType == typeof(System.DateTime) && Object.Equals(value, DateTime.MinValue))
//                    value = DateTime.Parse("1900-01-01");

//                AddParameter(cmd, pi.Name, value);
//            }
//        }


//        private static void AddParameter(DbCommand cmd, string name, object value)
//        {
//            var p = cmd.CreateParameter();
//            p.ParameterName = name;
//            p.Value = value ?? DBNull.Value;
//            cmd.Parameters.Add(p);
//        }

//        private static void AddParameter(DbCommand cmd, UserParameter parameter)
//        {
//            var p = cmd.CreateParameter();
//            //parameter._ParameterName, parameter._Size, parameter._Value
//            p.ParameterName = parameter._ParameterName;
//            p.Value = parameter._Value ?? DBNull.Value;
//            p.Size = parameter._Size;
//            if ((int)parameter._Type > 0)
//                p.DbType = parameter._Type;
//            p.Direction = parameter._ParameterDirection;
//            cmd.Parameters.Add(p);
//        }

//        private static void AddParameter(DbCommand cmd, string name, int Size, object value)
//        {
//            var p = cmd.CreateParameter();
//            p.ParameterName = name;
//            p.Value = value ?? DBNull.Value;
//            p.Size = Size;
//            cmd.Parameters.Add(p);
//        }
//    }
//}