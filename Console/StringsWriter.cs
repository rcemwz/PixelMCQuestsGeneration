using PixelMCQuestsGeneration.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelMCQuestsGeneration.Console
{
    internal class StringsWriter : IQuestWriter
    {
        private PixelmonQuest _pixelmonQuest;
        public Dictionary<string, string> Strings { get; private set; }

        public StringsWriter(PixelmonQuest quest, Dictionary<String, String> strings) {
            this.Strings = strings;
            this._pixelmonQuest = quest;
        }

        public StringsWriter(PixelmonQuest quest)
        {
            this.Strings = quest.Strings;
            this._pixelmonQuest = quest;
        }

        public void Add(Dictionary<String, String> entries) { 
            Strings.Concat(entries);
        }

        public void Add(string a, string b)
        {
            Strings.Add(a, b);
        }

        public void Save() { 
            this._pixelmonQuest.Strings = Strings;
        }

        public void GiveConsole() {
            string key;
            string value;

            System.Console.WriteLine("Enter Strings: ");
            do
            {
                System.Console.WriteLine("Key: ");
                key = System.Console.ReadLine();

                if (key == "") break;

                System.Console.WriteLine("Value: ");
                value = System.Console.ReadLine();

                this.Add(key, value);
            } while (key != "");
            this.Save();
        }
    }
}
