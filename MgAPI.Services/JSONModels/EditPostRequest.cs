using System.ComponentModel.DataAnnotations;

namespace MgAPI.Business.JSONModels
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
