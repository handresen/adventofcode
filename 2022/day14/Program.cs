// https://adventofcode.com/2022/day/14
var data=File.ReadAllLines("input.txt").ToArray();

var points = new HashSet<(int x, int y)>();
foreach(var l in data){
    var coords = l.Split(" -> ").Select(c=>ToCoord(c)).ToArray();
    for(int i=0;i<coords.Length-1;i++){
        Trace(coords[i], coords[i+1], points);
    }
}

int maxY=points.Max(c=>c.y);
var cave = new HashSet<(int x,int y)>(points);
int grains=0;
while(AddSand(cave, maxY))
    grains++;
Console.WriteLine($"Part 1 grains:{grains}");

cave = new HashSet<(int,int)>(points);
Trace((-100000,maxY+2),(100000,maxY+2),cave);
grains=0;
while(AddSand(cave, maxY+2))
    grains++;
Console.WriteLine($"Part 2 grains:{grains}");

(int, int) ToCoord(string s) => (int.Parse(s.Split(",").First()), int.Parse(s.Split(",").Last()));
bool Eq((int x, int y) a, (int x, int y) b) => a.x==b.x && a.y==b.y;
(int x,int y) Add((int x, int y) a, (int x, int y) b) => (a.x+b.x, a.y+b.y);
int Cap1(int n)=>n>0?1:n<0?-1:0;
(int dx,int dy) Step((int x, int y) a, (int x, int y) b) => (Cap1(b.x-a.x), Cap1(b.y-a.y));

(int x, int y) TryMove((int x, int y) c, HashSet<(int x, int y)> pts){
    foreach(var dir in new []{(0,1),(-1,1),(1,1)})
        if(!pts.Contains(Add(c,dir)))
            return Add(c,dir);
    return c;   // Unable to move
}

bool AddSand(HashSet<(int x, int y)> pts, int maxY){
    var sc=(500,0);
    if(pts.Contains(sc))   // Full at the top
        return false;
    var scNext=TryMove(sc, pts);
    while(scNext!=sc && scNext.y<=maxY){
        sc=scNext;
        scNext=TryMove(sc,pts);
    }
    if(scNext.y>maxY)
        return false;

    pts.Add(sc);
    return true;
}

void Trace((int x, int y) a, (int x, int y) b, HashSet<(int x, int y)> tgt){
    var step=Step(a,b);  
    while(!Eq(a,b)){
        tgt.Add(a);
        a=Add(a,step);
    }    
    tgt.Add(b);
}
