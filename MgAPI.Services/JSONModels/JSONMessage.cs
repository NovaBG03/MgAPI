using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MgAPI.Business.JSONModels
{
    public class JSONMessage
    {
        public string Message { get; set; }

        public JSONMessage(string message)
        {
            Message = message;
        }
    }
}
