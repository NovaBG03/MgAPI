using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using MgAPI.Models;

namespace MgAPI.JSONModels
{
    public class CreatePostRequest
    {
        [Required]
        public string AuthorID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
