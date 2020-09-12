using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication23.Models
{
    [Table("rank")]
    public class Rank
    {
        [Key]
        public int Id { get; set; }
        public int Id_user { get; set; }
        public int Total_score { get; set; }
        public int Current_score { get; set; }
        public int Crown { get; set; }
        public int Streak { get; set; }
        public int BestStreak { get; set; }
        public int Hint { get; set; }
        public DateTime StartDate { get; set; }

        public Rank(int id_user)
        {
            this.Id_user = id_user;
        }

        public Rank()
        {
        }

        public Rank(int id_user, int total_score, int crown) : this(id_user)
        {
            Total_score = total_score;
            Crown = crown;
        }
    }
}
