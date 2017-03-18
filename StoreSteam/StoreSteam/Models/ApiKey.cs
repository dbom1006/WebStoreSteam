using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreSteam.Models
{
    public class ApiKey
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string SteamId { get; set; }
    }
}