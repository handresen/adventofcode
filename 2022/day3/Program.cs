// https://adventofcode.com/2022/day/3
var lines=File.ReadAllLines("input.txt").ToArray();
var sackParts=lines.ToArray().Select(l=>(l.Take(l.Count()/2),l.TakeLast(l.Count()/2))).ToArray();

int score=0;
foreach(var l in sackParts){
    var c1=new HashSet<char>(l.Item1);
    char dup='!';
    foreach(var c in l.Item2){
        if(c1.Contains(c)){
            dup=c;
            break;
        }
    }
    score+=Score(dup);
}
Console.WriteLine($"Part 1 score: {score}");

score=0;
for(int i=0;i<lines.Count();i+=3)
{
    var c1=new HashSet<char>(lines[i]);
    var c2=c1.Where(c=>new HashSet<char>(lines[i+1]).Contains(c));
    var c3=c2.Where(c=>new HashSet<char>(lines[i+2]).Contains(c));
    score+=Score(c3.First());
}
Console.WriteLine($"Part 2 score: {score}");

int Score(char c){
    return char.IsUpper(c)?c-'A'+27:c-'a'+1;
}


