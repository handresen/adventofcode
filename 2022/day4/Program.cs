// https://adventofcode.com/2022/day/4
int score1=0, score2=0;
foreach(var l in File.ReadAllLines("input.txt").ToArray()){
    var rng=l.Replace("-",",").Split(",").Select(a=>int.Parse(a)).ToArray();
    if(Contains(rng[0], rng[1], rng[2],rng[3]))
        score1++;
    if(Overlaps(rng[0], rng[1], rng[2],rng[3]))
        score2++;
}

bool Contains(int a0,int a1,int b0,int b1) => (b0>=a0 && b1<=a1) || (b0<=a0 && b1>=a1);
bool Overlaps(int a0,int a1,int b0,int b1) => !(b1<a0 || b0>a1);

Console.WriteLine($"Part 1 score: {score1}");
Console.WriteLine($"Part 1 score: {score2}");
