namespace IntUtils
{
    public static class U{
        static public (int x, int y) Add((int x, int y) a, (int x, int y) b)=>(a.x+b.x,a.y+b.y);
        static public (int x, int y) Sub((int x, int y) a, (int x, int y) b)=>(a.x-b.x,a.y-b.y);
        static public int Dot((int x, int y) a, (int x, int y) b)=>(a.x*b.x+a.y+b.y);
        static public (int x, int y) Wrap(int W, int H, (int x, int y) coord)=>
            (SafeMod(coord.x,W),SafeMod(coord.y,H));
        static public int SafeMod(long x, int m)=>(int)((x%m + m)%m);
        static public char IntCharRep(int n){
            if(n<0) return '-';
            if(n>9) return '+';
            return (char)('0'+n);
       }
    }
}

