using PixelMCQuestsGeneration.Console;
using PixelMCQuestsGeneration.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelMCQuestsGeneration.Console
{
    internal class StageWriter : IQuestWriter
    {
        private PixelmonQuest _pixelmonQuest;
        private Dictionary<string, string>.Enumerator _stringsEnumerator;
        private List<Stage> _stages;

        public StageWriter(Serialization.PixelmonQuest pixelmonQuest)
        {
            this._pixelmonQuest = pixelmonQuest;
            this._stages = _pixelmonQuest.Stages;
        }

        public void ClearCurrentLine(int cursorLeft)
        {
            var currentLine = System.Console.CursorTop;
            System.Console.SetCursorPosition(cursorLeft, System.Console.CursorTop);
            System.Console.Write(new string(' ', System.Console.WindowWidth - cursorLeft));
            System.Console.SetCursorPosition(cursorLeft, currentLine);
        }

        public void ClearLetters(int numberChars) {

            new string[] { "\b", " ", "\b" }
            .ToList()
            .ForEach(c => {
                for (int i = 0; i < numberChars; i++)
                    System.Console.Write(c);
            });
        }

        private void StartAutoComplete() {
            this._stringsEnumerator = this._pixelmonQuest.Strings.GetEnumerator();
        }

        private string NextAutoComplete()
        {
            bool moveNext = _stringsEnumerator.MoveNext();
            if (!moveNext)
            {
                StartAutoComplete();
                _stringsEnumerator.MoveNext();
            }
            KeyValuePair<string, string> x = _stringsEnumerator.Current;
            return x.Key;
        }

        private long RetrieveLong(string prompt) {
            long x;
            do
            {
                System.Console.WriteLine(prompt);
            } while (!long.TryParse(System.Console.ReadLine(), out x));
            return x;
        }

        private string RetrieveString(string prompt) {
            string? inp;
            do
            {
                System.Console.WriteLine(prompt);
                inp = System.Console.ReadLine();
            } while (inp == null);
            return inp;
        }

        private List<string> RetrieveList(string prompt) {
            List<string> list = new List<string>(); 
            StringBuilder sb = new StringBuilder();
            do
            {
                System.Console.WriteLine(prompt);
                this.StartAutoComplete();
                string lastAutoComplete = "";
                bool isLastActionAutoComplete = false;
                while (true)
                {
                    ConsoleKeyInfo consoleKey = System.Console.ReadKey();
                    char key = consoleKey.KeyChar;

                    if (consoleKey.Key == ConsoleKey.Enter)
                        break;

                    if(consoleKey.Key == ConsoleKey.Tab)
                    {
                        string a = this.NextAutoComplete();
                        ClearLetters(4);
                        if (isLastActionAutoComplete)
                            ClearLetters(lastAutoComplete.Length);
                        System.Console.Write(a);
                        lastAutoComplete = a;
                        isLastActionAutoComplete = true;
                    } else
                        isLastActionAutoComplete = false;

                    sb.Append(key);
                }

                list.Add(sb.ToString());
            } while (sb.Length == 0);

            return list;
        }

        public void GiveConsole()
        {
            while (true) {
                Stage newStage = new Stage();
                newStage.StageStage = RetrieveLong("Stage Number: ");
                newStage.NextStage = RetrieveLong("Next Stage Number: ");
                newStage.Icon = RetrieveString("Icon: ");
                newStage.Objectives = RetrieveList("Objectives: ");
                newStage.Actions = RetrieveList("Actions: ");
                this._stages.Add(newStage);

                System.Console.Write("Another stage?");
                string s = System.Console.ReadLine();
                if (!string.IsNullOrEmpty(s) && s.ToLower()[0] == 'n')
                    break;
            }

            this.Save();
        }

        public void Save()
        {
            this._pixelmonQuest.Stages = this._stages;
        }
    }
}
