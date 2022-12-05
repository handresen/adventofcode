// https://adventofcode.com/2021/day/9
var lines=File.ReadAllLines("input.txt").ToArray();
int rows=lines.Count();
int cols=lines.First().Count();
var basinSeeds=new List<(int r,int c,int h)>();
var data=new int[rows,cols];

for(int r=0;r<rows;r++){
    var nums=lines[r].Select(c=>c-'0').ToArray();
    for(int c=0;c<cols;c++)
        data[r,c]=nums[c];
}
int riskLevel=0;
for(int r=0;r<rows;r++){
    for(int c=0;c<cols;c++){
        var val=data[r,c];
        if(GetVal(r,c-1,10)>val && GetVal(r,c+1,10)>val && GetVal(r-1,c,10)>val && GetVal(r+1,c,10)>val){
            riskLevel+=1+val;
            basinSeeds.Add((r,c,val));
        }
    }
}
var orderedBasins = basinSeeds.Select(bs=>MakeBasin(bs.r,bs.c).Count()).OrderDescending().Take(3).ToArray();

Console.WriteLine($"Part 1: {riskLevel}");
Console.WriteLine($"Part 2: {orderedBasins[0]*orderedBasins[1]*orderedBasins[2]}");

List<(int,int)> MakeBasin(int r,int c, List<(int,int)>? basin = null){
    if(basin==null)
        basin=new List<(int, int)>();
    if(!HasVal(r,c) || data[r,c]==9)
        return basin;
    basin.Add((r,c));
    data[r,c]=9;
    MakeBasin(r,c-1,basin);
    MakeBasin(r,c+1,basin);
    MakeBasin(r-1,c,basin);
    MakeBasin(r+1,c,basin);
    return basin;
}

bool HasVal(int r, int c) => r>=0 && c>=0 && r<data.GetLength(0) && c<data.GetLength(1);
int GetVal(int r, int c, int defaultValue) => HasVal(r,c)?data[r,c]:defaultValue;
