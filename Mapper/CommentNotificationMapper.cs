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
        private UserMapper userMapper;
        private PostResponseMapper postMapper;
        public CommentNotificationMapper()
        {
            this.userMapper = new UserMapper();
            this.postMapper = new PostResponseMapper();
        }

        public CommentNotificationDto toDto(CommentNotification entity)
        {
            CommentNotificationDto dto = new CommentNotificationDto();
            dto.Date = entity.Date;
            dto.FromWho = userMapper.toDto(entity.FromWho);
            dto.Post = postMapper.toDto(entity.Post);
            dto.Comment = entity.Comment;

            return dto;
        }

        public CommentNotification toEntity(CommentNotificationDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
