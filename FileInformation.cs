using System;

public class FileInformation {
   // [JsonConstructor]
    public FileInformation(string relativeName,DateTime  createDate ,long  length )
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
}