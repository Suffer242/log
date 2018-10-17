using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

class comp : IEqualityComparer<FileInformation>
{
    public bool Equals(FileInformation x, FileInformation y)
    {
        return x.RelativeName==y.RelativeName && x.Length==y.Length && x.CreateDate==y.CreateDate;
    }

    public int GetHashCode(FileInformation obj)
    {
        return obj.CreateDate.GetHashCode() ^ obj.RelativeName.GetHashCode() ^ obj.Length.GetHashCode();
    }
}
public class Master {

    public string RootPath {get; private set;}
    public string Chunkpath {get; private set;}
    public FileInformation[] files {get; private set;}
    public FileInformation[] exclusiveFiles {get; private set;}
    Master _PairFile;


    static bool isMatchFileStructure(FileInformation[] files1, FileInformation[] files2)
    {
        var temp = new HashSet<FileInformation>(files1,new comp());
        temp.SymmetricExceptWith(files2);
        return temp.Count()==0;  
    }

    static String NormPath(String path)
    {
       return Path.GetFullPath(path) + (path.EndsWith('\\') ? "" : "\\");
    }
    static FileInformation[] GetFiles(String path)
    {
         var normpath = NormPath(path);
         var normpathlen = normpath.Length;
         var di = new DirectoryInfo(normpath);
         return di.GetFiles("*.*",SearchOption.AllDirectories)
                .Select(fileInfo => new FileInformation(fileInfo.FullName.Substring(normpathlen),fileInfo.LastWriteTime,fileInfo.Length)).ToArray();   
    }

    public Master(string chunkPath, string rootPath)
    {
          Chunkpath = chunkPath;
          RootPath = NormPath(rootPath);
          files = GetFiles(RootPath);
    }
    FileInformation[] GetSharedFiles()
    {
        var temp = new HashSet<FileInformation>(files,new comp());
        temp.IntersectWith(_PairFile.files);     
        return temp.ToArray(); 
    }
    public void Save()
    {
          var temp = JsonConvert.SerializeObject(this);
          File.WriteAllText(Path.Combine(Chunkpath,".master.json"), temp);
    }


    public void MakeChunk()
    {
        var currentFiles = GetFiles(RootPath);

        var ReadyForCopy  = isMatchFileStructure(files,currentFiles);

        if (!ReadyForCopy) throw new Exception("Структура файлов не соответствует");

        foreach (var file in exclusiveFiles)
        {
            File.Move(Path.Combine(RootPath,file.RelativeName), Path.Combine(Chunkpath,file.RelativeName));
        }
    }

    public void Activate(String DstPath)
    {
        var dstFilesBefore = GetFiles(DstPath);
        var ReadyForCopy  = isMatchFileStructure(GetSharedFiles(),dstFilesBefore);

        if (!ReadyForCopy) throw new Exception("Структура файлов целевого пути нарушена");

        foreach (var file in exclusiveFiles)
        {
            File.Move(Path.Combine(Chunkpath,file.RelativeName),Path.Combine(DstPath,file.RelativeName));
        }
    }

    public void MakePair(Master second)
    {
        if (_PairFile==null) {
            _PairFile = second;
            second.MakePair(this);
            exclusiveFiles = GetExclusiveFiles();
        }
    }

    FileInformation[] GetExclusiveFiles()
    {
        if (_PairFile==null) throw new Exception("No Pair");
        var temp = new HashSet<FileInformation>(files,new comp());
        temp.ExceptWith(_PairFile.files);

       

         return temp.ToArray();
    }
}




