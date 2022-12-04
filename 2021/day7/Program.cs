// https://adventofcode.com/2021/day/7
var initial=File.ReadAllLines("input.txt").First().Split(',').Select(s=>int.Parse(s));

(int,int) FindPos(Func<int,int> Cost)
{
    (int,int) minPosCost=(-1,int.MaxValue);
    for(int candidatePos=initial.Min();candidatePos<=initial.Max();candidatePos++){
        int cost=0;
        foreach(var pos in initial){
            cost+=Cost(Math.Abs(candidatePos-pos));
        }
        if(cost<minPosCost.Item2)
            minPosCost=(candidatePos,cost);
    }
    return minPosCost;
}

var c1=FindPos((dist)=> dist);
Console.WriteLine($"[Part1] Pos: {c1.Item1} Cost:{c1.Item2}");

var c2=FindPos((dist)=> dist*(dist+1)/2);
Console.WriteLine($"[Part1] Pos: {c2.Item1} Cost:{c2.Item2}");
