using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Alternative
    {
        public int AlternativeID { get; set; }
        public int QuestionID { get; set; }
        public string TextEn { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string TextEs { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string TranslationChatGptEn { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string TranslationChatGptEs { get; set; }
        [Column(TypeName = "varchar(200)")]
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public bool IsEnabled { get; set; }


        public Question Question { get; set; }
    }

}
