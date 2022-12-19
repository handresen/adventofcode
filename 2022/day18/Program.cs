// https://adventofcode.com/2022/day/18
var lines=File.ReadAllLines("input.txt");

var blocks=new HashSet<(int x, int y, int z)>();
var steam=new HashSet<(int x, int y, int z)>();

(int x, int y, int z)[] NeiDirs=new []{(-1,0,0),(1,0,0),(0,-1,0),(0,1,0),(0,0,-1),(0,0,1)};
(int x, int y, int z) Add((int x, int y, int z) a, (int x, int y, int z) b)=>(a.x+b.x,a.y+b.y,a.z+b.z);
bool IsOutside((int x, int y, int z) s)=>s.x<-1 || s.y<-1 || s.z<-1 || s.x>20 || s.y>20 || s.z>20;

foreach(var l in lines){
    var coords=l.Split(",").Select(x=>int.Parse(x)).ToArray();
    blocks.Add((coords[0],coords[1],coords[2]));
}

int outFacing=0;
foreach(var b in blocks)
    foreach(var dir in NeiDirs)
        if(!blocks.Contains(Add(b,dir)))
            outFacing++;
Console.WriteLine($"Part 1:{outFacing}");

void GrowSteam((int x, int y, int z)[] seeds, HashSet<(int x, int y, int z)> steam){
    foreach(var b in seeds){
        if(blocks.Contains(b) || steam.Contains(b) || IsOutside(b))
            continue;

        steam.Add(b);
        GrowSteam(NeiDirs.Select(x=>Add(b,x)).ToArray(),steam);
    }
}

Console.Write($"Expanding steam...");
GrowSteam(new []{(-1,-1,-1)},steam);
Console.WriteLine($"Done");

outFacing=0;
foreach(var b in blocks)
    foreach(var dir in NeiDirs)
        if(!blocks.Contains(Add(b,dir))&& steam.Contains(Add(b,dir)))
            outFacing++;

Console.WriteLine($"Part 2:{outFacing}");

