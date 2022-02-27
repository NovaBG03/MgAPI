using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MgAPI.Business.JSONModels
{
    public class CreateWebFileRequest
    {
        [Required]
        public string PostID { get; set; }

        [Required]
        public string Localpath { get; set; }

    }
}
