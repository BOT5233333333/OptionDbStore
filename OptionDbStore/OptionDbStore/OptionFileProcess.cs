using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OptionDbStore
{
    class OptionFileProcess
    {
        string sourceDir;
        string outputDir;

        public OptionFileProcess(string sourceDir)
        {
            this.sourceDir = sourceDir;
        }

        public void FileProcess(string outputDir)
        {
            foreach(var yearDir in new DirectoryInfo(this.sourceDir).GetDirectories())
            {
                foreach(var file in yearDir.GetFiles())
                {
                    FileStream fs = file.OpenRead();
                    StreamReader sr = new StreamReader(fs, Encoding.UTF8);
                    string line = null;
                    List<string> contents = new List<string>();
                    while((line = sr.ReadLine())!=null)
                    {
                        string[] list = line.Split(',');
                        if(list[1].Length == 5)
                        {
                            list[1] = list[1].Insert(0, "0");
                        }
                        DateTime dt = new DateTime(
                            int.Parse(file.Name.Substring(0, 4))
                            , int.Parse(file.Name.Substring(4, 2))
                            , int.Parse(file.Name.Substring(6, 2))
                            , int.Parse(list[1].Substring(0, 2))
                            , int.Parse(list[1].Substring(2, 2))
                            , int.Parse(list[1].Substring(4, 2)));
                        list[1] = dt.ToString();
                        string content = "";
                        foreach(var item in list)
                        {
                            content += item + ",";
                        }
                        contents.Add(content.TrimEnd(','));
                    }
                    fs.Dispose();
                    sr.Dispose();
                    File.WriteAllLines(this.outputDir + yearDir + "\\" + file.Name, contents);
                }
            }
        }
    }
}
