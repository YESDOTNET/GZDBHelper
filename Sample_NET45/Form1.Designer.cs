
namespace Sample_NET45
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.btn_testSQLite = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_MSSQL_Server = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt__MSSQL_user = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_MSSQL_pwd = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_MSSQL_dbName = new System.Windows.Forms.TextBox();
            this.btn_testMySQL = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_MySQL_Server = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_MySQL_User = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_MySQL_Pwd = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_MySQL_dbName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_MySQL_Port = new System.Windows.Forms.TextBox();
            this.btn_testOracle = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_Oracle_Host = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txt_Oracle_Port = new System.Windows.Forms.TextBox();
            this.txt_Oracle_User = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txt_Oracle_PWD = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txt_Oracle_DataBaseName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(75, 68);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "测试SQL Server数据库";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_testSQLite
            // 
            this.btn_testSQLite.Location = new System.Drawing.Point(75, 193);
            this.btn_testSQLite.Name = "btn_testSQLite";
            this.btn_testSQLite.Size = new System.Drawing.Size(114, 23);
            this.btn_testSQLite.TabIndex = 1;
            this.btn_testSQLite.Text = "测试SQLite数据库";
            this.btn_testSQLite.UseVisualStyleBackColor = true;
            this.btn_testSQLite.Click += new System.EventHandler(this.btn_testSQLite_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "服务器";
            // 
            // txt_MSSQL_Server
            // 
            this.txt_MSSQL_Server.Location = new System.Drawing.Point(75, 28);
            this.txt_MSSQL_Server.Name = "txt_MSSQL_Server";
            this.txt_MSSQL_Server.Size = new System.Drawing.Size(100, 21);
            this.txt_MSSQL_Server.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(199, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "用户名";
            // 
            // txt__MSSQL_user
            // 
            this.txt__MSSQL_user.Location = new System.Drawing.Point(246, 28);
            this.txt__MSSQL_user.Name = "txt__MSSQL_user";
            this.txt__MSSQL_user.Size = new System.Drawing.Size(100, 21);
            this.txt__MSSQL_user.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(360, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "密码";
            // 
            // txt_MSSQL_pwd
            // 
            this.txt_MSSQL_pwd.Location = new System.Drawing.Point(407, 28);
            this.txt_MSSQL_pwd.Name = "txt_MSSQL_pwd";
            this.txt_MSSQL_pwd.Size = new System.Drawing.Size(100, 21);
            this.txt_MSSQL_pwd.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(521, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "数据库名称";
            // 
            // txt_MSSQL_dbName
            // 
            this.txt_MSSQL_dbName.Location = new System.Drawing.Point(592, 28);
            this.txt_MSSQL_dbName.Name = "txt_MSSQL_dbName";
            this.txt_MSSQL_dbName.Size = new System.Drawing.Size(100, 21);
            this.txt_MSSQL_dbName.TabIndex = 3;
            // 
            // btn_testMySQL
            // 
            this.btn_testMySQL.Location = new System.Drawing.Point(75, 303);
            this.btn_testMySQL.Name = "btn_testMySQL";
            this.btn_testMySQL.Size = new System.Drawing.Size(114, 23);
            this.btn_testMySQL.TabIndex = 1;
            this.btn_testMySQL.Text = "测试MySQL数据库";
            this.btn_testMySQL.UseVisualStyleBackColor = true;
            this.btn_testMySQL.Click += new System.EventHandler(this.btn_testMySQL_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 257);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "服务器";
            // 
            // txt_MySQL_Server
            // 
            this.txt_MySQL_Server.Location = new System.Drawing.Point(75, 253);
            this.txt_MySQL_Server.Name = "txt_MySQL_Server";
            this.txt_MySQL_Server.Size = new System.Drawing.Size(100, 21);
            this.txt_MySQL_Server.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(360, 257);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "用户名";
            // 
            // txt_MySQL_User
            // 
            this.txt_MySQL_User.Location = new System.Drawing.Point(407, 253);
            this.txt_MySQL_User.Name = "txt_MySQL_User";
            this.txt_MySQL_User.Size = new System.Drawing.Size(100, 21);
            this.txt_MySQL_User.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(545, 257);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "密码";
            // 
            // txt_MySQL_Pwd
            // 
            this.txt_MySQL_Pwd.Location = new System.Drawing.Point(592, 253);
            this.txt_MySQL_Pwd.Name = "txt_MySQL_Pwd";
            this.txt_MySQL_Pwd.Size = new System.Drawing.Size(100, 21);
            this.txt_MySQL_Pwd.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(730, 257);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 2;
            this.label8.Text = "数据库名称";
            // 
            // txt_MySQL_dbName
            // 
            this.txt_MySQL_dbName.Location = new System.Drawing.Point(801, 253);
            this.txt_MySQL_dbName.Name = "txt_MySQL_dbName";
            this.txt_MySQL_dbName.Size = new System.Drawing.Size(100, 21);
            this.txt_MySQL_dbName.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(199, 257);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 2;
            this.label9.Text = "端口";
            // 
            // txt_MySQL_Port
            // 
            this.txt_MySQL_Port.Location = new System.Drawing.Point(246, 253);
            this.txt_MySQL_Port.Name = "txt_MySQL_Port";
            this.txt_MySQL_Port.Size = new System.Drawing.Size(100, 21);
            this.txt_MySQL_Port.TabIndex = 3;
            // 
            // btn_testOracle
            // 
            this.btn_testOracle.Location = new System.Drawing.Point(75, 402);
            this.btn_testOracle.Name = "btn_testOracle";
            this.btn_testOracle.Size = new System.Drawing.Size(114, 23);
            this.btn_testOracle.TabIndex = 1;
            this.btn_testOracle.Text = "测试Oracle数据库";
            this.btn_testOracle.UseVisualStyleBackColor = true;
            this.btn_testOracle.Click += new System.EventHandler(this.btn_testOracle_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(28, 356);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 2;
            this.label10.Text = "服务器";
            // 
            // txt_Oracle_Host
            // 
            this.txt_Oracle_Host.Location = new System.Drawing.Point(75, 352);
            this.txt_Oracle_Host.Name = "txt_Oracle_Host";
            this.txt_Oracle_Host.Size = new System.Drawing.Size(100, 21);
            this.txt_Oracle_Host.TabIndex = 3;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(360, 356);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 2;
            this.label11.Text = "用户名";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(199, 356);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 12);
            this.label12.TabIndex = 2;
            this.label12.Text = "端口";
            // 
            // txt_Oracle_Port
            // 
            this.txt_Oracle_Port.Location = new System.Drawing.Point(246, 352);
            this.txt_Oracle_Port.Name = "txt_Oracle_Port";
            this.txt_Oracle_Port.Size = new System.Drawing.Size(100, 21);
            this.txt_Oracle_Port.TabIndex = 3;
            // 
            // txt_Oracle_User
            // 
            this.txt_Oracle_User.Location = new System.Drawing.Point(407, 352);
            this.txt_Oracle_User.Name = "txt_Oracle_User";
            this.txt_Oracle_User.Size = new System.Drawing.Size(100, 21);
            this.txt_Oracle_User.TabIndex = 3;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(545, 356);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 12);
            this.label13.TabIndex = 2;
            this.label13.Text = "密码";
            // 
            // txt_Oracle_PWD
            // 
            this.txt_Oracle_PWD.Location = new System.Drawing.Point(592, 352);
            this.txt_Oracle_PWD.Name = "txt_Oracle_PWD";
            this.txt_Oracle_PWD.Size = new System.Drawing.Size(100, 21);
            this.txt_Oracle_PWD.TabIndex = 3;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(730, 356);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 2;
            this.label14.Text = "数据库名称";
            // 
            // txt_Oracle_DataBaseName
            // 
            this.txt_Oracle_DataBaseName.Location = new System.Drawing.Point(801, 352);
            this.txt_Oracle_DataBaseName.Name = "txt_Oracle_DataBaseName";
            this.txt_Oracle_DataBaseName.Size = new System.Drawing.Size(100, 21);
            this.txt_Oracle_DataBaseName.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(995, 450);
            this.Controls.Add(this.txt_Oracle_DataBaseName);
            this.Controls.Add(this.txt_MySQL_dbName);
            this.Controls.Add(this.txt_MSSQL_dbName);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_Oracle_PWD);
            this.Controls.Add(this.txt_MySQL_Pwd);
            this.Controls.Add(this.txt_MSSQL_pwd);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_Oracle_User);
            this.Controls.Add(this.txt_MySQL_User);
            this.Controls.Add(this.txt_Oracle_Port);
            this.Controls.Add(this.txt_MySQL_Port);
            this.Controls.Add(this.txt__MSSQL_user);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_Oracle_Host);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txt_MySQL_Server);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_MSSQL_Server);
            this.Controls.Add(this.btn_testOracle);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_testMySQL);
            this.Controls.Add(this.btn_testSQLite);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_testSQLite;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_MSSQL_Server;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt__MSSQL_user;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_MSSQL_pwd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_MSSQL_dbName;
        private System.Windows.Forms.Button btn_testMySQL;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_MySQL_Server;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_MySQL_User;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_MySQL_Pwd;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_MySQL_dbName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_MySQL_Port;
        private System.Windows.Forms.Button btn_testOracle;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_Oracle_Host;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txt_Oracle_Port;
        private System.Windows.Forms.TextBox txt_Oracle_User;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txt_Oracle_PWD;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txt_Oracle_DataBaseName;
    }
}

