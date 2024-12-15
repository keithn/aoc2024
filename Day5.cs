namespace Aoc2024;

public class Day5
{
    public static void Solve(string input)
    {
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var rules = lines.Where(l => l.Contains('|')).Select(l => l.Split('|').Select(int.Parse).ToArray()).ToArray();
        var updates = lines.Where(l => !l.Contains('|')).Select(l => l.Split(',').Select(int.Parse).ToArray()).ToList();


        bool FollowsRules(int[] update)
        {
            var notAllowed = new HashSet<int>();
            return update.All(page =>
            {
                if (notAllowed.Contains(page)) return false;
                notAllowed.UnionWith(rules.Where(r => r[1] == page).Select(r => r[0]));
                return true;
            });
        }

        var part1 = updates.Where(FollowsRules).Select(u => u[u.Length / 2]).Sum();
        Console.WriteLine(part1);

        int RuleComparer(int x, int y) => rules.Any(r => r[0] == x && r[1] == y) ? -1 : 1;
        var part2 = updates.Where(u => !FollowsRules(u)).Select(u => u.Order(Comparer<int>.Create(RuleComparer)).ToArray())
            .Select(u => u[u.Length / 2]).Sum();
        Console.WriteLine(part2);
    }
}