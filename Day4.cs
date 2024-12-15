using System.Text;

namespace Aoc2024;

public class Day4
{
    public static void Solve(string input)
    {
        var grid = input.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(row => row.ToCharArray()).ToArray();
        var width = grid[0].Length;
        var height = grid.Length;

        string Collect(int x, int y, int dx, int dy, int n) =>
            new(Enumerable.Range(0, n)
                .Select(step => (x: x + step * dx, y: y + step * dy))
                .TakeWhile(pos => pos.x >= 0 && pos.x < width && pos.y >= 0 && pos.y < height)
                .Select(pos => grid[pos.y][pos.x])
                .ToArray());        
        
        (int dx, int dy)[] directions = [(1, 0), (1, 1), (0, 1), (-1, 1), (-1, 0), (-1, -1), (0, -1), (1, -1)];

        var part1 = Enumerable.Range(0, height)
            .SelectMany(y => Enumerable.Range(0, width)
                .SelectMany(x => directions
                    .Where(dir => Collect(x, y, dir.dx, dir.dy, 4) == "XMAS")))
            .Count();
        Console.WriteLine(part1);

        var part2 = Enumerable.Range(0, height)
            .SelectMany(y => Enumerable.Range(0, width)
                .Where(x => 
                    (Collect(x, y, 1, 1, 3) is "MAS" or "SAM") && 
                    (Collect(x + 2, y, -1, 1, 3) is "MAS" or "SAM")))
            .Count();
        Console.WriteLine(part2);
    }
}