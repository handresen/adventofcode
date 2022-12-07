// https://adventofcode.com/2022/day/7
var lines=File.ReadAllLines("input.txt").ToArray();

var root=new Dir(null);
Dir current=root;

foreach(var l in lines){
    var parts=l.Split(" ").ToArray();
    if(parts[0]=="$"){
        if(parts[1]=="cd"){
            var path=parts[2];
            if(path=="/")
                current=root;
            else if(path=="..")
                current=current?.parentDir??root;
            else if(current.subdirs.ContainsKey(path))
                    current=current.subdirs[path]??current;
        }
        if(parts[1]=="ls"){// No need to do anything...
        }
    }
    else if(parts[0]=="dir"){
        current.subdirs[parts[1]]=new Dir(current);
        continue;
    }
    else if(int.TryParse(parts[0],out var len)){
        current.files[parts[1]]=len;
    }      
}

var flattened=root.FlattenDirSizes();;
Console.WriteLine($"Part 1:{flattened.Where(n=>n<100000).Sum()}");

Int64 free=70000000-root.Size();
Console.WriteLine($"Part 2:{flattened.Where(n=>free+n>=30000000).Min()}");

public class Dir{
    public Dir(Dir ?parent){
        parentDir=parent;
    }
    public Dictionary<string, Dir> subdirs=new Dictionary<string, Dir>();
    public Dictionary<string, int> files=new Dictionary<string, int>();
    public Dir ?parentDir=null;
    public long Size(){
        Int64 filesSize= files.Select(kv=>kv.Value).Sum();
        foreach(var child in subdirs)
            filesSize+=child.Value.Size();
        return filesSize;
    }

    public List<long> FlattenDirSizes(List<long> ?sizes=null){
        sizes=sizes??new List<long>();
        sizes.Add(Size());
        foreach(var child in subdirs)
            child.Value.FlattenDirSizes(sizes);
        return sizes;
    }
}
