using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MgAPI.Business.JSONModels
{
    public class CreateFileRequest
    {
        [Required]
        public string PostID { get; set; }

        [Required]
        public string Path { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string NameInServer { get; set; }

        [Required]
        public string Extension { get; set; }

    }
}
