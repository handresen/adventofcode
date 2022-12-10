// https://adventofcode.com/2021/day/10
var lines=File.ReadAllLines("input.txt").ToArray();
var completionScores=new List<Int64>();
int score=0;

foreach(var line in lines){
    Symbol ?root=null;
    Symbol ?current=null;
    int lastScore=0;
    foreach(char c in line){
        if(root==null){
            root=new Symbol(null,c);
            current=root;
            continue;
        }
        var next = current.Process(c);
        if(next.Item2>0){
            lastScore=next.Item2;
            break;
        }
        current = next.Item1;
    }
    score+=lastScore;
    if(current!=null && lastScore==0){
        completionScores.Add(CalcCompletionScore(current.GetClosing()));
    }
}
Console.WriteLine($"Part 1:{score}");
Console.WriteLine($"Part 2:{completionScores.Order().ToArray()[completionScores.Count()/2]}");

long CalcCompletionScore(string s){
    Int64 total=0;
    string chars="*)]}>";
    foreach(char c in s){
        total = total * 5;
        total = total + chars.IndexOf(c);
    }
    return total;
}

class Symbol
{
    public Symbol(Symbol ?parent, char c){
        _parent=parent;
        _symIdx=Symbols.IndexOf(c);
    }
    public int _symIdx;
    public Symbol ?_parent;
    public List<Symbol> _children=new List<Symbol>();
    static readonly string Symbols="()[]{}<>";
    static readonly int[] Scores=new []{3,57,1197,25137};

    public (Symbol?,int) Process(char c){
        int idx=Symbols.IndexOf(c);
        if(idx%2 == 0){  // Opening
            _children.Add(new Symbol(this,c));
            return (_children.Last(),0);
        }
        if(idx==_symIdx+1)  // Closing
            return (_parent,0);
        
        return (null,Scores[idx/2]);   // Error
    }

    public string GetClosing(){
        string result = Symbols[_symIdx+1].ToString();
        if(_parent!=null)
            result += _parent.GetClosing();
        return result;
    }
}

