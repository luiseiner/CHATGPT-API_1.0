using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Response
{
    public class UserResponseResponse
    {
        public int UserResponseID { get; set; }
        public int UserID { get; set; }
        public int QuestionID { get; set; }
        public string ResponseEn { get; set; }
        public string ResponseEs { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public bool IsEnabled { get; set; }
    }

}
