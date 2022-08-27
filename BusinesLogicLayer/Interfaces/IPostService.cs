using SocialNetwork.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogicLayer.Interfaces
{
    public interface IPostService : IService<PostRequest>
    {
        public List<PostResponse> GetAllMyPosts(int userId);
    }
}
