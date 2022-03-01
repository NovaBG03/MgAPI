using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MgAPI.Data.Entities
{
    public class WebFile : BaseEntity
    {        

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

        public WebFile()
        {

        }
    }
}
