using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Response
{
    public class QuestionResponseSimple
    {
        
        public string QuestionText { get; set; }
        public string SelectedAnswer { get; set; }
        public string TranslationChatGpt { get; set; }



    }
}
