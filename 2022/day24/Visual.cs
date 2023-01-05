
using IntUtils;

namespace Visual
{
    public class WindBoard{
        static readonly (int dx, int dy)[] Dirs=new []{(1,0),(0,1),(-1,0),(0,-1)};
        static char ToDirChar((int dx, int dy) dir) => ">v<^"[Array.IndexOf(Dirs,dir)];
        public const bool Enabled=false; 
        public static void Dump(int minute, HashSet<(int x, int y)> elves, int [,] board, List<((int x, int y) pos, (int x, int dy) dir)> blizzards){
            if(!Enabled)
                return;
            Console.WriteLine($"Minute {minute}");
            int W=board.GetLength(0);
            int H=board.GetLength(1);
            for(int y=-1;y<H+1;y++){
                for(int x=-1;x<W+1;x++){
                    if(elves.Contains((x,y))){
                        Console.Write("E");
                        continue;
                    }
                    if(y==-1 || y==H || x==-1 || x==W)
                        Console.Write("#");
                    else{
                        if(elves.Contains((x,y)))
                            Console.Write("E");
                        /*else*/ if(board[x,y]==0)
                            Console.Write(".");
                        else if(board[x,y]==1)
                            Console.Write(ToDirChar(blizzards.First(b=>b.pos==(x,y)).dir));
                        else Console.Write(U.IntCharRep(board[x,y]));
                    }
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}