// https://adventofcode.com/2022/day/2
var data=File.ReadAllLines("input.txt").ToArray();

int score=0;
foreach(var l in data){
    var chars=l.Split(" ").Select(s=>s[0]);
    int a=chars.First()-'A';
    int b=chars.Last()-'X';
    //score+=Score(a,b);    // Part1
    score += Score(a, Strategy(a,b));
}

Console.WriteLine($"Score: {score}");

int Strategy(int a, int b){
    // 0 - Lose
    // 1 - Draw
    // 2 - Win
    if(b==0)
        return (a+2)%3; // Previous
    if(b==1)
        return a;
    if(b==2)
        return (a+1)%3; // Next
    return 0;
}

int Score(int a, int b){
    // 0 A - Rock         1
    // 1 B - Paper        2
    // 2 C - Scissors     3
    int result=b+1;
    if(a==b)
        return result+3;
    if(b == ((a+1)%3))
        return result+6;
    return result;
}
