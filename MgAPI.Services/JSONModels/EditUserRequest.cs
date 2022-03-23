using System.ComponentModel.DataAnnotations;

namespace MgAPI.Business.JSONModels
{
    public class EditUserRequest
    {
        [Required]
        public string ID { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

    }
}
