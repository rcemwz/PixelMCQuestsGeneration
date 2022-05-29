using PixelMCQuestsGeneration.Serialization;
using System.Linq;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

Console.WriteLine(args[0]);

if (args.Length == 0 || !File.Exists(args[0]))
{
    Console.Error.WriteLine("Please specify file path to quests json.");
    System.Environment.Exit(1);
}

string fileContents = "";
using (StreamReader reader = new StreamReader(args[0])) { 
    fileContents = reader.ReadToEnd();
}

PixelmonQuest pixelmonQuest = PixelmonQuest.FromJson(fileContents);
Console.WriteLine(pixelmonQuest.ToJson());