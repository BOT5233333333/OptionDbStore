using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OptionDbStore
{
    class Program
    {
        static void Main(string[] args)
        {
            //string rootPath = @"F:\个股期权\option\";
            //string outputDir = @"F:\个股期权\output\";

            //OptionFileProcess ofp = new OptionFileProcess(rootPath);
            //ofp.FileProcess(outputDir);

            //路径必须不能带有中文字符
            string rootPath = @"F:\option";
            DirectoryInfo root = new DirectoryInfo(rootPath);
            AppHelper.numAllFiles = 0;
            foreach (var yearDir in root.GetDirectories())
            {
                AppHelper.numAllFiles += yearDir.GetFiles().Length;
            }

            SPTxtToSqlClass mainFunc = new SPTxtToSqlClass(rootPath, AppHelper.numAllFiles);
            //SPTxtToSqlClass mainFunc = new SPTxtToSqlClass(rootPath, "STHisDBTick_deng", AppHelper.numAllFiles);
            mainFunc.MainFunc();
        }
    }
}
