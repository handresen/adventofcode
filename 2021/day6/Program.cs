// https://adventofcode.com/2021/day/6
var initial=File.ReadAllLines("input.txt").First().Split(',').Select(s=>int.Parse(s));

Int64 Simulate(IEnumerable<int> initial, int iterations)
{
    // Bin the fish based on timer
    var fish=new Int64[9];
    foreach(var timer in initial)
        fish[timer]++;

    for(int day=0;day<iterations;day++)
    {
        Int64 births=fish[0];
        for(int i=0;i<8;i++)
            fish[i]=fish[i+1];
        fish[8]=births;
        fish[6]+=births;
    }
    return fish.Sum();
}

Console.WriteLine($"[Part1] Total fish: {80} iterations: {Simulate(initial,80)}");
Console.WriteLine($"[Part2] Total fish: {256} iterations: {Simulate(initial,256)}");
