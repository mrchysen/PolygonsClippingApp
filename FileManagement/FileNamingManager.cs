using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement;

public static class FileNamingManager
{
    public static readonly string BasicPathToFiles = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "polygons");

    static FileNamingManager()
    {
        Directory.CreateDirectory(BasicPathToFiles);
        Debug.WriteLine($"Созданы директории по адресу:{BasicPathToFiles}");
    }

    public static string GetNameOfPolygonFile(string FileName, string Path)
    {
        throw new NotImplementedException();
    }
}
