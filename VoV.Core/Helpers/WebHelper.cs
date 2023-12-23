using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VoV.Core.Helpers
{
    public class WebHelper
    {
        public Tuple<string, string, string, DateTime?> GetLatestFileDetailsFromFolder(string physicalFolderPath)
        {
            Tuple<string, string, string, DateTime?> file = new Tuple<string, string, string, DateTime?>(string.Empty, string.Empty, string.Empty, null);
            string fileDestPath = string.Empty;
            DirectoryInfo dirInfo = new DirectoryInfo(physicalFolderPath);
            DateTime? fileCreatedOn = null;
            if (Directory.Exists(physicalFolderPath))
            {
                FileInfo fInfo = dirInfo.GetFiles().OrderByDescending(f => f.LastWriteTime).FirstOrDefault();
                if (fInfo != null)
                {
                    fileDestPath = physicalFolderPath + fInfo.Name;
                    fileCreatedOn = File.GetLastWriteTime(fileDestPath);
                    file = Tuple.Create(fileDestPath, fInfo.Name, fInfo.Extension, fileCreatedOn);
                }
            }
            return file;
        }

        public static string GetEnumDescription(System.Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}
