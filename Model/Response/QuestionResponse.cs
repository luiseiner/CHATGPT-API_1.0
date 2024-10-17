using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Response
{
    public class QuestionResponse

    {
        public int CategoryID { get; set; }
        public int QuestionID { get; set; }
        public int QuestionTypeID { get; set; }
        public int SelectedAlternativeID { get; set; }
        public string TextEn { get; set; }
        public string TextEs { get; set; }
        
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public string QuestionText { get; set; } // Text of the question in the selected language
        public string SelectedAnswer { get; set; }

        public bool IsEnabled { get; set; }

        public List<AlternativeResponse> Alternatives { get; set; }
    }
}
