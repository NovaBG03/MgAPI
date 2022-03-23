using MgAPI.Data.Entities;
using MgAPI.Data.Interfaces;

namespace MgAPI.Data.Repositories
{
    public class WebFileRepository : BaseRepository<WebFile>, IWebFileRepository
    {
        public WebFileRepository(Context context)
            : base(context)
        { }
    }
}
