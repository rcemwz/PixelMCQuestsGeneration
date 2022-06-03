using PixelMCQuestsGeneration.Serialization;
using PixelMCQuestsGeneration.Console;

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

List<IQuestWriter> Writers = new List<IQuestWriter>
{
    new StringsWriter(pixelmonQuest),
    new StageWriter(pixelmonQuest),
};

Writers.ToList().ForEach(w => w.GiveConsole());

Console.WriteLine();
Console.WriteLine(pixelmonQuest.ToJson());

using (StreamWriter writer = new StreamWriter(args[0])) {
    writer.Write(pixelmonQuest.ToJson());
}