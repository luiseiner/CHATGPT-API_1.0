using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Response
{
    public class ChatGptResponseRep
    {
        public int ResponseID { get; set; }
        public int UserID { get; set; }
        public string CategoryName { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public bool IsEnabled { get; set; }
    }
}
