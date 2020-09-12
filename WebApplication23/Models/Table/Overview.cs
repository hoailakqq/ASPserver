using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication23.Models
{
    [Table("overview")]
    public class Overview
    {[Key]
        public int Id { get; set; }
        public int Id_user { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Status2 { get; set; }
        public int IsActive { get; set; }
    }
}
