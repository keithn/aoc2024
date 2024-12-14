using System.Text.RegularExpressions;

namespace Aoc2024;

public class Day3
{
    private class Context( bool AlwaysEnabled )
    {
        public bool Enabled { get => field | AlwaysEnabled; set; } = true;
    }
    public static void Solve(string input)
    {
        var operations = Regex.Matches(input, @"(mul|do|don't)\((\d+)?,?(\d+)?\)")
            .Select(m => (Op: m.Groups[1].Value, A: m.Groups[2].Value, B: m.Groups[3].Value))
            .ToList();

        int Evaluate(string op, string a, string b, Context context) =>
            op switch
            {
                "do" => (context.Enabled = true, 0).Item2,
                "don't" => (context.Enabled = false, 0).Item2,
                "mul" => (context.Enabled) ? int.Parse(a) * int.Parse(b) : 0,
                _ => 0
            };

        int Calculate(Context context) => operations.Sum(op => Evaluate(op.Op, op.A, op.B, context));

        Console.WriteLine(Calculate(new Context(true))); // Part 1
        Console.WriteLine(Calculate(new Context(false))); // Part 2
    }
}