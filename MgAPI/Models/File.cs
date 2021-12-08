using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MgAPI.Models
{
    public class File
    {
        [Key]
        public int ID { get; set; }
        
        [Required]
        public Post Post { get; set; }
        
        [Required]
        public string Path { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string NameInServer { get; set; }
        
        [Required]
        public string Extension { get; set; }

        public File()
        {

        }
    }
}
