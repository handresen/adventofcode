// https://adventofcode.com/2022/day/24
using IntUtils;
using Visual;

(int dx, int dy)[] Dirs=new []{(1,0),(0,1),(-1,0),(0,-1)};
(int dx, int dy) ToDir(char c)=> Dirs[">v<^".IndexOf(c)];

var Lines=File.ReadAllLines("input.txt").ToArray();
var W=Lines.First().Length-2;
var H=Lines.Length-2;

List<((int x, int y) pos, (int x, int dy) dir)> ReadBlizzards(){
    var blizzards = new List<((int x, int y) pos, (int x, int dy) dir)>();

    for(int row=1;row<Lines.Length-1;row++){
        var line=Lines[row];
        for(int col=1;col<line.Length-1;col++){
            char c=line[col];
            if(c!='.')
                blizzards.Add(((col-1, row-1), ToDir(c)));
        }
    }
    return blizzards;
}

int GetTimeToExit((int x, int y) start, (int x, int y) end, List<((int x, int y) pos, (int x, int dy) dir)> blizzards){   
    var elves=new HashSet<(int x, int y)>();
    elves.Add(start);
    var board = new int[W,H];
    PopulateBoard(board,blizzards.Select(b=>b.pos));

    for(int minute=1;true;minute++){
        UpdateBlizzards(blizzards);
        PopulateBoard(board,blizzards.Select(b=>b.pos));

        var newElves=new HashSet<(int x, int y)>();
        foreach(var e in elves){
            foreach(var d in Dirs){
                var c=U.Add(e,d);
                if(c==end)
                    return minute;
                if(c.x<0 || c.y<0 || c.x>=W || c.y>=H)
                    continue;
                if(board[c.x,c.y]==0)
                    newElves.Add(c); // Spawn new elf
            }
        }
        foreach(var newElf in newElves)
            elves.Add(newElf);

        for(int y=0;y<H;y++){
            for(int x=0;x<W;x++){
                if(board[x,y]>0)
                    elves.Remove((x,y));
            }
        }
        WindBoard.Dump(minute, elves, board, blizzards);
    }
}

void PopulateBoard(int[,] board, IEnumerable<(int x, int y)> coords){
    Array.Clear(board);
    foreach(var c in coords)
        board[c.x, c.y]++;
}

void UpdateBlizzards(List<((int x, int y) pos, (int x, int dy) dir)> blizzards){
    for(int i=0;i<blizzards.Count;i++)
        blizzards[i]=(U.Wrap(W,H,U.Add(blizzards[i].pos, blizzards[i].dir)),
            blizzards[i].dir);            
}

var blizzards=ReadBlizzards();
Console.WriteLine($"Part 1:{GetTimeToExit((0,-1),(W-1,H), blizzards)}");

blizzards=ReadBlizzards();
Console.WriteLine($"Part 2:{GetTimeToExit((0,-1),(W-1,H),blizzards)+
    GetTimeToExit((W-1,H),(0,-1),blizzards) + 
    GetTimeToExit((0,-1),(W-1,H),blizzards)}");
