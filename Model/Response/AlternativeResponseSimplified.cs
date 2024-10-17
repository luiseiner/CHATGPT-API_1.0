using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Response
{
    public class AlternativeResponseSimplified
    {
        public int AlternativeID { get; set; }
        public int QuestionID { get; set; }
        public string TextEn { get; set; }
        public string TextEs { get; set; }
        public string TranslationChatGptEn { get; set; }
        public string TranslationChatGptEs { get; set; }
    }
}
