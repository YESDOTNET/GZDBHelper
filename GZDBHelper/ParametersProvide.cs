using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GZFramework.DB.Core
{
    public class ParametersProvide
    {
        public static ParametersProvide New()
        {
            return new ParametersProvide();
        }

        List<UserParameter> lst;
        public ParametersProvide()
        {
            lst = new List<UserParameter>();
        }

        public IEnumerable<UserParameter> Parameters
        {
            get
            {
                return lst.AsEnumerable();
            }
        }

        public void AddParameter(string ParameterName, object Value)
        {
            lst.Add(new UserParameter(ParameterName, Value));

        }
        public void AddParameter(string ParameterName, int Size, object Value)
        {
            lst.Add(new UserParameter(ParameterName, Value, Size));
        }
        public void AddParameter(string ParameterName, DbType type, int Size, object Value, ParameterDirection dir)
        {
            lst.Add(new UserParameter(ParameterName, Value, Size, type, dir));
        }


    }


    public class UserParameter
    {
        public string _ParameterName { get; set; }
        public object _Value { get; set; }
        public int _Size { get; set; }

        public DbType _Type { get; set; }
        public ParameterDirection _ParameterDirection { get; set; }

        public UserParameter(string ParameterName, object Value, int Size = 0)
        {
            _ParameterName = ParameterName;
            _Value = Value;
            _Size = Size;

            _ParameterDirection = ParameterDirection.Input;
        }
        public UserParameter(string ParameterName, object Value, int Size, DbType Type, ParameterDirection ParameterDirection)
        {
            _ParameterName = ParameterName;
            _Value = Value;
            _Size = Size;
            _Type = Type;
            _ParameterDirection = ParameterDirection;
        }
    }
}
