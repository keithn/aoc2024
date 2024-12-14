namespace Aoc2024;

public class Day2
{
    public static void Solve(string input)
    {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToArray();
        var reports = lines.Select(l => l.Split(' ').Select(int.Parse).ToArray()).ToArray();

        bool Safe(int[] report)
        {
            var diffs = report.Skip(1).Select((v, i) => v - report[i]);
            return (diffs.All(v => v > 0) || diffs.All(v => v < 0)) &&
                   diffs.All(v => int.Abs(v) is 1 or 2 or 3);
        }

        var part1 = reports.Count(Safe);
        Console.WriteLine(part1);

        IEnumerable<int[]> WithOmissions(int[] report) =>
            report.Select((_, omitIndex) => report.Where((_, i) => i != omitIndex).ToArray());

        bool SafeWithinTolerance(int[] report) => Safe(report) || WithOmissions(report).Any(Safe);

        var part2 = reports.Count(SafeWithinTolerance);
        Console.WriteLine(part2);
    }
}