// https://adventofcode.com/2022/day/3
var lines=File.ReadAllLines("input.txt").ToArray();

int score=0;
foreach(var l in lines){
    var parts=l.Chunk(l.Trim().Count()/2);
    var dup=parts.First().Where(c=>parts.Last().Contains(c)).First();
    score+=Score(dup);
}
Console.WriteLine($"Part 1 score: {score}");

score=0;
foreach(var group in lines.Chunk(3).Select(g=>g.ToArray())){
    var dup=group[0].Where(c=>group[1].Contains(c)).Where(c=>group[2].Contains(c)).First();
    score+=Score(dup);
}
Console.WriteLine($"Part 2 score: {score}");

int Score(char c){
    return char.IsUpper(c)?c-'A'+27:c-'a'+1;
}
