using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;    

namespace DojoSecrets.Models
{
    public class Users
    {
        [Key]
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime created_at { get; set; }
        public Users()
        {
            Secret = new List<Secrets>();
            Like = new List<Likes>();
        }
        public List<Secrets> Secret { get; set;}
        public List<Likes> Like { get; set;}
    }
}