using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication23.Entities
{
    public class GetQuestion
    {
        int id_part,id, id_lession;
        DateTime date;

        public GetQuestion(int id_part, int id, int id_lession)
        {
            this.Id_part = id_part;
            this.Id = id;
            this.Id_lession = id_lession;
        }

        public GetQuestion()
        {
        }

        public GetQuestion(DateTime date)
        {
            this.date = date;
        }

        public int Id_part { get => id_part; set => id_part = value; }
        public int Id { get => id; set => id = value; }
        public int Id_lession { get => id_lession; set => id_lession = value; }
        public DateTime Date { get => date; set => date = value; }
        public string Day { get => day; set => day = value; }

        private string day;

        public GetQuestion(string day)
        {
            Day = day;
        }
    }
}
