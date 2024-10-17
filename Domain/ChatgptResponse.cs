using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ChatGptResponse
    {
        [Key]
        public int ResponseID { get; set; }
        public int? UserID { get; set; }

        public string CategoryName { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public bool IsEnabled { get; set; } 

        
    }

}
