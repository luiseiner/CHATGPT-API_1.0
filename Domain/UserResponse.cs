using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class UserResponse
    {
        public int UserResponseID { get; set; }
        public int QuestionID { get; set; }
        public int UserID { get; set; }
        public string ResponseEn { get; set; }
        public string ResponseEs { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public bool IsEnabled { get; set; }

        public Question Question { get; set; }
        public User User { get; set; }
    }

}
