using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class comp : IEqualityComparer<fileInfo>
{
    public bool Equals(fileInfo x, fileInfo y)
    {
        return x.relativeName==y.relativeName && x.length==y.length && x.createDate==y.createDate;
    }

    public int GetHashCode(fileInfo obj)
    {
       // return obj.relativeName.GetHashCode()+obj.length.GetHashCode()+obj.createDate.GetHashCode();
        return obj.ToString().GetHashCode();
    }
}
public class File {

    public File(string rootdir)
    {
          rootDir = rootdir; if (!rootDir.EndsWith('\\')) rootDir+='\\';
          var len  = rootdir.Length;
          var di = new DirectoryInfo(rootDir);
          files =  di.GetFiles("*.*",SearchOption.AllDirectories)
          .Select(fileInfo => new fileInfo { relativeName = fileInfo.FullName.Substring(len), length = fileInfo.Length, createDate = fileInfo.LastWriteTime}).ToHashSet(new comp());
    }
    public string rootDir {get; private set;}
    public HashSet<fileInfo> files {get; private set;}

    public IEnumerable<fileInfo> GetMissingFiles(File withFile)
    {
        var temp = new HashSet<fileInfo>(files,new comp());
        temp.ExceptWith(withFile.files);

         return temp.ToArray();
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
    public override int GetHashCode()
    {
        return relativeName.GetHashCode() + createDate.GetHashCode()+length.GetHashCode();
    }
}
