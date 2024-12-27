using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerApi.Models
{
    public class player
    {
        public int id { get; set; }
        public int age { get; set; }
        public string name { get; set; } = string.Empty;
        public string position { get; set; } = string.Empty;
        public string team { get; set; } = string.Empty;

    }
}