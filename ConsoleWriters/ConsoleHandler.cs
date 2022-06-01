using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelMCQuestsGeneration.Console
{
    internal class ConsoleHandler
    {
        private OrderedDictionary _autoCompletes;

        public ConsoleHandler(Dictionary<String, String> autoComplete) {
            _autoCompletes = (OrderedDictionary) autoComplete.OrderBy(x => x.Key).CreateOrderedEnumerable(x => x.Key, Comparer<string>.Default, false);
        }

        public void ClearCurrentLine(int cursorLeft)
        {
            var currentLine = System.Console.CursorTop;
            System.Console.SetCursorPosition(cursorLeft, System.Console.CursorTop);
            System.Console.Write(new string(' ', System.Console.WindowWidth - cursorLeft));
            System.Console.SetCursorPosition(cursorLeft, currentLine);
        }
    }
}
