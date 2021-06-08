using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample_NETCoe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // SQLite 文件位置
            string dbFile = @"C:\Users\XQ-Garson\Desktop\TestDB.db";

            // 生成数据库连接
            var connStr = GZDBHelper.ConnectionStrings.BuildSqliteConnectionString(dbFile);


            // 创建数据库对象
            var db = GZDBHelper.DatabaseFactory.CreateDatabase(SQLiteFactory.Instance, connStr, null);

            // 执行SQL获取数据
            DataTable data = db.GetTable("select * from tb_test", "tb_test", null);
        }
    }
}
