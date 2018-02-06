using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Events
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int IdRelation { get; set; }
        public bool? IsConfirmed { get; set; }
        public String Type { get; set; }
    }
}
