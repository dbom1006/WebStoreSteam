using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreSteam.Models
{
    public class Item
    {
        public int Id { get; set; }
        
        public int AppId { get; set; }
        public long ContextId = 2;
        [JsonProperty("classid")]
        public string ClassId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("icon_url")]
        public string IconUrl { get; set; }
        [JsonProperty("name_color")]
        public string Color { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("tradable")]
        public int Tradable { get; set; }
        [JsonProperty("marketable")]
        public int Marketable { get; set; }
        public List<Action> ListAction { get; set; }
        public List<Tag> ListTag { get; set; }
    }

    public class Tag
    {
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("internal_name")]
        public string InternalName { get; set; }
        [JsonProperty("category_name")]
        public string Category { get; set; }
        [JsonProperty("color")]
        public string Color { get; set; }
    }
    public class Action
    {
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("link")]
        public string Link { get; set; }
    }
}