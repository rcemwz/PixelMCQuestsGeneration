using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelMCQuestsGeneration.Console
{
    internal abstract class AbstractQuestWriter : IQuestWriter
    {
        private Dictionary<string, string>.Enumerator _stringsEnumerator;
        protected Func<Dictionary<string, string>.Enumerator> _startAutoCompleteFunc;

        protected long RetrieveLong(string prompt)
        {
            long x;
            do
            {
                System.Console.WriteLine(prompt);
            } while (!long.TryParse(System.Console.ReadLine(), out x));
            return x;
        }

        protected string RetrieveString(string prompt)
        {
            string? inp;
            do
            {
                System.Console.WriteLine(prompt);
                inp = System.Console.ReadLine();
            } while (inp == null);
            return inp;
        }

        protected List<string> RetrieveList(string prompt)
        {
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

                    if (consoleKey.Key == ConsoleKey.Tab)
                    {
                        string a = this.NextAutoComplete();
                        ClearLetters(4);
                        if (isLastActionAutoComplete)
                            ClearLetters(lastAutoComplete.Length);
                        System.Console.Write(a);
                        lastAutoComplete = a;
                        isLastActionAutoComplete = true;
                    }
                    else
                        isLastActionAutoComplete = false;

                    sb.Append(key);
                }

                list.Add(sb.ToString());
            } while (sb.Length == 0);

            return list;
        }

        public void ClearCurrentLine(int cursorLeft)
        {
            var currentLine = System.Console.CursorTop;
            System.Console.SetCursorPosition(cursorLeft, System.Console.CursorTop);
            System.Console.Write(new string(' ', System.Console.WindowWidth - cursorLeft));
            System.Console.SetCursorPosition(cursorLeft, currentLine);
        }

        public void ClearLetters(int numberChars)
        {

            new string[] { "\b", " ", "\b" }
            .ToList()
            .ForEach(c => {
                for (int i = 0; i < numberChars; i++)
                    System.Console.Write(c);
            });
        }

        protected void StartAutoComplete()
        {
            this._stringsEnumerator = this._startAutoCompleteFunc.Invoke();
        }

        protected string NextAutoComplete()
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

        public abstract void GiveConsole();
        public abstract void Save();
    }
}
