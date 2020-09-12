using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication23.Entities
{
    public class UsernameObj
    {
        public string Username { get; set; }

        public UsernameObj(string username)
        {
            Username = username;
        }

        public UsernameObj()
        {
        }
        
    }
}
