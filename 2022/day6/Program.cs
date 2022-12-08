// https://adventofcode.com/2022/day/6
var s=File.ReadAllText("input.txt");
Console.WriteLine($"Part 1:{SlidingCheck(s,4)}");
Console.WriteLine($"Part 2:{SlidingCheck(s,14)}");
Console.WriteLine($"Part 1:{RangeCheck(s,4)}");
Console.WriteLine($"Part 2:{RangeCheck(s,14)}");


// Use .net ranges
int RangeCheck(string s, int window){
    for(int i=0;i<s.Length-window;i++)
        if(new HashSet<char>(s[i..(i+window)]).Count()==window)
            return i+window;
    return -1;
}


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
