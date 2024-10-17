using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Request
{
    public class ChatGptRequest
    {
        public int ProntID { get; set; }
        public string Type { get; set; }
        public Dictionary<string, string> Inputs { get; set; }
    }
}
