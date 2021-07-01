using System.Collections.Generic;
using System.Data;
using System.Linq;
using GZDBHelper.MSSQL.Model;
#if NET40 || NET45 || NET46
using System.Data.SqlClient;
#else
#endif
namespace GZDBHelper
{
    /// <summary>
    /// MSSQL 数据库 集成
    /// </summary>
    public class SQLServerTools
    {
        private IDatabase db;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_db"></param>
        public SQLServerTools(IDatabase _db)
        {
            db = _db;
        }

        /// <summary>
        /// 获得数据库列表
        /// </summary>
        /// <returns></returns>
        public List<ModelDBItem> GetDBList_Simple()
        {
            string sql = "SELECT dbid,name FROM  master..sysdatabases WHERE name NOT IN ( 'master', 'model', 'msdb', 'tempdb', 'northwind','pubs' ) order by name";
            var data = db.ExecuteDataList<ModelDBItem>(sql, null);

            return data;
        }
        /// <summary>
        /// 获得数据库列表，包含数据库文件信息
        /// </summary>
        /// <returns></returns>
        public List<ModelDBItem2> GetDBList_FileInfo()
        {
            string sql = "SELECT dbid,name FROM  master..sysdatabases WHERE name NOT IN ( 'master', 'model', 'msdb', 'tempdb', 'northwind','pubs' ) order by name";
            var data = db.ExecuteDataList<ModelDBItem2>(sql, null);
            db.Excute(_db =>
            {
                foreach (var obj in data)
                {
                    obj.FileData = new List<ModelDBItem2.FileModel>();
                    string sql = $"select * from [{obj.name}].dbo.sysfiles";
                    _db.ExecuteDataReader(sql, null, row =>
                    {
                        int sizeValue = row.GetFieldValue<int>("size");
                        obj.FileData.Add(new ModelDBItem2.FileModel()
                        {
                            FileName = row.GetFieldValue<string>("name"),
                            SizeValue = sizeValue,
                            FileFullName = row.GetFieldValue<string>("filename"),
                            SizeDescription = (sizeValue * (8192.0 / 1024.0) / 1024) + "MB"
                        });
                    });
                }
            });

            return data;
        }



        /// <summary>
        /// 获得数据库对象
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public List<ModelObjectItem> GetObjects_Simple(ObjectType objectType)
        {
            List<string> xtype = new List<string>();
            switch (objectType)
            {
                case ObjectType.Table:
                    xtype.Add("U");
                    break;
                case ObjectType.View:
                    xtype.Add("V");
                    break;
            }
            if (xtype.Count > 0)
            {
                string inStr = string.Join(",", xtype.Select(a => "'" + a + "'"));
                string sql = $@"SELECT a.object_id as ObjectID,a.name as ObjectName,a.type as ObjectType,a.create_date as CreateDate,a.modify_date as ModifyDate,b.value as Description
                    FROM  sys.objects  AS a LEFT JOIN  (SELECT * FROM sys.extended_properties  WHERE name='MS_Description') AS b ON a.object_id=b.major_id AND b.minor_id=0
                    Where xtype in ({inStr}) ORDER BY xtype, name";

                return db.ExecuteDataList<ModelObjectItem>(sql, null);
            }
            else
                return null;
        }
        /// <summary>
        /// 获得数据库对象，包含大小
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public List<ModelObjectItem2> GetObjects_Size(ObjectType objectType)
        {
            List<string> xtype = new List<string>();
            switch (objectType)
            {
                case ObjectType.Table:
                    xtype.Add("U");
                    break;
                case ObjectType.View:
                    xtype.Add("V");
                    break;
            }
            if (xtype.Count > 0)
            {
                string inStr = string.Join(",", xtype.Select(a => "'" + a + "'"));
                string sql = $@"SELECT a.object_id as ObjectID,a.name as ObjectName,a.type as ObjectType,a.create_date as CreateDate,a.modify_date as ModifyDate,b.value as Description
                    FROM  sys.objects  AS a LEFT JOIN  (SELECT * FROM sys.extended_properties  WHERE name='MS_Description') AS b ON a.object_id=b.major_id AND b.minor_id=0
                    Where xtype in ({inStr}) ORDER BY xtype, name";

                var data = db.ExecuteDataList<ModelObjectItem2>(sql, null);

                db.Excute(_db =>
                {
                    foreach (var obj in data)
                    {
                        if (obj.ObjectType == "U")
                        {
                            SqlParameterProvider paramers = new SqlParameterProvider();
                            paramers.AddParameter("@objname", SqlDbType.VarChar, obj.ObjectName);

                            _db.ExecuteDataReaderSP("sp_spaceused", paramers, row =>
                            {
                                obj.rows = row.GetFieldValue<int>("rows");
                                obj.reserved = row.GetFieldValue<string>("reserved");
                                obj.data = row.GetFieldValue<string>("data");
                                obj.index_size = row.GetFieldValue<string>("index_size");
                                obj.unused = row.GetFieldValue<string>("unused");
                            });

                        }
                    }
                });

                return data;
            }
            else
                return null;
        }

        /// <summary>
        /// 获得表结构
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public List<ModelObjectColumnItem> GetObjectColumns(string TableName)
        {
            string sql = @"
                    SELECT  a.object_id AS ObjectID,
                         OBJECT_NAME(a.object_id) AS ObjectName,
                         a.system_type_id AS TypeID,
                         a.name AS ColumnName,
                         c.name AS TypeName,
                         a.max_length AS [MaxLength],
                         a.[precision] AS [Precision],
                         a.scale AS Scale,
                         b.value AS [Description],
                         IsIdentity = a.is_identity
                         --Flag_Edit=CASE WHEN a.is_identity=1 OR c.name='timestamp' THEN 'N' ELSE 'Y' END,
                    
                         --Flag_TS=CASE WHEN c.name='timestamp' THEN 'Y' ELSE 'N' END

                     FROM sys.columns a LEFT JOIN sys.extended_properties b ON b.major_id = a.object_id AND b.minor_id = a.column_id AND b.name = 'MS_Description' 
                         LEFT JOIN sys.types AS c ON c.user_type_id = a.user_type_id
                     WHERE    a.object_id =select object_id from sys.objects where name=@tableName
                     ORDER  BY OBJECT_NAME(a.object_id), a.column_id";




            SqlParameterProvider parameters = new SqlParameterProvider();
            parameters.AddParameter("@tableName", SqlDbType.VarChar, TableName);
            var data = db.ExecuteDataList<ModelObjectColumnItem>(sql, parameters);
            foreach (var obj in data)
            {
                if (string.Equals(obj.TypeName, "timestamp", System.StringComparison.OrdinalIgnoreCase))
                    obj.IsTimestamp = true;
            }

            string sqlKeys = @" SELECT a.TABLE_NAME,a.COLUMN_NAME,b.type
		                FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS a INNER JOIN sys.objects AS b ON a.CONSTRAINT_NAME=b.name
		                WHERE TABLE_NAME=@tableName ";
            db.ExecuteDataReader(sqlKeys, parameters, row =>
            {
                string type = row.GetFieldValue<string>("type");
                string columnName = row.GetFieldValue<string>("COLUMN_NAME");
                if (type == "PK")
                {
                    var obj = data.Where(w => w.ColumnName == columnName).FirstOrDefault();
                    obj.IsPK = true;
                }
                if (type == "F")
                {
                    var obj = data.Where(w => w.ColumnName == columnName).FirstOrDefault();
                    obj.IsFK = true;
                }
            });

            return data;
        }
    }
    /// <summary>
    /// 数据库对象类型
    /// </summary>
    public enum ObjectType
    {
        /// <summary>
        /// 表
        /// </summary>
        Table,
        /// <summary>
        /// 视图
        /// </summary>
        View
    }
}
