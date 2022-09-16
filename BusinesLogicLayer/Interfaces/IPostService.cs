using SocialNetwork.Dto;

namespace BusinesLogicLayer.Interfaces
{
    public interface IPostService : IService<PostRequest>
    {
        public List<PostResponse> GetAllMyPosts(int userId, string username);
        public List<PostResponse> GetAllForHome(int i, int numOfPosts);
    }
}
