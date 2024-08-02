﻿using HahnCargoTruckLoader.Model;
using System.Text.Json;

namespace HahnCargoTruckLoader.Helper
{
    public class FileHelper
    {
        public static async void WriteToFile(string callerId, string content, string fileName = "")
        {
            var rootPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            
            if (fileName == "" || fileName == "\"\"")
            {
                fileName = "HahnCargoTruckLoader" + callerId + ".json";
            }

            await using var streamWriter = new StreamWriter(Path.Combine(rootPath, fileName), false);
            await streamWriter.WriteLineAsync(content);
        }

        public static Truck? LoadTruckFromJson()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "HahnCargoTruckLoaderTruck.json");

            if (!File.Exists(path)) return null;

            var jsonString = File.ReadAllText(path);
            return JsonSerializer.Deserialize<Truck>(jsonString)!;
        }

        public static List<Crate>? LoadCratesFromJson()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "HahnCargoTruckLoaderCrates.json");

            if (!File.Exists(path)) return null;

            var jsonString = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<Crate>>(jsonString)!;
        }
    }
}