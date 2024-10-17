using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Request
{
    public class AlternativeRequest
    {
        public int QuestionID { get; set; }
        public string TextEn { get; set; }
        public string TextEs { get; set; }
        public string? TranslationChatGptEn { get; set; } // Permitir valores nulos
        public string? TranslationChatGptEs { get; set; }
    }

}
