var input=File.ReadAllText("input.txt").Replace("\r\n","|");
var summedGroups=input.Split("||").Select(g=>g.Split("|").Select(s=>int.Parse(s)).Sum()).OrderByDescending(n=>n).ToArray();

Console.WriteLine($"Part 1:{summedGroups[0]}");
Console.WriteLine($"Part 2: {summedGroups[0]+summedGroups[1]+summedGroups[2]}");            
