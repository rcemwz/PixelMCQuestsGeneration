using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PixelMCQuestsGeneration.Serialization
{
    public partial class PixelmonQuest
    {
        [JsonProperty("radiant")]
        public bool Radiant { get; set; }

        [JsonProperty("weight")]
        public long Weight { get; set; }

        [JsonProperty("abandonable")]
        public bool Abandonable { get; set; }

        [JsonProperty("repeatable")]
        public bool Repeatable { get; set; }

        [JsonProperty("color")]
        public Color Color { get; set; }

        [JsonProperty("activeStage")]
        public long ActiveStage { get; set; }

        [JsonProperty("stages")]
        public List<Stage> Stages { get; set; }

        [JsonProperty("strings")]
        public Dictionary<String, String> Strings { get; set; }

        public static PixelmonQuest FromJson(string json) => JsonConvert.DeserializeObject<PixelmonQuest>(json, Converter.Settings);

        public string ToJson() => JsonConvert.SerializeObject(this, Converter.Settings);

        public override string ToString() => this.ToJson();
    }

    public partial class Color
    {
        [JsonProperty("r")]
        public long R { get; set; }

        [JsonProperty("g")]
        public long G { get; set; }

        [JsonProperty("b")]
        public long B { get; set; }
    }

    public partial class Stage
    {
        [JsonProperty("stage")]
        public long StageStage { get; set; }

        [JsonProperty("nextStage")]
        public long NextStage { get; set; }

        [JsonProperty("icon", NullValueHandling = NullValueHandling.Ignore)]
        public string Icon { get; set; }

        [JsonProperty("objectives")]
        public List<string> Objectives { get; set; }

        [JsonProperty("actions")]
        public List<string> Actions { get; set; }
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
            Formatting = Formatting.Indented,
        };
    }
}
