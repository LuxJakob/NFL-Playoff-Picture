using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace NFLPlayoffPicture.ConsoleApp
{
    public class TeamRecord
    {
        public int Seed { get; set; }
        public string Team { get; set; } = string.Empty;
        public string Division { get; set; } = string.Empty;
        public string Record { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }

    public class PlayoffPictureData
    {
        public DateTime LastUpdated { get; set; }
        public List<TeamRecord> AfcSeeds { get; set; } = new();
        public List<TeamRecord> NfcSeeds { get; set; } = new();
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating NFL Playoff Picture data...");

            var data = new PlayoffPictureData
            {
                LastUpdated = DateTime.Now,
                AfcSeeds = new List<TeamRecord>
                {
                    new TeamRecord { Seed = 1, Team = "Kansas City Chiefs", Division = "AFC West", Record = "11-2", Status = "Clinched Division" },
                    new TeamRecord { Seed = 2, Team = "Buffalo Bills", Division = "AFC East", Record = "10-3", Status = "In Playoff Spot" },
                    new TeamRecord { Seed = 3, Team = "Baltimore Ravens", Division = "AFC North", Record = "9-4", Status = "In Playoff Spot" }
                },
                NfcSeeds = new List<TeamRecord>
                {
                    new TeamRecord { Seed = 1, Team = "Detroit Lions", Division = "NFC North", Record = "12-1", Status = "Clinched Playoff" },
                    new TeamRecord { Seed = 2, Team = "Philadelphia Eagles", Division = "NFC East", Record = "11-2", Status = "In Playoff Spot" },
                    new TeamRecord { Seed = 3, Team = "Seattle Seahawks", Division = "NFC West", Record = "8-5", Status = "In Playoff Spot" }
                }
            };

            // Determine output directory: ../../../wwwroot/data/ relative to execution bin
            string outputDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "..", "wwwroot", "data");

            // Fallback for direct execution
            if (!Directory.Exists(outputDirectory))
            {
                outputDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data");
            }

            Directory.CreateDirectory(outputDirectory);
            string outputPath = Path.Combine(outputDirectory, "output.json");

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(data, options);

            File.WriteAllText(outputPath, jsonString);
            Console.WriteLine($"Successfully generated output at: {Path.GetFullPath(outputPath)}");
        }
    }
}