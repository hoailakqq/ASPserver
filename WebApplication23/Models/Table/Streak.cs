using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication23.Models
{
    [Table("streak")]
    public class Streak
    {[Key]
            public string Id { get; set; }
            public string id_user { get; set; }
            public string streak { get; set; }
            public string best_streak { get; set; }
    }
}
