using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Response
{
    public class CategoryResponse
    {
        public int CategoryID { get; set; }
        public int UserID { get; set; }
        public string NameEn { get; set; }
        public string NameEs { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public bool IsEnabled { get; set; }

        public string Prompt { get; set; }
        public List<QuestionResponse> Questions { get; set; }
    }

}
