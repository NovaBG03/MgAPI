using System.ComponentModel.DataAnnotations;

namespace MgAPI.Business.JSONModels
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
