using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace KapiteinHoek
{
    public class Logger
    {
        private readonly Stopwatch stopwatch = new Stopwatch();
        private readonly IList<string> messages = new List<string>();
        private readonly string fileLocation;

        public Logger(string fileLocation)
        {
            this.fileLocation = fileLocation;
            this.WriteLine("Started logger");
        }

        public void WriteLine(string line, string lvl = "LOG") => messages.Add($"[{stopwatch.Elapsed} {lvl}]: {line}");
        public void WriteLine(Exception e) => WriteLine($"{e.GetType()}: {e.Message}", "ERR");

        public void WriteLineWithConsole(string line)
        {
            WriteLine(line, "OUT");
            Console.WriteLine(line);
        }

        public void FlushAndThrow(Exception e) => FlushAndThrow($"{e.GetType()}: {e.Message}", "ERR");
        public void FlushAndThrow(string finalMessage, string lvl = "LOG")
        {
            WriteLine(finalMessage, lvl);
            var fullLog = string.Join(Environment.NewLine, messages);
            File.WriteAllText(fileLocation, fullLog);
            throw new Exception(fullLog);
        }
    }
}
