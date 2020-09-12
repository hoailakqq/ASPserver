using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication23.Entities
{
    public class GetID
    {
        int id;

        public int Id { get => id; set => id = value; }

        public GetID(int id)
        {
            this.Id = id;
        }

        public GetID()
        {
        }
        public string Token { get; set; }

        public GetID(string token)
        {
            Token = token;
        }
    }
}
