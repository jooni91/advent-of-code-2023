using AdventOfCode2023;
using System.Reflection;

internal class Program
{
    private static async Task Main(string[] args)
    {
        if (args.Length == 0)
        {
            args = AskForArguments().ToArray() ?? Array.Empty<string>();
        }

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

        Console.WriteLine($"If you want to continue running another solution type y and press enter.");

        if (Console.ReadLine() == "y")
        {
            await Main([]);
        }
    }

    static IEnumerable<string> AskForArguments()
    {
        Console.WriteLine("You didn't pass the day and puzzle arguments to the program. What day do you want to run?");
        yield return Console.ReadLine() ?? string.Empty;
        Console.WriteLine("Which part of the day, 1 or 2?");
        yield return Console.ReadLine() ?? string.Empty;
    }
}