﻿using Newtonsoft.Json;
using Reddit.NET.Models.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class ModmailSubreddit
    {
        [JsonProperty("display_name")]
        public string DisplayName;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("lastUpdated")]
        [JsonConverter(typeof(TimestampConvert))]
        public DateTime LastUpdated;

        [JsonProperty("keyColor")]
        public string KeyColor;

        [JsonProperty("subscribers")]
        public int Subscribers;

        [JsonProperty("id")]
        public string Id;

        [JsonProperty("icon")]
        public string Icon;
    }
}
