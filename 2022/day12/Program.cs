// https://adventofcode.com/2022/day/12
using System.Diagnostics;

var lines=File.ReadAllLines("input.txt").Select(l=>l.ToArray()).ToArray();
int W=lines.First().Length;
int H=lines.Length;
const int NOT_VISITED=int.MaxValue;
const int NO_WAY=1000000;
var elevations=new int[W,H];
(int x,int y) target=(0,0);
(int x,int y) start=(0,0);

bool Ok(int x, int y)=>x>=0 && x<W && y>=0 && y<H;
bool OkCoord((int x, int y) c)=>Ok(c.x,c.y);

for(int y=0;y<H;y++){
    for(int x=0;x<W;x++){
        char val=lines[y][x];
        if(val=='E'){
            target=(x,y);
            val='z';
        }
        else if(val=='S'){
            val='a';
            start=(x,y);
        }
        elevations[x,y]=val-'a';
    }
}

int CalcCost((int x, int y) self, (int x, int y) next){
    var selfElev=elevations[self.x, self.y];
    var nextElev=elevations[next.x,next.y];
    return nextElev<=selfElev+1 ? 1:NO_WAY;
}

(int x,int y, int cost) FindCheapestExpansion(HashSet<(int x,int y)> visited, int[,]costs){
    (int dx, int dy) []dirs=new []{(1,0),(-1,0),(0,1),(0,-1)};
    (int x, int y, int cost) lowest = (-1,-1,NO_WAY);
    foreach(var ec in visited){
        int selfCost = costs[ec.x, ec.y];
        foreach(var dir in dirs){
            var next = (ec.x+dir.dx, ec.y + dir.dy);
            if(!OkCoord(next) || visited.Contains(next))
                continue;
            int costCandidate=CalcCost(ec,next)+selfCost;
            if(lowest.cost>costCandidate){
                lowest=(ec.x+dir.dx,ec.y+dir.dy,costCandidate);
            }
        }
    }
    return lowest;
}

int CostToTarget((int x, int y) target, Func<int,int,bool> isStartingPos){
    var costs=new int[W,H];
    var visited=new HashSet<(int,int)>();
    for(int y=0;y<H;y++){
        for(int x=0;x<W;x++){
            if(isStartingPos(x,y)){
                visited.Add((x,y));
                costs[x,y]=0;
            }
            else
                costs[x,y]=NOT_VISITED;
        }
    }   
    (int x,int y, int cost) nextItem = FindCheapestExpansion(visited,costs);
    while(nextItem.cost<NO_WAY){
        costs[nextItem.x,nextItem.y]=nextItem.cost;
        visited.Add((nextItem.x, nextItem.y));
        nextItem = FindCheapestExpansion(visited,costs);
    }
    return costs[target.x, target.y];
}

Console.WriteLine($"Part 1:{CostToTarget(target, (x,y)=>(x,y)==start)}");
Console.WriteLine($"Part 2:{CostToTarget(target, (x,y)=>elevations[x,y]==0)}");