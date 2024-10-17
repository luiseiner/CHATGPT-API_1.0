using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Pront
    {
        public int ProntID { get; set; }
        public int UserID { get; set; }
        public int CategoryID { get; set; }
        public DateTime DatePront { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public bool IsEnabled { get; set; }


        public User User { get; set; }
        public Category Category { get; set; }

        
    }

}
