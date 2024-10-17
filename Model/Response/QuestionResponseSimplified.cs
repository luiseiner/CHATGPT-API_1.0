using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Response
{
    public class QuestionResponseSimplified
    {
        public int QuestionID { get; set; }
        public int QuestionTypeID { get; set; }
        public string TextEn { get; set; }
        public string TextEs { get; set; }
        public int SelectedAlternativeID { get; set; }
        public List<AlternativeResponseSimplified> Alternatives { get; set; }
    }
}
