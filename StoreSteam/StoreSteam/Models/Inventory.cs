using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreSteam.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public int NumSlots { get; set; }
        public Item[] Items { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsGood { get; set; }
    }
}