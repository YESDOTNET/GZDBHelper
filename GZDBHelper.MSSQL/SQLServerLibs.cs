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
                    string sql2 = $"select * from [{obj.name}].dbo.sysfiles";
                    _db.ExecuteDataReader(sql2, null, row =>
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
                case ObjectType.Procedure:
                    xtype.Add("P");
                    break;
            }
            if (xtype.Count > 0)
            {
                string inStr = string.Join(",", xtype.Select(a => "'" + a + "'"));
                string sql = $@"SELECT a.object_id as ObjectID,a.name as ObjectName,a.type as ObjectType,a.create_date as CreateDate,a.modify_date as ModifyDate,b.value as Description
                    FROM  sys.objects  AS a LEFT JOIN  (SELECT * FROM sys.extended_properties  WHERE name='MS_Description') AS b ON a.object_id=b.major_id AND b.minor_id=0
                    Where a.type in ({inStr}) ORDER BY a.type, a.name";

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
                case ObjectType.Procedure:
                    xtype.Add("P");
                    break;
            }
            if (xtype.Count > 0)
            {
                string inStr = string.Join(",", xtype.Select(a => "'" + a + "'"));
                string sql = $@"SELECT a.object_id as ObjectID,a.name as ObjectName,a.type as ObjectType,a.create_date as CreateDate,a.modify_date as ModifyDate,b.value as Description
                    FROM  sys.objects  AS a LEFT JOIN  (SELECT * FROM sys.extended_properties  WHERE name='MS_Description') AS b ON a.object_id=b.major_id AND b.minor_id=0
                    Where a.type in ({inStr}) ORDER BY a.type, a.name";

                var data = db.ExecuteDataList<ModelObjectItem2>(sql, null);

                db.Excute(_db =>
                {
                    foreach (var obj in data)
                    {
                        obj.ObjectType = obj.ObjectType.Trim();
                        if (obj.ObjectType == "U")
                        {
                            SqlParameterProvider paramers = new SqlParameterProvider();
                            paramers.AddParameter("@objname", SqlDbType.VarChar, obj.ObjectName);

                            _db.ExecuteDataReaderSP("sp_spaceused", paramers, row =>
                            {
                                obj.rows = int.Parse(row.GetFieldValue<string>("rows").Trim());
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
                     WHERE    a.object_id =(select object_id from sys.objects where name=@tableName)
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

            SqlParameterProvider parameters2 = new SqlParameterProvider();
            parameters2.AddParameter("@tableName", SqlDbType.VarChar, TableName);
            db.ExecuteDataReader(sqlKeys, parameters2, row =>
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

        /// <summary>
        /// 获得存储过程或函数的参数
        /// </summary>
        /// <param name="FunctionName"></param>
        /// <returns></returns>
        public List<ModelParameterItem> GetFunctionParameters(string FunctionName)
        {
            string sql = @"SELECT sp.object_Id as ObjectID, sp.name as FunctionName,
            isnull(param.name,'')as ObjectName,isnull(usrt.name,'') AS [DataType],
            ISNULL(baset.name, '') AS [SystemType], CAST(CASE when baset.name is null then 0  WHEN baset.name IN ('nchar', 'nvarchar') AND param.max_length <> -1 THEN param.max_length/2 ELSE param.max_length END AS int) AS [Size],
            '' as ParamReamrk,isnull(parameter_id,0) as SortId,
			is_output=case when param.is_output is null then 0 else param.is_output end
FROM sys.objects AS sp  INNER JOIN sys.schemas b ON sp.schema_id = b.schema_id
            left outer JOIN sys.all_parameters AS param ON param.object_id=sp.object_Id
            LEFT OUTER JOIN sys.types AS usrt ON usrt.user_type_id = param.user_type_id
            LEFT OUTER JOIN sys.types AS baset ON (baset.user_type_id = param.system_type_id and baset.user_type_id = baset.system_type_id) or ((baset.system_type_id = param.system_type_id) and (baset.user_type_id = param.user_type_id) and (baset.is_user_defined = 0) and (baset.is_assembly_type = 1)) 
           LEFT OUTER JOIN sys.extended_properties E ON sp.object_id = E.major_id
WHERE sp.TYPE in ('FN', 'IF', 'TF','P')  AND ISNULL(sp.is_ms_shipped, 0) = 0 AND ISNULL(E.name, '') <> 'microsoft_database_tools_support' and sp.name=@SPName";

            SqlParameterProvider parameters = new SqlParameterProvider();
            parameters.AddParameter("@SPName", SqlDbType.VarChar, FunctionName);
            var data = db.ExecuteDataList<ModelParameterItem>(sql, parameters);
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
        Table = 1,
        /// <summary>
        /// 视图
        /// </summary>
        View = 2,
        /// <summary>
        /// 存储过程
        /// </summary>
        Procedure = 3
    }
}
