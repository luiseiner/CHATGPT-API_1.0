using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Request
{
    public class UserResponseRequestPost
    {
        public int QuestionID { get; set; }
        public int UserID { get; set; }
        public string ResponseEn { get; set; }
        public string ResponseEs { get; set; }
    }
}
