// https://adventofcode.com/2022/day/10
var lines=File.ReadAllLines("input.txt").ToArray();
var cycleValues=new List<int>(new []{1});
foreach(var line in lines.Select(l=>l.Trim()).Select(l=>l.Split(" "))){
    if(line[0]=="noop"){
        cycleValues.Add(cycleValues.Last());
        continue;
    }
    if(line[0]=="addx"){
        cycleValues.Add(cycleValues.Last());
        cycleValues.Add(cycleValues.Last()+int.Parse(line[1]));
    }
}
var acc=0;
for(int i=20;i<=220;i+=40){
    acc+=cycleValues[i-1]*i;
}
Console.WriteLine($"Part 1:{acc}");

Console.WriteLine($"Part 2:");
var vals=cycleValues.ToArray();
for(int r=0;r<cycleValues.Count()/40;r++){
    var values=vals[(r*40)..(r*40+40)];
    for(int i=0;i<40;i++)
        Console.Write(Math.Abs(values[i]-i)<2?"#":".");
    Console.WriteLine();
}
