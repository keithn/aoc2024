namespace Aoc2024;

public class Day7
{
    public record Calibration(long Value, long[] Weights)
    {
        public static Calibration Parse(string s)
        {
            var parts = s.Split([':', ' '], StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
            return new Calibration(parts[0], parts[1..]);
        }
        public bool IsValid(bool allowConcat = false) => CheckCombination(0, Weights[0], allowConcat);

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
        var part1 = calibrations.Where(c => c.IsValid()).Sum(c => c.Value);
        Console.WriteLine(part1);
        var part2 = calibrations.Where(c => c.IsValid(true)).Sum(c => c.Value);
        Console.WriteLine(part2);
    }
}