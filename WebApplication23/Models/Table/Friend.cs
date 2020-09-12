using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication23.Models
{
    [Table("friend")]
    public class Friend
    {
        public string Id { get; set; }
        public string Id_user { get; set; }
        public string Id_friend { get; set; }
    }
}
