var data=File.ReadAllLines("input.txt").Select(l=>l.Trim());
var calories=new List<int>();

int sum=0;
foreach(var l in data){
    if(String.IsNullOrWhiteSpace(l)){
        if(sum>0)
            calories.Add(sum);
        sum=0;
        continue;
    }
    sum+=int.Parse(l);
}
if(sum>0)
    calories.Add(sum);

var ordered=calories.OrderByDescending(n=>n).ToArray();
Console.WriteLine($"Total: {ordered[0]+ordered[1]+ordered[2]}");            
