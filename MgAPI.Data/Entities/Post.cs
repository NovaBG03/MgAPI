using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MgAPI.Data.Entities
{
    public class Post
    {
        [Key]
        public string ID { get; set; }

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
