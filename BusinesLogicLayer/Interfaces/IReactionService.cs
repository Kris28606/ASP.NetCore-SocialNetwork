using SocialNetwork.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogicLayer.Interfaces
{
    public interface IReactionService : IService<UserDto>
    {
        public void LikeIt(int postId, string username);
        public bool UnlikeIt(int postId, string username);
        public List<UserDto> GetLikes(int postId, string username);
    }
}
