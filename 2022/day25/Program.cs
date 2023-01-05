// https://adventofcode.com/2022/day/25
int[] DigVals = { 2, 1, 0, -1, -2 };
int SNum(char c) => DigVals["210-=".IndexOf(c)];
char NumS(int n) => "210-="[Array.IndexOf(DigVals, n)];

string ToBase5(long n)
{ // c# built in convert only supports base 2^n
    string s = "";
    for (; n > 0; n /= 5)
        s = (n % 5).ToString() + s;
    return s;
}

string ToSnafu(long num)
{
    string b5 = ToBase5(num);
    string result = "";
    int carry = 0;
    while (b5.Any())
    {
        char digitS = b5[^1];
        b5 = b5[0..^1];
        int digitVal = digitS - '0' + carry;
        carry = 0;
        while (digitVal > 2)
        {
            digitVal = digitVal - 5;
            carry++;
        }
        result = NumS(digitVal) + result;
    }
    if (carry > 0)
        result = NumS(carry) + result;

    return result;
}

long Sum = 0;
foreach (var line in File.ReadAllLines("input.txt"))
{
    var rl = line.Reverse().ToList();
    long num = 0;
    for (int i = 0; i < rl.Count(); i++)
    {
        long snum = SNum(rl[i]);
        num += snum * (long)(Math.Pow(5, i) + 0.5);
    }
    Sum += num;
}

Console.WriteLine($"Part 1 SNAFU:{ToSnafu(Sum)}");