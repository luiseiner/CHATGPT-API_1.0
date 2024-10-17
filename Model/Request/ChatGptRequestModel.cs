using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Request
{
    public class ChatGptRequestModel
    {
        public int? UserID { get; set; }
        public string CategoryName { get; set; }
        public string Request { get; set; }
    }
}
