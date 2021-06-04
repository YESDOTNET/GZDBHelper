using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GZDBHelper
{
    internal class _log
    {
        internal static void LogException(Exception ex)
        {
            Add("GZFramework.Exception.log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + System.Environment.NewLine + ex.ToString());
        }
        /// <summary>
        /// 写日志，指定日志文件
        /// </summary>
        /// <param name="File"></param>
        /// <param name="Msg"></param>
        static void Add(string File, string Msg)
        {

            try
            {
                string logFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log", File);
                string path = Path.GetDirectoryName(logFile);
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                    System.IO.File.CreateText(logFile).Dispose();
                }
                else if (!System.IO.File.Exists(logFile))
                {
                    System.IO.File.CreateText(logFile).Dispose();
                }
                using (TextWriter writer2 = System.IO.File.AppendText(logFile))
                {
                    writer2.WriteLine(Msg);
                    writer2.WriteLine(" ");
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
