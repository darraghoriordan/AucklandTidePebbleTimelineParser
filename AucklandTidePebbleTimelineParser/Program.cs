using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic.FileIO;

namespace AucklandTidePebbleTimelineParser
{
    class Program
    {
        private static string apiKey;


        private static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("You must supply an API key.");
                Console.WriteLine("You must supply a filename.");
                Console.WriteLine("e.g. program.exe c:\\tideData.csv testApiKey123");
                Environment.Exit(1);
            }

            apiKey = args[0];
            var path = args[1];
            if (!File.Exists(path))
            {
                Console.WriteLine("Couldn't find " + path);
                return;
            }

            var tideNotifications = ReadNotifications(path);

            File.WriteAllLines(path + ".curl.txt", tideNotifications);
        }

        private static List<string> ReadNotifications(string path)
        {
            var tideParser = new TideParser();
            var notificationFactory = new PebbleTimeTideNotificationFactory(apiKey);
            var tideNotifications = new List<string>();

            using (var parser = new TextFieldParser(path))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    //Processing row
                    var fields = parser.ReadFields();
                    if (fields != null)
                    {
                        tideNotifications.AddRange(notificationFactory.GetTideNotifications(tideParser.ParseTides(fields)));
                    }
                }
            }
            return tideNotifications;
        }
    }
}

