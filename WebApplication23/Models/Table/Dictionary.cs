using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication23.Models
{
    [Table("dictionary")]
    public class Dictionary
    {
        [Key]
        public int Id { get; set; }
        public string Word { get; set; }
        public string Pronounced { get; set; }
        public string Type1 { get; set; }
        public string Content1 { get; set; }
        public string Type2 { get; set; }
        public string Content2 { get; set; }
        public string Type3 { get; set; }
        public string Content3 { get; set; }
        public string Type4 { get; set; }
        public string Content4 { get; set; }

        public Dictionary()
        {
        }

        public Dictionary(string word)
        {
            Word = word;
        }
    }
}
