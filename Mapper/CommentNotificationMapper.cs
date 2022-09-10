using Domain;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    public class CommentNotificationMapper : GenericMapper<CommentNotificationDto, CommentNotification>
    {

        public CommentNotificationDto toDto(CommentNotification entity)
        {
            CommentNotificationDto dto = new CommentNotificationDto();
            dto.Date = entity.Date;
            dto.FromWhoId = entity.FromWhoId;
            dto.FromWhoPicture = entity.FromWho.ProfilePicture;
            dto.FromWhoUsername = entity.FromWho.UserName;
            dto.PostId = entity.PostId;
            dto.PostPicture = entity.Post.ImagePath;
            dto.Comment = entity.Comment;

            return dto;
        }

        public CommentNotification toEntity(CommentNotificationDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
