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
        public static void Start(Func<TurnState, PlacePiecesCommand> strategy, string fileLocation)
        {
            using var fileStream = new FileStream(fileLocation, FileMode.Create);
            using var writer = new StreamWriter(fileStream) {AutoFlush = true};

            try
            {
                // Output before bot-start signal is ignored.
                // Sending bot-start signals that the bot is ready to receive data.
                Console.WriteLine("bot-start");

                string line;

                if ((line = Console.In.ReadNextLine()) != "game-init")
                {
                    throw new Exception($"Expected 'game-init', got '{line}'");
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
                            throw new Exception("Deserialization resulted in null object");
                        }
                    }
                    catch (Exception e)
                    {
                        writer.WriteLine(e);
                        throw new Exception(
                            $"'game-state' was not initialized correctly, with error {e.Message}, got '{line}'");
                    }

                    writer.WriteLine(line);
                }

                writer.WriteLine(line);

                while ((line = Console.In.ReadNextLine()) != "game-end")
                {
                    writer.WriteLine(line);

                    if (line != "turn-init")
                    {
                        throw new Exception($"Expected 'turn-init', got '{line}'");
                    }

                    var echos = new List<string>();

                    var sendOutputAfterTurnEnd = false;

                    while ((line = Console.In.ReadNextLine()) != "turn-start")
                    {
                        writer.WriteLine(line);

                        turnState = JsonConvert.DeserializeObject<TurnState>(line);

                        var placePiecesCommand = strategy(turnState);

                        echos.Add(JsonConvert.SerializeObject(placePiecesCommand));

                        if (line.StartsWith("throw"))
                        {
                            throw new Exception("Throw requested");
                        }

                        if (line.StartsWith("win"))
                        {
                            Console.WriteLine("win");
                        }

                        if (line.StartsWith("sleep"))
                        {
                            Thread.Sleep(1000);
                        }

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
                        Console.WriteLine(echo);
                    }

                    Console.WriteLine("turn-end");

                    if (sendOutputAfterTurnEnd)
                    {
                        Console.WriteLine(
                            "extra output sent after turn-end but before the next turn-start should be ignored");
                    }
                }

                writer.WriteLine(line);
            }
            catch(Exception e)
            {
                writer.WriteLine(e);
            }
            finally
            {
                writer.Close();
            }
        }
    }

    static class StreamReaderExtensions
    {
        internal static string ReadNextLine(this TextReader reader)
        {
            string line = null;
            while ((line = reader.ReadLine()) == null)
            {
                Thread.Sleep(10);
            }

            return line;
        }
    }
}