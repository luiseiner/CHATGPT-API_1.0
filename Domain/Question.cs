using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Question
    {
       
            public int QuestionID { get; set; }
            public int CategoryID { get; set; }
            public int QuestionTypeID { get; set; }
            public string TextEn { get; set; }
            [Column(TypeName = "varchar(200)")]
            public string TextEs { get; set; }
            [Column(TypeName = "varchar(200)")]
            public DateTime CreationDate { get; set; }
            public DateTime ModificationDate { get; set; }
            public bool IsEnabled { get; set; }

            public QuestionType QuestionType { get; set; }
            public Category Category { get; set; }
            public ICollection<UserResponse> UserResponses { get; set; }
            public ICollection<Alternative> Alternatives { get; set; }
       
       
       
    }

}
