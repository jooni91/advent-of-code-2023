using System.Text.RegularExpressions;

namespace AdventOfCode2023.Utility
{
    public static class InputLoader
    {
        /// <summary>
        /// Get the content of a file as file stream.
        /// </summary>
        /// <param name="filePath">The relative path to a file.</param>
        /// <returns></returns>
        public static FileStream LoadInputsAsFileStream(string filePath, bool loggingEnabled = true)
        {
            if (loggingEnabled)
                Console.WriteLine("Started opening file stream.");

            var result = File.OpenRead(filePath);

            if (loggingEnabled)
                Console.WriteLine($"Opened file stream of file {filePath}.");

            return result;
        }

        /// <summary>
        /// Get the content of a file as string.
        /// </summary>
        /// <param name="filePath">The relative path to a file.</param>
        /// <returns></returns>
        public static string LoadInputsFromFileAsString(string filePath, bool loggingEnabled = true)
        {
            if (loggingEnabled)
                Console.WriteLine("Started loading inputs from file.");

            var result = File.ReadAllText(filePath);

            if (loggingEnabled)
                Console.WriteLine($"Loaded inputs from file {filePath}.");

            return result;
        }

        /// <summary>
        /// Split inputs by an specified separator.
        /// </summary>
        /// <param name="inputs">Inputs to split.</param>
        /// <param name="separator">The separator to use. Commas are used as a default separator if not specified.</param>
        /// <returns></returns>
        public static string[] SplitInputs(this string inputs, params char[] separator)
        {
            if (separator.Length == 0)
            {
                separator = new char[] { ',' };
            }

            return inputs.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Split inputs by an specified separator.
        /// </summary>
        /// <param name="inputs">Inputs to split.</param>
        /// <param name="keepSeparator">Include the separator in the returned result.</param>
        /// <param name="separator">The separator to use. Commas are used as a default separator if not specified.</param>
        /// <returns></returns>
        public static IEnumerable<string[]> SplitInputs(this IEnumerable<string> inputs, bool keepSeparator, params char[] separator)
        {
            if (separator.Length == 0)
            {
                separator = new char[] { ',' };
            }

            foreach (var s in inputs)
            {
                yield return keepSeparator ? Regex.Split(s, $@"({string.Concat(separator)})") : s.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        /// <summary>
        /// Converts each char of a string to an int value.
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public static IEnumerable<int> ConvertInputsToIntegers(this string inputs, bool loggingEnabled = false)
        {
            foreach (var s in inputs)
            {
                yield return Convert.ToInt32(s.ToString());
            }

            if (loggingEnabled)
                Console.WriteLine("Converted inputs to integer values.");
        }

        /// <summary>
        /// Converts an array of strings to int values.
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public static IEnumerable<int> ConvertInputsToIntegers(this string[] inputs, bool loggingEnabled = false)
        {
            foreach (var s in inputs)
            {
                yield return Convert.ToInt32(s);
            }

            if (loggingEnabled)
                Console.WriteLine("Converted inputs to integer values.");
        }

        /// <summary>
        /// Converts an <see cref="IEnumerable{string}"/> of strings to int values.
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public static IEnumerable<int> ConvertInputsToIntegers(this IEnumerable<string> inputs, bool loggingEnabled = false)
        {
            foreach (var s in inputs)
            {
                yield return Convert.ToInt32(s);
            }

            if (loggingEnabled)
                Console.WriteLine("Converted inputs to integer values.");
        }

        /// <summary>
        /// Converts an array of strings to long values.
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public static IEnumerable<long> ConvertInputsToLongs(this string[] inputs, bool loggingEnabled = false)
        {
            foreach (var s in inputs)
            {
                yield return Convert.ToInt64(s);
            }

            if (loggingEnabled)
                Console.WriteLine("Converted inputs to long values.");
        }

        /// <summary>
        /// Converts an <see cref="IEnumerable{string}"/> of strings to long values.
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public static IEnumerable<long> ConvertInputsToLongs(this IEnumerable<string> inputs, bool loggingEnabled = false)
        {
            foreach (var s in inputs)
            {
                yield return Convert.ToInt64(s);
            }

            if (loggingEnabled)
                Console.WriteLine("Converted inputs to long values.");
        }

        /// <summary>
        /// Converts an array of chars to an int array.
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public static IEnumerable<int> ConvertInputsToIntegers(this char[] inputs, bool loggingEnabled = false)
        {
            foreach (var s in inputs)
            {
                yield return Convert.ToInt32(s);
            }

            if (loggingEnabled)
                Console.WriteLine("Converted inputs to long values.");
        }

        /// <summary>
        /// Use to enumerate through a string which contains separated values, each on a new line.
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public static IEnumerable<string> ReadInputLines(this string inputs)
        {
            using var sr = new StringReader(inputs);

            while (sr.Peek() >= 0)
            {
                yield return sr.ReadLine()!;
            }
        }
    }
}
