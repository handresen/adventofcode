// https://adventofcode.com/2022/day/8
var lines=File.ReadAllLines("input.txt").ToArray();

int W=lines[0].Length;
int H=lines.Count();
<<<<<<< Updated upstream
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
=======
var grid=new int[W*H];

bool Ok(int r, int c)=>r>=0 && c>=0 && r<H && c<W;
int Idx(int r, int c)=>Ok(r,c)?r*W+c:-1;

for(int r=0;r<H;r++)
    for(int c=0;c<W;c++)
        grid[Idx(r,c)]=lines[r][c]-'0';

int visible=0;
 for(int i=0;i<W*H;i++)
     visible += (IsVisible(i,-1,0) || IsVisible(i,1,0) || IsVisible(i,0,-1) || IsVisible(i,0,1))?1:0;


var viewScores=new List<Int64>();
 for(int i=0;i<W*H;i++)
    viewScores.Add(ViewDist(i,-1,0)*ViewDist(i,1,0)*ViewDist(i,0,-1)*ViewDist(i,0,1));

>>>>>>> Stashed changes

Console.WriteLine($"Part 1:{visible}");
Console.WriteLine($"Part 2:{viewScores.Order().Last()}");

<<<<<<< Updated upstream
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
=======

bool IsVisible(int i, int dr, int dc){
    int r=i/W, c=i%W;    
    int val=grid[Idx(r,c)];
    r+=dr;c+=dc;
    while(Ok(r,c) && grid[Idx(r,c)]<val){
        r+=dr;c+=dc;
    }
    return !Ok(r,c);
}

int ViewDist(int i, int dr, int dc){
    int r=i/W, c=i%W;    
    int val=grid[Idx(r,c)];
    int dist=0;
    r+=dr;c+=dc;
    while(Ok(r,c) && grid[Idx(r,c)]<val){
        dist++;
        r+=dr;c+=dc;
    }
    return dist+(Ok(r,c)?1:0);
}



>>>>>>> Stashed changes
