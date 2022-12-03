// https://adventofcode.com/2022/day/3
var lines=File.ReadAllLines("input.txt").ToArray();
var sackParts=lines.ToArray().Select(l=>(l.Take(l.Count()/2),l.TakeLast(l.Count()/2))).ToArray();

int score=0;
foreach(var l in sackParts){
    var dup=l.Item1.Where(c=>l.Item2.Contains(c)).First();
    score+=Score(dup);
}
Console.WriteLine($"Part 1 score: {score}");

score=0;
for(int i=0;i<lines.Count();i+=3){
    var dup=lines[i].Where(c=>lines[i+1].Contains(c)).Where(c=>lines[i+2].Contains(c)).First();
    score+=Score(dup);
}
Console.WriteLine($"Part 2 score: {score}");

int Score(char c){
    return char.IsUpper(c)?c-'A'+27:c-'a'+1;
}
