using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Web;
using RDotNet;

namespace ProjectoSeminario.Models
{
    public class RToExcel
    {
        private static void InitPath()
        {
            var oldPath = System.Environment.GetEnvironmentVariable("PATH");
            var rPath = System.Environment.Is64BitProcess ? @"C:\Program Files\R\R-3.0.3\bin\x64" : @"C:\Program Files\R\R-3.0.3\bin\i386";
            if (Directory.Exists(rPath) == false)
                throw new DirectoryNotFoundException(string.Format("Could not found the specified path to the directory containing R.dll: {0}", rPath));
            var newPath = string.Format("{0}{1}{2}", rPath, System.IO.Path.PathSeparator, oldPath);
            System.Environment.SetEnvironmentVariable("PATH", newPath);
            string rHome = "";
            var platform = Environment.OSVersion.Platform;
            switch (platform)
            {
                case PlatformID.Win32NT:
                    break;
                case PlatformID.MacOSX:
                    rHome = "/Library/Frameworks/R.framework/Resources";
                    break;
                case PlatformID.Unix:
                    rHome = "/usr/lib/R";
                    break;
                default:
                    throw new NotSupportedException(platform.ToString());
            }
            if (!string.IsNullOrEmpty(rHome))
                Environment.SetEnvironmentVariable("R_HOME", rHome);
        }

        public static int GetTableExcel()
        {
            InitPath();
            DataFrame dataset;
            REngine engine = REngine.CreateInstance("RDotNet");

            engine.Initialize();
            engine.Evaluate("require(\"XLConnect\",character.only=TRUE)");
            engine.Evaluate("dados = readWorksheet(loadWorkbook(file.choose()),sheet=\"dadosGerais + Doppler\",header=TRUE)");
            dataset = engine.Evaluate("dados").AsDataFrame();

            /*for (int i = 0; i < dataset.ColumnCount; ++i)
            {
                dataGridView1.ColumnCount++;
                dataGridView1.Columns[i].Name = dataset.ColumnNames[i];
            }*/

            /*for (int i = 0; i < dataset.RowCount; ++i)
            {
                dataGridView1.RowCount++;
                dataGridView1.Rows[i].HeaderCell.Value = dataset.RowNames[i];

                for (int k = 0; k < dataset.ColumnCount; ++k)
                {
                    dataGridView1[k, i].Value = dataset[i, k];

                }

            }*/

            return dataset.RowCount;
        }
    }
}