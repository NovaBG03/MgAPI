﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MgAPI.Models
{
    public class User
    {
        [Key]
        public string ID { get; set; }
        
        [Required]
        public string Firstname { get; set; }
        
        [Required]
        public string Lastname { get; set; }
       
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }
        
        [Required]
        public DateTime CreationDate { get; set; }
        
        [Required]
        public Role Role { get; set; }
        
        public virtual List<Post> Posts { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }
    }
}
