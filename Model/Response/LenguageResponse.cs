using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Response
{
    public class LanguageResponse
    {
        public int LanguageID { get; set; }
        public string LanguageName { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public bool IsEnabled { get; set; }
    }
}
