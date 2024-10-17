using Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Request
{
    public class ChatgptRequestSemple
    {
        public int UserID { get; set; }
        public string CategoryName { get; set; }
        public List<QuestionResponseSimple> Questions { get; set; }
    }
}
