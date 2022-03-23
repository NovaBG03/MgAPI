using System.ComponentModel.DataAnnotations;

namespace MgAPI.Data.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public string ID { get; set; }
    }
}
