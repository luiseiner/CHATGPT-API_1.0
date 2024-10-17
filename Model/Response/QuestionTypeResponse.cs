using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Response
{
    public class QuestionTypeResponse
    {
        public int QuestionTypeID { get; set; }
        public string TypeEn { get; set; }
        public string TypeEs { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public bool IsEnabled { get; set; }
    }

}
