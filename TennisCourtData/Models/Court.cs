using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TennisCourtData.Models
{
    public class Court
    {
        public int id { get; set; }
        [System.ComponentModel.DisplayName("Court")]
        public string name { get; set; }
        public IEnumerable<Slot> slots { get; set; }

        public Court()
        {
            id = -1;
        }

        public void Update(Court court)
        {
            name = court.name;
        }
    }
}