// See https://aka.ms/new-console-template for more information

using Aoc2024;

// Day1.Solve(Load("Day1.txt"));
// Day2.Solve(Load("Day2.txt"));
// Day3.Solve(Load("Day3.txt"));
// Day4.Solve(Load("Day4.txt"));
Day5.Solve(Load("Day5Example.txt"));
Day5.Solve(Load("Day5.txt"));


string Load(string file) => File.ReadAllText(Path.ChangeExtension(file, ".txt"));