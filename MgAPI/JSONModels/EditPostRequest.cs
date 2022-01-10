using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MgAPI.JSONModels
{
    public class EditPostRequest
    {
        [Required]
        public string ID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
