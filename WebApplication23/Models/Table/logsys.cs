using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication23.Models
{
    [Table("log_system")]
    public class Logsys
    {
        [Key]
        public int id { get; set; }
        public string description { get; set; }
        public DateTime date { get; set; }
        public int id_user { get; set; }

        public Logsys(string description, DateTime date, int id_user)
        {
            this.description = description;
            this.date = date;
            this.id_user = id_user;
        }

        public Logsys()
        {
        }
    }
}
