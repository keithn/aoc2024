namespace Aoc2024;

public static class Day6
{
    public static void Solve(string input)
    {
        var startingGrid = input.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(row => row.ToCharArray()).ToArray();
        var width = startingGrid[0].Length;
        var height = startingGrid.Length;

        var startingPosition = startingGrid
            .SelectMany((row, y) => row.Select((ch, x) => (x, y, ch)))
            .FirstOrDefault(cell => cell.ch == '^');

        bool InBounds(int x, int y) => x >= 0 && x < width && y >= 0 && y < height;
        char[][] CloneGrid(char[][] grid) => grid.Select(row => row.ToArray()).ToArray();

        (bool looped, char[][] path) Patrol(char[][] grid, (int x, int y, char ch) guard)
        {
            var path = CloneGrid(grid);
            var visited = new HashSet<(int x, int y, char ch)>();
            while (InBounds(guard.x, guard.y))
            {
                var move = guard.ch switch {'^' => (0, -1), 'v' => (0, 1), '<' => (-1, 0), '>' => (1, 0),};
                var nextX = guard.x + move.Item1;
                var nextY = guard.y + move.Item2;
                if (InBounds(nextX, nextY) && path[nextY][nextX] == '#')
                {
                    guard = (guard.x, guard.y,
                        rotated: guard.ch switch {'^' => '>', 'v' => '<', '<' => '^', '>' => 'v',});
                }
                else
                {
                    if (!visited.Add(guard)) return (true, path);
                    path[guard.y][guard.x] = guard.ch;
                    guard = (nextX, nextY, guard.ch);
                }
            }

            return (false, path);
        }

        var (_, patrolledArea) = Patrol(startingGrid, startingPosition);
        var part1 = patrolledArea.Sum(row => row.Count(ch => ch is '^' or 'v' or '<' or '>'));

        Console.WriteLine(part1);

        var patrolled = patrolledArea
            .SelectMany((row, y) => row.Select((ch, x) => (x, y, ch)))
            .Where(cell => cell.ch is '^' or 'v' or '<' or '>' && cell != startingPosition)
            .ToList();

        T Mutate<T>(T obj, Action<T> action)
        {
            action(obj);
            return obj;
        }

        var part2 = patrolled
            .Select(candidate => Mutate(CloneGrid(startingGrid), path => path[candidate.y][candidate.x] = '#'))
            .Count(grid => Patrol(grid, startingPosition).looped);

        Console.WriteLine(part2);
    }

}