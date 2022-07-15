using PixelMCQuestsGeneration.Console;
using PixelMCQuestsGeneration.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelMCQuestsGeneration.Console
{
    internal class StageWriter : AbstractQuestWriter
    {
        private PixelmonQuest _pixelmonQuest;
        private List<Stage> _stages;

        public StageWriter(Serialization.PixelmonQuest pixelmonQuest)
        {
            this._pixelmonQuest = pixelmonQuest;
            this._stages = _pixelmonQuest.Stages;
            this._startAutoCompleteFunc = () => this._pixelmonQuest.Strings.GetEnumerator();
        }

        public override void GiveConsole()
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

        public override void Save()
        {
            this._pixelmonQuest.Stages = this._stages;
        }
    }
}
