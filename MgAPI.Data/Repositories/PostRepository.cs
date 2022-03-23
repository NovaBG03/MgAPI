using MgAPI.Data.Entities;
using MgAPI.Data.Interfaces;

namespace MgAPI.Data.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(Context context)
            : base(context)
        { }
    }
}