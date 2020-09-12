using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication23.Models.Table
{
    [Table("Log")]
    public class Log
    {
        [Key]
       public int id { get; set; }
        public DateTime current_day { get; set; }
        public int actural_score { get; set; }
        public int expect_score { get; set; }
        public   int id_user { get; set; }

        public Log(DateTime current_day, int actural_score, int expect_score, int id_user)
        {
            this.current_day = current_day;
            this.actural_score = actural_score;
            this.expect_score = expect_score;
            this.id_user = id_user;
        }

        public Log()
        {
        }

        public Log(int id_user)
        {
            this.id_user = id_user;
        }

        public Log(DateTime current_day)
        {
            this.current_day = current_day;
        }

        public Log(DateTime current_day, int id_user) : this(current_day)
        {
            this.id_user = id_user;
        }
    }
}
