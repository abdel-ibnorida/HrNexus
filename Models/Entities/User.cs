using System;
using System.Collections.Generic;
using HrNexus.Models.Entities;

namespace HrNexus.Models.Entities
{
    public class User
    { 
        public string Username { get; set; }
        public string Password { get; set; }
        public string Nome { get; set; }
         public User()
        {
        }
        public User(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
    }
}