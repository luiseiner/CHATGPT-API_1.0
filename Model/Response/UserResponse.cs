using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
namespace Model.Response
{
    public class UserResponse_user
    {
        public int UserID { get; set; }
        public int LanguageID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        
        public bool IsEnabled { get; set; }
    }

}
