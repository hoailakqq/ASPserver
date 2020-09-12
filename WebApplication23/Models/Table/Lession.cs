using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication23.Models
{
    [Table("lession")]
    public class Lession
    {
        [Key]
        public int Id { get; set; }
        public int Id_category { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string ImageCheck { get; set; }
        public int IsActive { get; set; }
        public string Link { get; set; }
    }
}
