// https://adventofcode.com/2022/day/5
var input=File.ReadAllText("input.txt");
var parts=input.Split("\r\n\r\n");
var stackLines=parts.First().Split("\r\n").ToArray();
int stackCount=int.Parse(stackLines.Last().Trim().Split(" ").Last());
var commands=parts.Last().Split("\r\n");

Stack<char>[] CreateStacks(){
    var stacks=new Stack<char>[stackCount];
    for(int i=0;i<stackCount;i++)
        stacks[i]=new Stack<char>();

    for(int n=stackLines.Count()-2;n>=0;n--){
        var stackLine = stackLines[n];
        for(int i=0;i<stackCount;i++){
            char c=stackLine[i*4+1];
            if(c!=' ')
                stacks[i].Push(c);
        }
    }
    return stacks;
}

var stacks=CreateStacks();
foreach(var cmd in commands)
{
    if(cmd.Split(" ").Where(s=>char.IsDigit(s[0])).Select(s=>int.Parse(s)).ToArray() is [var count, var from, var to]){   
    for(int n=0;n<count;n++)
        stacks[to-1].Push(stacks[from-1].Pop());
    }
}
Console.WriteLine($"Part 1 result: {string.Join("",stacks.Select(s=>s.Peek()))}");

stacks=CreateStacks();
foreach(var cmd in commands)
{
    if(cmd.Split(" ").Where(s=>char.IsDigit(s[0])).Select(s=>int.Parse(s)).ToArray() is [var count, var from, var to]){   
        var temp = new List<char>();
        for(int n=0;n<count;n++)
            temp.Add(stacks[from-1].Pop());
        temp.Reverse();
        foreach(var c in temp)
            stacks[to-1].Push(c);        
    }
}
Console.WriteLine($"Part 1 result: {string.Join("",stacks.Select(s=>s.Peek()))}");
