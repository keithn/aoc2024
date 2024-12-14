namespace Aoc2024;

public static class Day1
{
    public static void Solve(string input)
    {
        var numbers = input.Split([' ', '\r', '\n'], StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
        var left = numbers.Where((_, i) => i % 2 == 0).ToArray();
        var right = numbers.Where((_, i) => i % 2 == 1).ToArray();
        var part1 = left.Order().Zip(right.Order(), (l, r) => Math.Abs(l - r)).Sum();
        Console.WriteLine(part1);
        
        var part2 = left.Sum(l => l * right.Count(r => r == l));
        Console.WriteLine(part2);
    }
}