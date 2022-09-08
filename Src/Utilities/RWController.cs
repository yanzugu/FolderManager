using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FolderManager.Src.Utilities
{
    class RWController
    {
        private string txtPath = "";

        public RWController(string path)
        {
            txtPath = path;
        }

        public void WriteData(string data)
        {
            using (var sw = new StreamWriter(txtPath))
            {
                sw.WriteLine(data);
            }
        }

        public void AppendData(string data)
        {
            using (var sw = File.AppendText(txtPath))
            {
                sw.WriteLine(data);
            }
        }

        public List<string> ReadData()
        {
            List<string> result = new();
            string s;

            if (File.Exists(txtPath))
            {
                using (var sr = new StreamReader(txtPath))
                {
                    while ((s = sr.ReadLine()) != null)
                    {
                        result.Add(s);
                    }
                }
            }

            return result;
        }
    }
}
