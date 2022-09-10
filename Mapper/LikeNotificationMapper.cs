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
        public LikeNotificationDto toDto(LikeNotification entity)
        {
            LikeNotificationDto dto = new LikeNotificationDto();
            dto.Date = entity.Date;
            dto.FromWhoId = entity.FromWhoId;
            dto.FromWhoPicture = entity.FromWho.ProfilePicture;
            dto.FromWhoUsername = entity.FromWho.UserName;
            dto.PostId = entity.PostId;
            dto.PostPicture = entity.Post.ImagePath;
            return dto;
        }

        public LikeNotification toEntity(LikeNotificationDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
