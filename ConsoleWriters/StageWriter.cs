using PixelMCQuestsGeneration.Console;
using PixelMCQuestsGeneration.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelMCQuestsGeneration.ConsoleWriters
{
    internal class StageWriter : IQuestWriter
    {
        private PixelmonQuest _pixelmonQuest;
        private Dictionary<string,string>.Enumerator _stringsEnumerator;

        public StageWriter(Serialization.PixelmonQuest pixelmonQuest)
        {
            this._pixelmonQuest = pixelmonQuest;
        }

        public void ClearCurrentLine(int cursorLeft)
        {
            var currentLine = System.Console.CursorTop;
            System.Console.SetCursorPosition(cursorLeft, System.Console.CursorTop);
            System.Console.Write(new string(' ', System.Console.WindowWidth - cursorLeft));
            System.Console.SetCursorPosition(cursorLeft, currentLine);
        }

        private void StartAutoComplete() { 
            this._stringsEnumerator = this._pixelmonQuest.Strings.GetEnumerator();
        }

        private string NextAutoComplete()
        {
            var x = _stringsEnumerator.Current;
            _stringsEnumerator.MoveNext();
            return x.Key;
        }

        private void ConsoleAutoComplete() {
            ClearCurrentLine(System.Console.CursorLeft);
            System.Console.WriteLine(NextAutoComplete());
        }

        public void GiveConsole()
        {
            // type objectives, on tab call ConsoleAutoComplete. This will clear the whole line so need to clear just the word.

            throw new NotImplementedException();
        }
    }
}
