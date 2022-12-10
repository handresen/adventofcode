// https://adventofcode.com/2022/day/9
var lines=File.ReadAllLines("input.txt").ToArray();
var cmds="RULD";
var move=new[]{(1,0),(0,1),(-1,0),(0,-1)};
(int,int) Move(char c)=>move[cmds.IndexOf(c)];
(int,int) Add((int x,int y) a, (int x,int y) b)=>(a.x+b.x,a.y+b.y);
(int,int) Sub((int x,int y) a, (int x,int y) b)=>(a.x-b.x,a.y-b.y);
(int,int) Cap((int x,int y) a) => (a.x>0?1:(a.x<0?-1:0),a.y>0?1:(a.y<0?-1:0));
<<<<<<< HEAD
int Len((int x,int y) a, (int x,int y) b) => (int)(Math.Sqrt((a.x-b.x)*(a.x-b.x)+(a.y-b.y)*(a.y-b.y)));
(int,int) Update((int x,int y) tail, (int x,int y) head)=>
    Len(head,tail)<=1?tail:Add(tail,Cap(Sub(head,tail)));
=======
int LenSq((int x,int y) a, (int x,int y) b) => ((a.x-b.x)*(a.x-b.x)+(a.y-b.y)*(a.y-b.y));

(int,int) UpdateNext((int x,int y) tail, (int x,int y) head){
    if(LenSq(head,tail)<=2)
        return tail;
    return Add(tail,Cap(Sub(head,tail)));
}
>>>>>>> a112878e4d9aae5cb3fb98cdd0041ac139fcae47

int VisitedCount(int ropeLen){
    var rope=new (int,int)[ropeLen];
    var visited = new HashSet<(int,int)>();
    foreach(var line in lines){
        char dir=line[0];
        int steps=int.Parse(line[2..]);
        for(int i=0;i<steps;i++){
            rope[0]=Add(rope[0],Move(dir));
            for(int j=1;j<rope.Count();j++)
<<<<<<< HEAD
                rope[j]=Update(rope[j],rope[j-1]);        
=======
                rope[j]=UpdateNext(rope[j],rope[j-1]);        
>>>>>>> a112878e4d9aae5cb3fb98cdd0041ac139fcae47
            visited.Add(rope[^1]);
        }
    }
    return visited.Count();
}
Console.WriteLine($"Part 1:{VisitedCount(2)}");
<<<<<<< HEAD
Console.WriteLine($"Part 2:{VisitedCount(10)}");
=======
Console.WriteLine($"Part 2:{VisitedCount(10)}");
>>>>>>> a112878e4d9aae5cb3fb98cdd0041ac139fcae47
