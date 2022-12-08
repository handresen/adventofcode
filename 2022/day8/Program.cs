// https://adventofcode.com/2022/day/8
var lines=File.ReadAllLines("input.txt").ToArray();

int W=lines[0].Length;
int H=lines.Count();
var grid=new int[H,W];
var dirs=new []{(-1,0),(1,0),(0,-1),(0,1)};
bool Ok((int r, int c) coord)=>coord.r>=0 && coord.c>=0 && coord.r<H && coord.c<W;
(int r, int c) Add((int r, int c) coord,(int dr, int dc) delta) => (coord.r+delta.dr,coord.c+delta.dc);

for(int r=0;r<H;r++)
    for(int c=0;c<W;c++)
        grid[r,c]=lines[r][c]-'0';

int visible=0;
var viewScores=new List<Int64>();

for(int r=0;r<H;r++){
    for(int c=0;c<W;c++){
        visible += dirs.Any(dir=>IsVisible((r,c),dir))?1:0;
        viewScores.Add(dirs.Select(dir=>ViewDist((r,c),dir)).Aggregate(1,(acc,val)=>acc*val));
    }
}

Console.WriteLine($"Part 1:{visible}");
Console.WriteLine($"Part 2:{viewScores.Order().Last()}");

bool IsVisible((int r, int c) coord, (int dr, int dc) dir){
    int val=grid[coord.r,coord.c];
    coord = Add(coord,dir);
    while(Ok(coord) && grid[coord.r,coord.c]<val){
        coord = Add(coord,dir);
    }
    return !Ok(coord);    // Visible if beyond the edge
}

int ViewDist((int r, int c) coord, (int dr, int dc) dir){
    int val=grid[coord.r,coord.c];
    int dist=0;
    coord=Add(coord,dir);
    while(Ok(coord) && grid[coord.r,coord.c]<val){
        dist++;
        coord=Add(coord,dir);
    }
    return dist+(Ok(coord)?1:0);  // If we have not reached the edge, we must include last tree
}
