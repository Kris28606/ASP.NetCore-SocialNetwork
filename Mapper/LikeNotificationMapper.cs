using Domain;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    public class LikeNotificationMapper : GenericMapper<LikeNotificationDto, LikeNotification>
    {
        private UserMapper userMapper;
        private PostResponseMapper postMapper;
        public LikeNotificationMapper()
        {
            this.userMapper = new UserMapper();
            this.postMapper = new PostResponseMapper();
        }
        public LikeNotificationDto toDto(LikeNotification entity)
        {
            LikeNotificationDto dto = new LikeNotificationDto();
            dto.Date = entity.Date;
            dto.Post = postMapper.toDto(entity.Post);
            dto.FromWho = userMapper.toDto(entity.FromWho);
            return dto;
        }

        public LikeNotification toEntity(LikeNotificationDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
