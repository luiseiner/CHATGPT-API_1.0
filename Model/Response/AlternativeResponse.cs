using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Response
{
    public class AlternativeResponse
    {
        public int AlternativeID { get; set; }
        public int QuestionID { get; set; }
        public string TextEn { get; set; }
        public string TextEs { get; set; }
        public string? TranslationChatGptEn { get; set; } // Permitir valores nulos
        public string? TranslationChatGptEs { get; set; }
        public DateTime CreationDate { get; set; }  
        public DateTime ModificationDate { get; set; }  
        public bool IsEnabled { get; set; }  
    }

}
