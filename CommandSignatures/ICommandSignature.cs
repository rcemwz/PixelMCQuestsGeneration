using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelMCQuestsGeneration.CommandSignatures
{
    internal interface ICommandSignature
    {
        public string ObjectiveName { get; }
        public Dictionary<string, string> NamedArguments { get; }

        public IEnumerable<string> PositionalArguments { get; }

        public bool Validate(string objective);
    }
}
