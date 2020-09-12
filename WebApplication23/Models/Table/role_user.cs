using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication23.Models
{
    [Table("role_user")]
    public class role_user
    {
        [Key]
        public int roleId { get; set; }
        public string name { get; set; }
        public string isActive { get; set; }
    }
}
