namespace Aoc2024;

public class Day8
{
    public static void Solve(string input)
    {
        var grid = input.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(row => row.ToCharArray()).ToArray();
        var width = grid[0].Length;
        var height = grid.Length;
        bool InBounds(int x, int y) => x >= 0 && x < width && y >= 0 && y < height;

        int GenerateAntiNodes(bool resonantFrequencies = false)
        {
            var frequencies = grid.SelectMany(row => row).Distinct().Where(c => c != '.').ToArray();
            var antiNodes = new HashSet<(int, int)>();
            foreach (var antenna in frequencies)
            {
                var locations = grid
                    .SelectMany((row, y) => row.Select((ch, x) => (x, y, ch)))
                    .Where(cell => cell.ch == antenna)
                    .Select(cell => (cell.x, cell.y))
                    .ToList();

                foreach (var (x1, y1) in locations)
                {
                    if (resonantFrequencies) antiNodes.Add((x1, y1));

                    foreach (var (x2, y2) in locations.Where(p => p != (x1, y1)))
                    {
                        var (dx, dy) = (x2 - x1, y2 - y1);
                        var (ax1, ay1) = (x1, y1);
                        var (ax2, ay2) = (x2, y2);
                        while (true)
                        {
                            var extended = false;

                            (ax1, ay1) = (ax1 - dx, ay1 - dy);
                            if (InBounds(ax1, ay1))
                            {
                                antiNodes.Add((ax1, ay1));
                                extended = true;
                            }

                            (ax2, ay2) = (ax2 + dx, ay2 + dy);
                            if (InBounds(ax2, ay2))
                            {
                                antiNodes.Add((ax2, ay2));
                                extended = true;
                            }

                            if (!resonantFrequencies || !extended) break;
                        }
                    }
                }
            }
            return antiNodes.Count;
        }

        Console.WriteLine(GenerateAntiNodes());
        Console.WriteLine(GenerateAntiNodes(true));
    }
}