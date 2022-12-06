// https://adventofcode.com/2022/day/6
var s=File.ReadAllText("input.txt");
int i;

for(i=0;i<s.Length;i++)
{
    if(!CheckUnique(s,i,4))
        continue;
    break;
}
Console.WriteLine($"Part 1:{i+4}");


for(i=0;i<s.Length;i++)
{
    if(!CheckUnique(s,i,14))
        continue;
    break;
}
Console.WriteLine($"Part 2:{i+14}");

bool CheckUnique(string s, int idx, int count){
    var ary = new int[27];
    while(count-->0){
        int c=s[idx++]-'a';
        ary[c]++;
        if(ary[c]>=2)
            return false;
    }
    return true;
}

