// https://adventofcode.com/2021/day/8

int count=0;
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
    MoveString(keys,decoded,1,(l)=>l.First(_=>_.Length==2));
    MoveString(keys,decoded,4,(l)=>l.First(_=>_.Length==4));
    MoveString(keys,decoded,7,(l)=>l.First(_=>_.Length==3));
    MoveString(keys,decoded,8,(l)=>l.First(_=>_.Length==7));

    MoveString(keys,decoded,9,(l)=>l.First(_=>_.Length==6 && _.Contains(NumToCode(decoded,4))));
    MoveString(keys,decoded,3,(l)=>l.First(_=>_.Length==5 && _.Contains(NumToCode(decoded,1))));
    MoveString(keys,decoded,0,(l)=>l.First(_=>_.Length==6 && _.Contains(NumToCode(decoded,1))));
    MoveString(keys,decoded,6,(l)=>l.First(_=>_.Length==6));
    MoveString(keys,decoded,5,(l)=>l.First(s=>NumToCode(decoded,6).Contains(s)));
    MoveString(keys,decoded,2,(l)=>l.First());


    
   // count+=values.Count(v=>uniqueLengths.Contains(v.Length));    
}

string NumToCode(Dictionary<string,int> decoded, int n){
    return decoded.First(kv=>kv.Value==n).Key;
}

void MoveString(List<string> src, Dictionary<string,int> tgt, int val, Func<List<string>,string> rule){
    var s=rule(src);
    tgt[s]=val;
    src.Remove(s);
}

Console.WriteLine($"Part 1: {count}");
