using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using KapiteinHoek.Models;

namespace KapiteinHoek
{
    public class Bot
    {
        public static void Start(Func<TurnState, PlacePiecesCommand> strategy, Logger writer)
        {
            try
            {
                // Output before bot-start signal is ignored.
                // Sending bot-start signals that the bot is ready to receive data.
                writer.WriteLineWithConsole("bot-start");

                string line;

                if ((line = Console.In.ReadNextLine()) != "game-init")
                {
                    writer.FlushAndThrow($"Expected 'game-init', got '{line}'");
                }

                writer.WriteLine(line);
                TurnState turnState;

                while ((line = Console.In.ReadNextLine()) != "game-start")
                {
                    try
                    {
                        turnState = JsonConvert.DeserializeObject<TurnState>(line);
                        if (turnState == null)
                        {
                            writer.FlushAndThrow("Deserialization resulted in null object");
                        }
                    }
                    catch (Exception e)
                    {
                        writer.WriteLine(e);
                        writer.FlushAndThrow($"'game-state' was not initialized correctly, with error {e.Message}, got '{line}'");
                    }

                    writer.WriteLine(line);
                }

                writer.WriteLine(line);

                while ((line = Console.In.ReadNextLine()) != "game-end")
                {
                    writer.WriteLine(line);

                    if (line != "turn-init")
                    {
                        writer.FlushAndThrow($"Expected 'turn-init', got '{line}'");
                    }

                    var echos = new List<string>();
                    var sendOutputAfterTurnEnd = false;

                    while ((line = Console.In.ReadNextLine()) != "turn-start")
                    {
                        writer.WriteLine(line);
                        turnState = JsonConvert.DeserializeObject<TurnState>(line);
                        var placePiecesCommand = strategy(turnState);
                        echos.Add(JsonConvert.SerializeObject(placePiecesCommand));

                        if (line.StartsWith("throw")) writer.FlushAndThrow("De Kapitein luistert naar het universem en gooit exceptioneel goed... ehmm... *iets*!");
                        if (line.StartsWith("win")) writer.WriteLineWithConsole("win");
                        if (line.StartsWith("sleep")) Thread.Sleep(1000);

                        if (line.StartsWith("stderr"))
                        {
                            Console.Error.WriteLine("Requested output on stderr");
                            Console.Error.WriteLine("An extra line of error for good measure");
                        }

                        if (line.StartsWith("extra-output"))
                        {
                            sendOutputAfterTurnEnd = true;
                        }
                    }

                    foreach (var echo in echos)
                    {
                        writer.WriteLineWithConsole(echo);
                    }

                    writer.WriteLineWithConsole("turn-end");

                    if (sendOutputAfterTurnEnd)
                    {
                        writer.WriteLineWithConsole("Extra output sent after turn-end but before the next turn-start should be ignored");
                    }
                }

                writer.WriteLine(line);
            }
            catch (Exception e)
            {
                writer.FlushAndThrow(e); // Starter bot tries to continue at this point. But let's try throwing?
            }
        }
    }

    static class StreamReaderExtensions
    {
        internal static string ReadNextLine(this TextReader reader)
        {
            string line;
            while ((line = reader.ReadLine()) == null)
            {
                Thread.Sleep(10);
            }
            return line;
        }
    }
}