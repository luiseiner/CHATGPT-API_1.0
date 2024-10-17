using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class User
    {
        public int UserID { get; set; }
        public int LanguageID { get; set; }
        public string Username { get; set; }
        [Column(TypeName = "varchar(200)")]

        public string Email { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string Password { get; set; }
        [Column(TypeName = "varchar(200)")]
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        
        public bool IsEnabled { get; set; }


        public Language Language { get; set; }
        
        public ICollection<UserResponse> UserResponses { get; set; }
    }

}
