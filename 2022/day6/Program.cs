// https://adventofcode.com/2022/day/6
var s=File.ReadAllText("input.txt");
int i;
for(i=0;i<s.Length;i++){
    if(!CheckUniqueHash(s,i,4))
        continue;
    break;
}
Console.WriteLine($"Part 1:{i+4}");

for(i=0;i<s.Length;i++){
    if(!CheckUniqueBin(s,i,14))
        continue;
    break;
}
Console.WriteLine($"Part 2:{i+14}");

bool CheckUniqueBin(string s, int idx, int count){
    var bins = new int[27];
    while(count-->0){
        int c=s[idx++]-'a';
        bins[c]++;
        if(bins[c]>=2)
            return false;
    }
    return true;
}

bool CheckUniqueHash(string s, int idx, int count){
    var h=new HashSet<char>();
    while(count-->0){
        char c=s[idx++];
        if(h.Contains(c))
            return false;
        h.Add(c);
    }
    return true;
}



