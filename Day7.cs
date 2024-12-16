namespace Aoc2024;

public class Day7
{
    public record Calibration(long Value, long[] Weights)
    {
        public static Calibration Parse(string s)
        {
            var parts = s.Split(':');
            var value = long.Parse(parts[0]);
            var weights = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
            return new Calibration(value, weights);
        }
        public bool IsValid(bool allowConcat) => CheckCombination(0, Weights[0], allowConcat);

        private bool CheckCombination(int index, long current, bool allowConcat)
        {
            if (index == Weights.Length - 1) return current == Value;
            var next = Weights[index + 1];
            return CheckCombination(index + 1, current + next, allowConcat) ||
                   CheckCombination(index + 1, current * next, allowConcat) ||
                   (allowConcat && CheckCombination(index + 1, Concat(current, next), allowConcat));
        }
        long Concat(long a, long b) => long.Parse($"{a}{b}");
    }

    public static void Solve(string input)
    {
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var calibrations = lines.Select(Calibration.Parse).ToList();
        var part1 = calibrations.Where(c => c.IsValid(false)).Sum(c => c.Value);
        Console.WriteLine(part1);
        var part2 = calibrations.Where(c => c.IsValid(true)).Sum(c => c.Value);
        Console.WriteLine(part2);
    }
}