// https://adventofcode.com/2021/day/8

int count=0, sum=0;
foreach(var line in File.ReadAllLines("input.txt")){
    var parts = line.Split(" | ").Select(s=>s.Split(" "));
    var keys=parts.First().Select(s=>new string(s.OrderBy(_=>_).ToArray())).ToList();
    var values=parts.Last().Select(s=>new string(s.OrderBy(_=>_).ToArray()));
    //1 1 -> L2 cf
    //  2 -> L5 acdeg
    //3 3 -> L5 contains 1
    //1 4 -> L4 bcdf
    //6 5 -> L5 abdfg contained by 6
    //5 6 -> L6 abdefg
    //1 7 -> L3 acf
    //1 8 -> L7 abcdefg
    //2 9 -> L6 contains 4
    //4 0 -> L6 contains 1
    var decoded = new Dictionary<string,int>();
    Move(keys,decoded,1,keys.First(s=>s.Length==2));
    Move(keys,decoded,4,keys.First(s=>s.Length==4));
    Move(keys,decoded,7,keys.First(s=>s.Length==3));
    Move(keys,decoded,8,keys.First(s=>s.Length==7));
    Move(keys,decoded,9,keys.First(s=>s.Length==6 && Contains(s, NumToCode(decoded,4))));
    Move(keys,decoded,3,keys.First(s=>s.Length==5 && Contains(s, NumToCode(decoded,1))));
    Move(keys,decoded,0,keys.First(s=>s.Length==6 && Contains(s, NumToCode(decoded,1))));
    Move(keys,decoded,6,keys.First(_=>_.Length==6));
    Move(keys,decoded,5,keys.First(s=>s.Length==5 && Contains(NumToCode(decoded,6), s)));
    Move(keys,decoded,2,keys.First());
  
   var part1Values=new []{1,4,7,8};
   count+=values.Count(v=> part1Values.Contains(decoded[v]));    
   var digits = values.Select(coded=>decoded[coded]).Reverse();
   int factor=1;
   foreach(int digit in digits){
        sum+=factor*digit;
        factor*=10;
   }
}
Console.WriteLine($"Part 1: {count}");
Console.WriteLine($"Part 2: {sum}");
 
string NumToCode(Dictionary<string,int> decoded, int n) => decoded.First(kv=>kv.Value==n).Key;
bool Contains(string container, string contained)=>contained.All(c=>container.Contains(c));
void Move(List<string> src, Dictionary<string,int> tgt, int val, string s){
    tgt[s]=val;
    src.Remove(s);
}