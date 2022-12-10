// https://adventofcode.com/2021/day/11
var lines=File.ReadAllLines("input.txt").ToArray();

int W=lines[0].Length;
int H=lines.Count();
var grid=new int[W*H];

bool Ok(int r, int c)=>r>=0 && c>=0 && r<H && c<W;
int Idx(int r, int c)=>Ok(r,c)?r*W+c:-1;
void Explode(int r, int c){
    grid[i]=-1000000;

}

for(int r=0;r<H;r++)
    for(int c=0;c<W;c++)
        grid[Idx(r,c)]=lines[r][c]-'0';

for(int step=0;step<100;step++){
    for(int i=0;i<W*H;i++)
        grid[i]++;
    int bangs=0;
    do{
        for(int i=0;i<W*H;i++)
            if(grid[i]>=9){

                bangs++;

            }
    }while(bangs>0);
}

