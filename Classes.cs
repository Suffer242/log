using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class comp : IEqualityComparer<fileInfo>
{
    public bool Equals(fileInfo x, fileInfo y)
    {
        return x.RelativeName==y.RelativeName && x.Length==y.Length && x.CreateDate==y.CreateDate;
    }

    public int GetHashCode(fileInfo obj)
    {
       // return obj.relativeName.GetHashCode()+obj.length.GetHashCode()+obj.createDate.GetHashCode();
        return obj.CreateDate.GetHashCode() ^ obj.RelativeName.GetHashCode() ^ obj.Length.GetHashCode();
    }
}
public class File {

    public File(string rootdir)
    {
          rootDir = Path.GetFullPath(rootdir); if (!rootDir.EndsWith('\\')) rootDir+='\\';
          var len  = rootDir.Length;
          var di = new DirectoryInfo(rootDir);
          files =  di.GetFiles("*.*",SearchOption.AllDirectories)
          .Select(fileInfo => new fileInfo(fileInfo.FullName.Substring(len),fileInfo.LastWriteTime,fileInfo.Length)).ToArray();
    }
    public string rootDir {get; private set;}
    public fileInfo[] files {get; private set;}

    public IEnumerable<fileInfo> GetMissingFiles(File withFile)
    {
        var temp = new HashSet<fileInfo>(files,new comp());
        temp.ExceptWith(withFile.files);

         return temp.ToArray();
    }
}

public class fileInfo {
    public fileInfo(string relativeName,DateTime  createDate ,long  length )
    {
        RelativeName = relativeName;
        CreateDate = createDate;
        Length = length;
    }
    public  string RelativeName {get;private set;}
    public  DateTime  CreateDate {get;private set;}
    public  long  Length {get;private set;}

    public override string ToString()
    {
        return $"{RelativeName} - {CreateDate}.{CreateDate.Millisecond:d3} - {Length}";
    }

    public (int,int) GetInts()
    {
        return (10,20);
    }

}


