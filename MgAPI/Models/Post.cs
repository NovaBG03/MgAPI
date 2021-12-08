using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MgAPI.Models
{
    public class Post
    {
        [Key]
        public int ID { get; set; }
        
        [Required]
        public User Author { get; set; }
       
        [Required]
        public DateTime PostDate { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Description { get; set; }
       
        public virtual List<File> Files { get; set; }

        public Post()
        {

        }
    }
}
