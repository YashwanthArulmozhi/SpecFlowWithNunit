using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SpecFlowWithNunit.Utils
{
    class FileReaderUtils
    {

        public static string ReadDataFromConfigFile(string key)
        {
            
            Dictionary<string, string> Configdata = new Dictionary<string, string>();
            string value = null;
            try
            {
                string filePath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent + @"\Config.txt";
                foreach (string data in File.ReadAllLines(filePath))
                {
                    Configdata.Add(data.Split('=')[0].ToLower().Trim(), data.Split('=')[1].ToUpper().TrimStart().TrimEnd());
                }
                value = Configdata[key.ToLower()];
                if (!String.IsNullOrEmpty(value))
                {
                    return value;
                }
                else
                {
                    throw new Exception("Provide Valid Key Property from Config to get the value");
                }
            }
            catch(FileNotFoundException e)
            {
                Assert.Fail("Exception in File Reading. "+e.InnerException);
            }
            return null;
        }
    }
}
