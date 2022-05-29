using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelMCQuestsGeneration.CommandSignatures
{
    public class AbsolutePositionCommand : ICommandSignature
    {
        public AbsolutePositionCommand()
        {
        }

        public string ObjectiveName { get; } = "ABSOLUTE_POSITION";
        public Dictionary<string, string> NamedArguments { get; } = new Dictionary<string, string>();
        public IEnumerable<string> PositionalArguments { get; } = new List<String>
        {
            "x1", "y1", "z1",
            "x2", "y2", "z2",
            "dimension"
        };

        public bool Validate(string objective)
        {
            var split = objective.Split(" ");
            // return name is objective name, has correct number of args and all args are int
            return split[0] == ObjectiveName && split.Length == 8 && split.Skip(1).All(a => int.TryParse(a, out int i));
        }
    }
}
