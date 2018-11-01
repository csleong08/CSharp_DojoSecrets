using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DojoSecrets.Models
{
    public class Secrets
    {
        [Key]
        public int id { get; set; }
        public string message { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime created_at { get; set; }
        public int usersid { get; set; }
        public Users Users { get; set; }
        public Secrets()
        {
            Like = new List<Likes>();
        }
        public List<Likes> Like { get; set;}
    }
}