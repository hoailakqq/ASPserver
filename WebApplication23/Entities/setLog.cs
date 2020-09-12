using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication23.Entities
{
    public class setLog
    {
        int id_user;
        String date; String description;

        public int Id_user { get => id_user; set => id_user = value; }
        public string Date { get => date; set => date = value; }
        public string Description { get => description; set => description = value; }
    }
}
