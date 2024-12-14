// See https://aka.ms/new-console-template for more information

using Aoc2024;

// Day1.Solve(Load("Day1.txt"));
// Day2.Solve(Load("Day2.txt"));
Day3.Solve(Load("Day3.txt"));


string Load(string file) => File.ReadAllText(Path.ChangeExtension(file, ".txt"));