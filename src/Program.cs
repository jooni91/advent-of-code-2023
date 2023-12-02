using AdventOfCode2023;
using BenchmarkDotNet.Running;
using System.Reflection;

internal class Program
{
    private static async Task Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("You didn't pass the day and puzzle arguments to the program. What day do you want to run?");
            args = [.. args, Console.ReadLine() ?? string.Empty];

#if !DEBUG
            Console.WriteLine("Run benchmark? (y/n)");
            var runBenchmark = (Console.ReadLine() ?? string.Empty) == "y";

            if (runBenchmark)
            {
                await RunBenchmark(args);
                return;
            }
#endif

            Console.WriteLine("Which part of the day? (1/2)");
            args = [.. args, Console.ReadLine() ?? string.Empty];
        }

        await RunSolution(args);
    }

    static async Task RunSolution(string[] args)
    {
        if (args == null || args.Length != 2)
        {
            throw new ArgumentException();
        }

        Console.WriteLine($"Starting part {args[1]} of day {args[0]}.");

        var dayType = Assembly.GetAssembly(typeof(DayBase))!.GetType($"AdventOfCode2023.Solutions.Day{(args[0].Length == 1 ? "0" : "")}{args[0]}.Day{args[0]}");

        if (dayType == null || Activator.CreateInstance(dayType) is not DayBase solutionForDay)
        {
            throw new NullReferenceException("Couldn't find solution for specified day.");
        }

        Console.WriteLine($"The result for day {args[0]} part {args[1]} is {await solutionForDay.GetResultAsync((Part)Enum.Parse(typeof(Part), args[1]))}.");

        Console.WriteLine($"");

        await RestartOrExit();
    }
    static async Task RunBenchmark(string[] args)
    {
        var dayType = Assembly.GetAssembly(typeof(DayBase))!.GetType($"AdventOfCode2023.Solutions.Day{(args[0].Length == 1 ? "0" : "")}{args[0]}.Day{args[0]}");

        BenchmarkRunner.Run(dayType!);

        await RestartOrExit();
    }
    static async Task RestartOrExit()
    {
        Console.WriteLine($"If you want to continue running another solution type y and press enter.");

        if (Console.ReadLine() == "y")
        {
            await Main([]);
        }
    }
}