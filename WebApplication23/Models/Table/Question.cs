using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication23.Models
{
    [Table("question")]
    public class QuestionModel
    {
        [Key]
        public int Id { get; set; }
        public int Id_Lession { get; set; }
        public int Id_Part { get; set; }
        public string title_question { get; set; }
        public string Question { get; set; }
        public string DapanA { get; set; }
        public string DapanB { get; set; }
        public string DapanC { get; set; }
        public string DapanD { get; set; }
        public string Answer { get; set; }
        public string description { get; set; }
        public string Sound { get; set; }
        public string Image { get; set; }
        public int isActive { get; set; }

        public QuestionModel(int id_Lession, int id_Part, string title_question, string question, string dapanA, string dapanB, string dapanC, string dapanD, string answer, string description, string sound, string image, int isActive)
        {
            Id_Lession = id_Lession;
            Id_Part = id_Part;
            this.title_question = title_question;
            Question = question;
            DapanA = dapanA;
            DapanB = dapanB;
            DapanC = dapanC;
            DapanD = dapanD;
            Answer = answer;
            this.description = description;
            Sound = sound;
            Image = image;
            this.isActive = isActive;
        }

        public QuestionModel()
        {
        }
    }
}
