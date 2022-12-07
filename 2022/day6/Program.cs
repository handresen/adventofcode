// https://adventofcode.com/2022/day/6
var s=File.ReadAllText("input.txt");
Console.WriteLine($"Part 1:{SlidingCheck(s,4)}");
Console.WriteLine($"Part 2:{SlidingCheck(s,14)}");

// For fun, keep track of unique count using sliding window rather than determine
// duplicates in each window. Likely much faster execution times for large files
int SlidingCheck(string s, int window){
    var bins = new int[27];
    int unique = 0, idx=window;
    foreach(var c in s.Take(window)){
        unique+=bins[c-'a']++==0?1:0;
    };
    for(int i=window;i<s.Length;i++){
        if(unique==window)
            return i;
        if(--bins[s[i-window]-'a']==0) unique--;
        if(++bins[s[i]-'a']==1) unique++;        
    }
    return -1;
}
