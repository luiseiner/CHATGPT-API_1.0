using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Response
{
    public class ProntResponse
    {
        public int ProntID { get; set; }
        public int UserID { get; set; }
        public int CategoryID { get; set; }
        public DateTime DatePront { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public bool IsEnabled { get; set; }
    }

}
