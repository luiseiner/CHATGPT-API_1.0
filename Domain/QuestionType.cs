using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class QuestionType
    {
        public int QuestionTypeID { get; set; }
        public string TypeEn { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string TypeEs { get; set; }
        [Column(TypeName = "varchar(200)")]
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public bool IsEnabled { get; set; }
    }

}
