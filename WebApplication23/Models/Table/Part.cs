using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication23.Models
{
    [Table("part")]
    public class Part
    {
        [Key]
        public int Id { get; set; }
        public int id_category { get; set; }
        public string Image { get; set; }
        public string description { get; set; }
        public string Name { get; set; }
        public int IsActive { get; set; }
        public string Link { get; set; }
        public string Type { get; set; }

    }
}
