namespace LackingPlatforms
{
    using System;
    using System.IO;

    public static class ErrorReporter
    {
        private static void CheckLogFile()
        {
            if (!File.Exists("Error.txt"))
            {
                StreamWriter sw = File.CreateText("Error.txt");
                sw.Close();
                sw.Dispose();
            }
        }

        public static void LogException(string[] Exceptionstring)
        {
            CheckLogFile();
            using (StreamWriter sw = File.AppendText("Error.txt"))
            {
                sw.WriteLine("-------------------------");
                sw.WriteLine("Error Report: " + DateTime.Now);
                sw.WriteLine("");
                foreach (string s in Exceptionstring)
                {
                    sw.WriteLine(s);
                }
                sw.WriteLine("");
                sw.WriteLine("-------------------------");
                sw.WriteLine("");
                sw.WriteLine("");
                sw.Close();
                sw.Dispose();
            }
        }
    }
}

