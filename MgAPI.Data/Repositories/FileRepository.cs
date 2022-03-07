using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MgAPI.Data.Entities;

namespace MgAPI.Data.Repositories
{
    public class FileRepository : BaseRepository<WebFile>
    {
        public FileRepository(Context context)
            : base(context)
        {

        }
    }
}
