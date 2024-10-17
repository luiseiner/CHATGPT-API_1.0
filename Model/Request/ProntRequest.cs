using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Request
{
    public class ProntRequest
    {
        public int UserID { get; set; }
        public int CategoryID { get; set; }
        public DateTime DatePront { get; set; }
    }

}
