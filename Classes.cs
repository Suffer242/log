using System;
using System.IO;
using System.Linq;

public class File {

    public File(string rootdir)
    {
          rootDir = rootdir; if (!rootDir.EndsWith('\\')) rootDir+='\\';
          var len  = rootdir.Length;
          var di = new DirectoryInfo(rootDir);
          files =  di.GetFiles("*.*",SearchOption.AllDirectories)
          .Select(fileInfo => new fileInfo { relativeName = fileInfo.FullName.Substring(len), length = fileInfo.Length, createDate = fileInfo.LastWriteTime}).ToArray();
    }
    public string rootDir {get; private set;}
    public fileInfo[] files {get; private set;}

    public fileInfo[] GetMissingFiles(File withFile)
    {
        return withFile.files.Where(f=>files.FirstOrDefault(fl=>fl.relativeName == f.relativeName && fl.length==f.length && fl.createDate==f.createDate )==null).ToArray();
    }
}

public class fileInfo {

 
    public  string relativeName;
    public  DateTime  createDate; 
    public  long  length;

    public override string ToString()
    {
        return $"{relativeName} - {createDate} - {length}";
    }
}
