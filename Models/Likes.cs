using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DojoSecrets.Models
{
    public class Likes
    {
        [Key]
        public int id { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime created_at { get; set; }
        public int usersid { get; set; }
        public Users Users { get; set; }
        public int secretsid { get; set; }
        public Secrets Secrets { get; set; }
    }
}