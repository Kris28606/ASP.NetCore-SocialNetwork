using Domain;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    public class FollowNotificationMapper : GenericMapper<FollowNotificationDto, FollowNotification>
    {
        public FollowNotificationDto toDto(FollowNotification entity)
        {
            FollowNotificationDto dto = new FollowNotificationDto();
            dto.FromWhoPicture = entity.FromWho.ProfilePicture;
            dto.FromWhoUsername = entity.FromWho.UserName;
            dto.FromWhoId = entity.FromWhoId;
            dto.Date = entity.Date;
            return dto;
        }

        public FollowNotification toEntity(FollowNotificationDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
