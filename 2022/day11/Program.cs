// https://adventofcode.com/2022/day/11
var monkeys = new List<Monkey>();
foreach(var monkeyString in File.ReadAllText("input.txt").Split("\r\n\r\n"))
    monkeys.Add(new Monkey(monkeyString));

for(int i=0;i<20;i++){
    foreach(var monkey in monkeys)
        monkey.Process(monkeys,n=>n/3);
}
var part1=monkeys.Select(m=>m._inspections).OrderDescending().Take(2);
Console.WriteLine($"Part 1:{part1.First()*part1.Last()}");

monkeys.Clear();
foreach(var monkeyString in File.ReadAllText("input.txt").Split("\r\n\r\n"))
    monkeys.Add(new Monkey(monkeyString));

long modProduct = 1;
foreach(var m in monkeys)
    modProduct *= m._divisor;
for(int i=0;i<10000;i++){
    foreach(var monkey in monkeys)
        monkey.Process(monkeys,n=>n%modProduct);
}
var part2=monkeys.Select(m=>m._inspections).OrderDescending().Take(2);
Console.WriteLine($"Part 2:{part1.First()*part1.Last()}");


class Monkey{
    public List<long> _items;
    public long _divisor; 
    public long _inspections=0;
    int _throwFailed;
    int _throwPassed;
    long ?_value;  // old == null
    char _operation;
    bool TestItem(long item){
        return item%_divisor == 0;
    }

    long Inspect(long itemWorry, Func<long,long> worryFunc){
        long o1 = itemWorry;
        long o2 = _value??itemWorry;
        long inspectedWorry =  _operation=='*'?(o1*o2):(o1+o2);
        inspectedWorry = worryFunc(inspectedWorry);
        _inspections++;
        return inspectedWorry;
    }

    public void Process(List<Monkey> group, Func<long,long> worryReduction){
        while(_items.Any()){
            var item = _items.First();
            _items.RemoveAt(0);
            item = Inspect(item, worryReduction);
            int idx=TestItem(item)?_throwPassed:_throwFailed;
            group[idx]._items.Add(item);
        }
    }

    public Monkey(string s){
        var lines = s.Split("\r\n").ToArray();
        _items = (lines[1].Split(": ").Last().Split(", ").Select(n=>long.Parse(n))).ToList();
        var tokens = lines[2].Split(" = ").Last().Split(" ").ToArray();
        _value=tokens[2]=="old"?null:long.Parse(tokens[2]);
        _operation = tokens[1][0];
        _divisor = int.Parse(lines[3].Split("by ").Last());
        _throwPassed = int.Parse(lines[4].Split(" ").Last());
        _throwFailed = int.Parse(lines[5].Split(" ").Last());
    }
}