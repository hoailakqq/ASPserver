using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication23.Models
{
    [Table("category")]
    public class Category
    {

            [Key]
            public int Id { get; set; }
            public string Name { get; set; }
            public string Color1 { get; set; }
            public string Color2 { get; set; }
            public string Image { get; set; }
            public string Link { get; set; }
            public int isActive { get; set; }
    }
}
