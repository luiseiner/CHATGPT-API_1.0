using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Response
{
    public class CategorySimpleResponse
    {

        public int CategoryID { get; set; }
        public string NameEn { get; set; }
        public string NameEs { get; set; }

        public string Prompt { get; set; }

        public List<QuestionResponseSimplified> Questions { get; set; }
    }
}