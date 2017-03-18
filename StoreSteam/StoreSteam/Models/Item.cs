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
        public string ClassId { get; set; }
        public string Name { get; set; }
        public string IconUrl { get; set; }
        public string Color { get; set; }
        public string Type { get; set; }
        public bool Tradable { get; set; }
        public bool Marketable { get; set; }
        public List<Action> Actions { get; set; }
        public List<Tag> Tags { get; set; }
    }

    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string InternalName { get; set; }
        public string Category { get; set; }
        public string Color { get; set; }
    }
    public class Action
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
    }
}