using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace mkrystianc.mytasks;


public class MyTaskItem : ITaskItem
{
    private readonly Dictionary<string, string> _dictionary = new ();
    
    public string GetMetadata(string metadataName)
    {
        return _dictionary[metadataName];
    }

    public void SetMetadata(string metadataName, string metadataValue)
    {
        _dictionary[metadataName] = metadataValue;
    }

    public void RemoveMetadata(string metadataName)
    {
        _dictionary.Remove(metadataName);
    }

    public void CopyMetadataTo(ITaskItem destinationItem)
    {
        foreach (var (metadataName, metadataValue) in _dictionary)
        {
            destinationItem.SetMetadata(metadataName, metadataValue);
        }
    }

    public IDictionary CloneCustomMetadata()
    {
        return new Dictionary<string, string>(_dictionary);
    }

    public string ItemSpec { get; set; } = "<>";
    public ICollection MetadataNames => _dictionary.Keys;
    public int MetadataCount => _dictionary.Count;
}

public class MyTaskItem2 : ITaskItem2
{
    private ITaskItem _taskItem = new TaskItem();
    
    public string GetMetadata(string metadataName)
    {
        return _taskItem.GetMetadata(metadataName);
    }

    public void SetMetadata(string metadataName, string metadataValue)
    {
        _taskItem.SetMetadata(metadataName, metadataValue);
    }

    public void RemoveMetadata(string metadataName)
    {
        _taskItem.RemoveMetadata(metadataName);
    }

    public void CopyMetadataTo(ITaskItem destinationItem)
    {
        _taskItem.CopyMetadataTo(destinationItem);
    }

    public IDictionary CloneCustomMetadata()
    {
        return _taskItem.CloneCustomMetadata();
    }

    public string ItemSpec { get; set; }
    public ICollection MetadataNames { get; }
    public int MetadataCount { get; }
    public string GetMetadataValueEscaped(string metadataName)
    {
        return _taskItem.GetMetadata(metadataName);
    }

    public void SetMetadataValueLiteral(string metadataName, string metadataValue)
    {
        _taskItem.SetMetadata(metadataName, metadataValue);
    }

    public IDictionary CloneCustomMetadataEscaped()
    {
        return _taskItem.CloneCustomMetadata();
    }

    public string EvaluatedIncludeEscaped { get; set; }
}


public class MyTask1 : Task
{
    public override bool Execute()
    {
        MyOutputProperty = "MyOutput_" + (MyInputProperty) +  "_" + DateTime.Now;
       
        MyOutputItem = new MyTaskItem();
        MyOutputItem.SetMetadata("MyKey1_1", "MyValue1_1");
        MyOutputItem.SetMetadata("MyKey1_2", "MyValue1_2");
        MyOutputItem.SetMetadata("Extension", "MyExtension1");
        MyOutputItem.SetMetadata("MyKey1_3", "MyValue1_3");
        
        return true;
    }

    public string MyInputProperty { get; set; } = "<EmptyInput>";
    
    [Output]
    public string MyOutputProperty { get; set; }

    [Output]
    public ITaskItem MyOutputItem { get; set; }
    
    /*
    [Output]
    public ITaskItem2 MyOutputItem2 { get; set; }
    */
}