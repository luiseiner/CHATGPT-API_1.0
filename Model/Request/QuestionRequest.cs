using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Request
{
    public class QuestionRequest
    {
        public int CategoryID { get; set; }

        public int QuestionTypeID { get; set; }
   
        public string TextEn { get; set; }
        public string TextEs { get; set; }
       
    }

}
