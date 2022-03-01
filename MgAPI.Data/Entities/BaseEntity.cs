using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MgAPI.Data.Entities
{
    public class BaseEntity
    {
        [Key]
        public string ID { get; set; }
    }
}
