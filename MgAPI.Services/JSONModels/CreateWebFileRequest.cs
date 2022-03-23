using System.ComponentModel.DataAnnotations;

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
