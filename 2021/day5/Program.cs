// https://adventofcode.com/2021/day/5

var pts=new List<((int x,int y) start,(int x,int y) end)>();
var data=File.ReadAllLines("input.txt").ToArray();

foreach(var l in data){
    var coords=l.Replace(" ","").Split("->");
    var c1=coords.First().Split(",").Select(s=>int.Parse(s));
    var c2=coords.Last().Split(",").Select(s=>int.Parse(s));
    pts.Add(((c1.First(),c1.Last()),(c2.First(),c2.Last())));
}

void AddLine(int x0, int y0, int x1, int y1, Dictionary<(int x, int y), int> target){
    int dx= x0==x1?0:(x1>x0?1:-1);
    int dy= y0==y1?0:(y1>y0?1:-1);
    int N=Math.Max(Math.Abs(x1-x0), Math.Abs(y1-y0))+1;
    
    for(int n=0;n<N;n++){
        target[(x0,y0)]=target.ContainsKey((x0,y0))?target[(x0,y0)]+1:1;
        x0+=dx;y0+=dy;
    }
}

var grid=new Dictionary<(int x, int y), int>();

foreach(var p in pts){
    if(p.start.x==p.end.x || p.start.y==p.end.y) // part 1
        AddLine(p.start.x,p.start.y,p.end.x,p.end.y,grid);
}  

Console.WriteLine($"Total >2: {grid.Where(kv=>kv.Value>=2).Count()}");
