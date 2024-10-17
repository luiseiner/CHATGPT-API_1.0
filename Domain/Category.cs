using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string NameEn { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string NameEs { get; set; }
        [Column(TypeName = "varchar(200)")]
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }


        // Nuevo campo Prompt
        [Column(TypeName = "varchar(500)")]
        public string Prompt { get; set; }

        public bool IsEnabled { get; set; }

        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }

}
