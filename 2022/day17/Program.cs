// https://adventofcode.com/2021/day/6

var pts=new List<((int x,int y) start,(int x,int y) end)>();
var data=File.ReadAllText("sample.txt").ToArray();

var rocksTemplates=new List<(int x, int y)>[5];
rocksTemplates[0]=new List<(int x, int y)>(new []{(0,0),(1,0),(2,0),(3,0)});
rocksTemplates[1]=new List<(int x, int y)>(new []{(1,0),(0,1),(1,1),(2,1),(1,2)});
rocksTemplates[2]=new List<(int x, int y)>(new []{(0,0),(1,0),(2,0),(2,1),(2,2)});
rocksTemplates[3]=new List<(int x, int y)>(new []{(0,0),(0,1),(0,2),(0,3)});
rocksTemplates[4]=new List<(int x, int y)>(new []{(0,0),(1,0),(0,1),(1,1)});


int Part1(int rocks=2022){

    IEnumerable<(int x, int y)> currentRock=null;
    var world=new HashSet<(int x, int y)>();
    for(int x=0;x<7;x++)
        world.Add((x,0));
    for(int y=0;y<10000;y++){
        world.Add((-1,y));
        world.Add((7,y));
    }
    int maxY=0;
    int nextRock=0;
    int rockCount=0;
    int i=0;

    while(rockCount<rocks){
        char c = data[i++%data.Length];
        int move=c=='>'?1:-1;
        if(currentRock==null){
            int height=maxY+4;
            currentRock = rocksTemplates[nextRock++ %5].Select(r=>(r.x+2,r.y+height)).ToArray();
        }    
        var shifted=currentRock.Select(r=>(r.x+move,r.y)).ToArray();
        if(!world.Intersect(shifted).Any())
            currentRock=shifted;
        shifted=currentRock.Select(r=>(r.x,r.y-1)).ToArray();
        if(!world.Intersect(shifted).Any())
            currentRock=shifted;
        else{
            // Put to rest
            foreach(var r in currentRock)
                world.Add(r);
            maxY = Math.Max(maxY, currentRock.Max(r=>r.y));
            currentRock=null;
            rockCount++;
            //DrawBoard();
        } 
    }
    Console.WriteLine($"Part 1:{maxY}");
    return maxY;
}
Part1();
return;

var deltaSeq=new HashSet<long>();
void Part2(){
    IEnumerable<(int x, int y)> currentRock=null;
    var world=new HashSet<(int x, int y)>();
    for(int x=0;x<7;x++)
        world.Add((x,0));
    for(int y=0;y<120000;y++){
        world.Add((-1,y));
        world.Add((7,y));
    }
    int maxY=0;
    int nextRock=0;
    int rockCount=0;
    int i=0;
    
    int deltaFloor=maxY;
    long regDeltas=4;
    long deltaSig=0;
    int seqStart = 0;
    int seqRocksStart=0;
    long sigLen=7;

    while(rockCount<60000){
        if(i%data.Length ==0){
            deltaFloor = maxY;
            regDeltas=sigLen;
            seqStart = i;
            seqRocksStart = rockCount;
        }
        char c = data[i++%data.Length];
        int move=c=='>'?1:-1;
        if(currentRock==null){
            int height=maxY+4;
            currentRock = rocksTemplates[nextRock++ %5].Select(r=>(r.x+2,r.y+height)).ToArray();
        }    
        var shifted=currentRock.Select(r=>(r.x+move,r.y)).ToArray();
        if(!world.Intersect(shifted).Any())
            currentRock=shifted;
        shifted=currentRock.Select(r=>(r.x,r.y-1)).ToArray();
        if(!world.Intersect(shifted).Any())
            currentRock=shifted;
        else{
            // Put to rest
            foreach(var r in currentRock)
                world.Add(r);
            maxY = Math.Max(maxY, currentRock.Max(r=>r.y));
            currentRock=null;
            rockCount++;

            if(regDeltas>0){
                var delta=maxY-deltaFloor;
                deltaSig += delta << (8*((int)sigLen-(int)regDeltas));
                regDeltas--;
                if(regDeltas==0){
                    if(deltaSeq.Contains(deltaSig)){
                        Console.WriteLine($"Found sig at {i} for rock {rockCount} seq start {seqStart} iter {seqStart/data.Length} floor {deltaFloor}");
                        Console.WriteLine($"Rocks at start: {seqRocksStart}");                       
                        break;
                    }
                    deltaSeq.Add(deltaSig);
                    deltaSig=0;

                }
            }
            

            //DrawBoard();
        } 
    }
    Console.WriteLine($"Part 2:{maxY}");
}
Part2();
long rockCount=1000000000000L;
var heightStartBlock=(rockCount/57L)*92L;
long remainingRocks=rockCount-(heightStartBlock/71L)*43L;
Console.WriteLine($"Remainding:{remainingRocks}");
var rest=Part1((int)remainingRocks);
Console.WriteLine($"Height:{heightStartBlock + rest}");


// void DrawBoard(){
//         for(int y=20;y>=0;y--){
//         for(int x=-1;x<=7;x++){
//             if(world.Contains((x,y)))
//                 Console.Write("#");
//             else
//                 Console.Write(".");
//         }
//         Console.WriteLine();
//     }
//     Console.ReadLine();
// }


bool Eq((int x, int y) a, (int x, int y) b) => a.x==b.x && a.y==b.y;
(int x,int y) Add((int x, int y) a, (int x, int y) b) => (a.x+b.x, a.y+b.y);
int Cap1(int n)=>n>0?1:n<0?-1:0;
(int dx,int dy) Step((int x, int y) a, (int x, int y) b) => (Cap1(b.x-a.x), Cap1(b.y-a.y));
